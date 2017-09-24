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
            if (dt == null || dt.Rows.Count == 0)
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
            private static readonly MethodInfo Object_ToString = typeof(object).GetMethod("ToString");
            //private static readonly MethodInfo Convert_IsDBNull = typeof(DBConvert).GetMethod("IsDBNull", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToInt16 = typeof(DBConvert).GetMethod("ToInt16", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToInt32 = typeof(DBConvert).GetMethod("ToInt32", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToInt64 = typeof(DBConvert).GetMethod("ToInt64", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToBoolean = typeof(DBConvert).GetMethod("ToBoolean", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToDateTime = typeof(DBConvert).GetMethod("ToDateTime", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToDecimal = typeof(DBConvert).GetMethod("ToDecimal", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToDouble = typeof(DBConvert).GetMethod("ToDouble", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullInt16 = typeof(DBConvert).GetMethod("ToNullableInt16", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullInt32 = typeof(DBConvert).GetMethod("ToNullableInt32", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullInt64 = typeof(DBConvert).GetMethod("ToNullableInt64", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullBoolean = typeof(DBConvert).GetMethod("ToNullableBoolean", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullDateTime = typeof(DBConvert).GetMethod("ToNullableDateTime", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullDecimal = typeof(DBConvert).GetMethod("ToNullableDecimal", new Type[] { typeof(object) });
            private static readonly MethodInfo Convert_ToNullDouble = typeof(DBConvert).GetMethod("ToNullableDouble", new Type[] { typeof(object) });
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

                #region 测试用，生成dll文件
                //AssemblyName assemblyName = new AssemblyName("test");
                //AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                //ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("testModule", "test.dll");
                //TypeBuilder typeBuilder = moduleBuilder.DefineType("test", TypeAttributes.Public);
                //MethodBuilder method = typeBuilder.DefineMethod("SetModel", MethodAttributes.Public, typeof(Entity), new Type[] { typeof(DataRow), typeof(Entity) });
                #endregion

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
                        Type memberType = propertyInfo.PropertyType;
                        Type nullUnderlyingType = Nullable.GetUnderlyingType(memberType);
                        Type unboxType = nullUnderlyingType != null ? nullUnderlyingType : memberType;

                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);

                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);

                        //if (unboxType == typeof(byte[]) || unboxType == typeof(string))
                        //{
                        //    generator.Emit(OpCodes.Castclass, memberType);
                        //}
                        //else
                        //{
                        //    generator.Emit(OpCodes.Unbox_Any, unboxType);
                        //    if (nullUnderlyingType != null)
                        //    {
                        //        generator.Emit(OpCodes.Newobj, memberType.GetConstructor(new[] { nullUnderlyingType }));
                        //    }
                        //}
                        if (unboxType == typeof(byte[]))
                        {
                            generator.Emit(OpCodes.Castclass, memberType);
                        }
                        else if (unboxType == typeof(string))
                        {
                            generator.Emit(OpCodes.Callvirt, Object_ToString);
                        }
                        else
                        {
                            ConvertValueType(generator, propertyInfo);
                        }

                        //generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);

                #region 测试用，生成dll文件
                //Type t = typeBuilder.CreateType();
                //assemblyBuilder.Save("test.dll");
                #endregion

                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }

            /// <summary>  
            /// 由T的属性类型决定使用的Convert方法。  
            /// </summary>  
            /// <param name="ilg"></param>  
            /// <param name="pi"></param>  
            /// <returns></returns>
            private static void ConvertValueType(ILGenerator ilg, PropertyInfo pi)
            {
                //不可空类型  
                TypeCode code = Type.GetTypeCode(pi.PropertyType);
                switch (code)
                {
                    case TypeCode.Int16:
                        ilg.Emit(OpCodes.Call, Convert_ToInt16);
                        return;
                    case TypeCode.Int32:
                        ilg.Emit(OpCodes.Call, Convert_ToInt32);
                        return;
                    case TypeCode.Int64:
                        ilg.Emit(OpCodes.Call, Convert_ToInt64);
                        return;
                    case TypeCode.Boolean:
                        ilg.Emit(OpCodes.Call, Convert_ToBoolean);
                        return;
                    case TypeCode.String:
                        ilg.Emit(OpCodes.Callvirt, Object_ToString);
                        return;
                    case TypeCode.DateTime:
                        ilg.Emit(OpCodes.Call, Convert_ToDateTime);
                        return;
                    case TypeCode.Decimal:
                        ilg.Emit(OpCodes.Call, Convert_ToDecimal);
                        return;
                    case TypeCode.Double:
                        ilg.Emit(OpCodes.Call, Convert_ToDouble);
                        return;
                }
                //可空类型处理  
                Type type = Nullable.GetUnderlyingType(pi.PropertyType);
                if (type != null)
                {
                    code = Type.GetTypeCode(type);
                    switch (code)
                    {
                        case TypeCode.Int16:
                            ilg.Emit(OpCodes.Call, Convert_ToNullInt16);
                            return;
                        case TypeCode.Int32:
                            ilg.Emit(OpCodes.Call, Convert_ToNullInt32);
                            return;
                        case TypeCode.Int64:
                            ilg.Emit(OpCodes.Call, Convert_ToNullInt64);
                            return;
                        case TypeCode.Boolean:
                            ilg.Emit(OpCodes.Call, Convert_ToNullBoolean);
                            return;
                        case TypeCode.DateTime:
                            ilg.Emit(OpCodes.Call, Convert_ToNullDateTime);
                            return;
                        case TypeCode.Decimal:
                            ilg.Emit(OpCodes.Call, Convert_ToNullDecimal);
                            return;
                        case TypeCode.Double:
                            ilg.Emit(OpCodes.Call, Convert_ToNullDouble);
                            return;
                    }
                }
                throw new Exception(string.Format("不支持\"{0}\"类型的转换！", pi.PropertyType.Name));
            }

        }

        /// <summary>  
        /// 数据库类型的值到本地类型的转换类  
        /// </summary>  
        public static class DBConvert
        {
            public static bool IsDBNull(object value)
            {
                return object.Equals(DBNull.Value, value);
            }
            public static short ToInt16(object value)
            {
                if (value is short)
                {
                    return (short)value;
                }
                try
                {
                    return Convert.ToInt16(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static int ToInt32(object value)
            {
                if (value is int)
                {
                    return (int)value;
                }
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static long ToInt64(object value)
            {
                if (value is long)
                {
                    return (long)value;
                }
                try
                {
                    return Convert.ToInt64(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static bool ToBoolean(object value)
            {
                if (value == null)
                {
                    return false;
                }
                if (value is bool)
                {
                    return (bool)value;
                }
                if (value.Equals("1") || value.Equals("-1"))
                {
                    value = "true";
                }
                else if (value.Equals("0"))
                {
                    value = "false";
                }

                try
                {
                    return Convert.ToBoolean(value);
                }
                catch
                {
                    return false;
                }
            }
            public static DateTime ToDateTime(object value)
            {
                if (value is DateTime)
                {
                    return (DateTime)value;
                }
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            public static decimal ToDecimal(object value)
            {
                if (value is decimal)
                {
                    return (decimal)value;
                }
                try
                {
                    return Convert.ToDecimal(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static double ToDouble(object value)
            {
                if (value is double)
                {
                    return (double)value;
                }
                try
                {
                    return Convert.ToDouble(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static Nullable<short> ToNullableInt16(object value)
            {
                if (value is short)
                {
                    return new Nullable<short>((short)value);
                }
                try
                {
                    return new Nullable<short>(Convert.ToInt16(value));
                }
                catch
                {
                    return new Nullable<short>();
                }
            }
            public static Nullable<int> ToNullableInt32(object value)
            {
                if (value is int)
                {
                    return new Nullable<int>((int)value);
                }
                try
                {
                    return new Nullable<int>(Convert.ToInt32(value));
                }
                catch
                {
                    return new Nullable<int>();
                }
            }
            public static Nullable<long> ToNullableInt64(object value)
            {
                if (value is long)
                {
                    return new Nullable<long>((long)value);
                }
                try
                {
                    return new Nullable<long>(Convert.ToInt64(value));
                }
                catch
                {
                    return new Nullable<long>();
                }
            }
            public static Nullable<bool> ToNullableBoolean(object value)
            {
                if (value is bool)
                {
                    return new Nullable<bool>((bool)value);
                }
                try
                {
                    return new Nullable<bool>(Convert.ToBoolean(value));
                }
                catch
                {
                    return new Nullable<bool>();
                }
            }
            public static Nullable<DateTime> ToNullableDateTime(object value)
            {
                if (value is DateTime)
                {
                    return new Nullable<DateTime>((DateTime)value);
                }
                try
                {
                    return new Nullable<DateTime>(Convert.ToDateTime(value));
                }
                catch
                {
                    return new Nullable<DateTime>();
                }
            }
            public static Nullable<decimal> ToNullableDecimal(object value)
            {
                if (value is decimal)
                {
                    return new Nullable<decimal>((decimal)value);
                }
                try
                {
                    return new Nullable<decimal>(Convert.ToDecimal(value));
                }
                catch
                {
                    return new Nullable<decimal>();
                }
            }
            public static Nullable<double> ToNullableDouble(object value)
            {
                if (value is double)
                {
                    return new Nullable<double>((double)value);
                }
                try
                {
                    return new Nullable<double>(Convert.ToDouble(value));
                }
                catch
                {
                    return new Nullable<double>();
                }
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
