using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq.Expressions;

namespace Vic.Data
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class MethodExtension
    {
        /// <summary>
        /// 返回一个DataTable的List&lt;TResult&gt;实例
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        {
            List<TResult> list = new List<TResult>();
            if (dt == null)
                return list;
            DataTableEntityBuilder<TResult> eblist = DataTableEntityBuilder<TResult>.CreateBuilder(dt.Rows[0]);
            foreach (DataRow info in dt.Rows)
                list.Add(eblist.Build(info));
            dt.Dispose();
            dt = null;
            return list;
        }

        /// <summary>
        /// DataTable转实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        public class DataTableEntityBuilder<Entity>
        {
            private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });
            private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });
            private delegate Entity Load(DataRow dataRecord);
            private Load handler;
            private DataTableEntityBuilder() { }
            /// <summary>
            /// DataRow转实体
            /// </summary>
            /// <param name="dataRecord"></param>
            /// <returns></returns>
            public Entity Build(DataRow dataRecord)
            {
                return handler(dataRecord);
            }
            public static DataTableEntityBuilder<Entity> CreateBuilder(DataRow dataRecord)
            {
                DataTableEntityBuilder<Entity> dynamicBuilder = new DataTableEntityBuilder<Entity>();
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(Entity), new Type[] { typeof(DataRow) }, typeof(Entity), true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(typeof(Entity));
                generator.Emit(OpCodes.Newobj, typeof(Entity).GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);
                for (int i = 0; i < dataRecord.ItemArray.Length; i++)
                {
                    PropertyInfo propertyInfo = typeof(Entity).GetProperty(dataRecord.Table.Columns[i].ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);
                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }
        }

        /// <summary>
        /// 利用表达式树将IDataReader转换成泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this IDataReader reader) where T : class, new()
        {
            // 定义返回结果
            List<T> result = new List<T>();

            // 获取所有字段
            var properties = typeof(T).GetProperties().ToList();
            // 定义字典
            Dictionary<int, DataColumn> columnDics = new Dictionary<int, DataColumn>();
            //表达式字典委托   
            Dictionary<int, Action<T, IDataReader>> actionDics = new Dictionary<int, Action<T, IDataReader>>();
            //生成表头  
            for (int i = 0; i < reader.FieldCount; i++)
            {
                DataColumn col = new DataColumn()
                {
                    ColumnName = reader.GetName(i),
                    DataType = reader.GetFieldType(i),
                    Namespace = reader.GetDataTypeName(i)
                };
                //添加列  
                columnDics.Add(i, col);

                if (!properties.Exists(p => p.Name.ToUpper() == col.ColumnName))
                {
                    continue;
                }

                //获取字典值  
                actionDics.Add(i, SetValueToEntity<T>(i, col.ColumnName, col.DataType));
            }

            //查询读取项  
            while (reader.Read())
            {
                T objT = new T();

                //添加到集合  
                result.Add(objT);

                //填充属性值  
                foreach (var item in actionDics)
                {
                    //判断字段是否为null  
                    if (!reader.IsDBNull(item.Key))
                    {
                        //设置属性值  
                        item.Value(objT, reader);
                    }
                    else
                    {
                        //null处理  
                    }
                }
            }

            return result;
        }

        /// <summary>  
        /// 获取指定索引的数据并且返回调用委托  
        /// </summary>  
        /// <typeparam name="T">实体类类型</typeparam>  
        /// <param name="index">当前对应在DataReader中的索引</param>  
        /// <param name="ProPertyName">对应实体类属性名</param>  
        /// <param name="FieldType">字段类型</param>  
        /// <returns>返回通过调用的委托</returns>  
        private static Action<T, IDataRecord> SetValueToEntity<T>(int index, string ProPertyName, Type FieldType)
        {
            Type datareader = typeof(IDataRecord);
            var Mdthods = datareader.GetMethods().Where(p => p.ReturnType == FieldType && p.Name.StartsWith("Get") && p.GetParameters().Where(n => n.ParameterType == typeof(int)).Count() == 1);
            //获取调用方法  
            System.Reflection.MethodInfo Method = null;
            if (Mdthods.Count() > 0)
            {
                Method = Mdthods.FirstOrDefault();
            }
            else
            {
                throw new EntryPointNotFoundException("没有从DataReader找到合适的取值方法");
            }
            ParameterExpression e = Expression.Parameter(typeof(T), "e");
            ParameterExpression r = Expression.Parameter(datareader, "r");
            //常数表达式  
            ConstantExpression i = Expression.Constant(index);
            MemberExpression ep = Expression.PropertyOrField(e, ProPertyName);
            MethodCallExpression call = Expression.Call(r, Method, i);



            //instance.Property = value 这句话是重点  
            BinaryExpression assignExpression = Expression.Assign(ep, call);
            var ex = Expression.Lambda(assignExpression, e, r);

            Expression<Action<T, IDataRecord>> resultEx = Expression.Lambda<Action<T, IDataRecord>>(assignExpression, e, r);
            Action<T, IDataRecord> result = resultEx.Compile();

            return result;
        }

        ///<summary>  
        ///利用反射和泛型将SqlDataReader转换成List模型  
        ///</summary>  
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static IList<T> ExecuteToList<T>(IDataReader reader, string sql) where T : new()
        {
            IList<T> list;

            Type type = typeof(T);

            string columnName = string.Empty;


            list = new List<T>();
            while (reader.Read())
            {
                T t = new T();

                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    columnName = pi.Name;

                    DataView dv = reader.GetSchemaTable().DefaultView;
                    dv.RowFilter = "ColumnName= '" + columnName + "'";

                    if (dv.Count > 0)
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        var value = reader[columnName];

                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }

                    }

                }

                list.Add(t);

            }
            return list;
        }

        public static List<T> ToListByEmit<T>(this IDataReader reader) where T : class, new()
        {
            // 定义返回结果
            List<T> result = new List<T>();

            // 获取所有字段
            var properties = typeof(T).GetProperties().ToList();
            // 定义字典
            Dictionary<int, DataColumn> columnDics = new Dictionary<int, DataColumn>();
            //表达式字典委托   
            Dictionary<int, Action<T, IDataReader>> actionDics = new Dictionary<int, Action<T, IDataReader>>();
            //生成表头  
            for (int i = 0; i < reader.FieldCount; i++)
            {
                DataColumn col = new DataColumn()
                {
                    ColumnName = reader.GetName(i),
                    DataType = reader.GetFieldType(i),
                    Namespace = reader.GetDataTypeName(i)
                };
                //添加列  
                columnDics.Add(i, col);

                if (!properties.Exists(p => p.Name.ToUpper() == col.ColumnName))
                {
                    continue;
                }

                //获取字典值  
                actionDics.Add(i, SetValueToEntity<T>(i, col.ColumnName, col.DataType));
            }

            //查询读取项  
            while (reader.Read())
            {
                T objT = new T();

                objT = DataReaderEntityBuilder<T>.CreateBuilder(typeof(T), reader).Build(reader);

                //添加到集合  
                result.Add(objT);

            }

            return result;
        }
    }


    /// <summary>
    /// ** 描述：DataReader实体生成
    /// ** 创始时间：2010-2-28
    /// ** 修改时间：-
    /// ** 作者：网络
    /// ** 使用说明：
    /// </summary>
    public class DataReaderEntityBuilder<T>
    {
        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });
        private delegate T Load(IDataRecord dataRecord);

        private Load handler;

        /// <summary>
        /// DataReader
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public T Build(IDataRecord dataRecord)
        {
            return handler(dataRecord);
        }

        /// <summary>
        /// DataReader转化为实体
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static DataReaderEntityBuilder<T> CreateBuilder(Type type, IDataRecord dataRecord)
        {
            {
                DataReaderEntityBuilder<T> dynamicBuilder = new DataReaderEntityBuilder<T>();
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity", type,
                        new Type[] { typeof(IDataRecord) }, type, true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(type);
                generator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);
                for (int i = 0; i < dataRecord.FieldCount; i++)
                {
                    PropertyInfo propertyInfo = type.GetProperty(dataRecord.GetName(i), BindingFlags.IgnoreCase);
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);
                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }
        }
    }
}
