using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Vic.Data
{
    public partial class DataAccess
    {
        /// <summary>
        /// 执行查询语句，返回泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql) where T : class, new()
        {
            // 定义返回参数
            List<T> lstResult = null;

            try
            {
                // 定义数据库Reader对象
                using (DbDataReader reader = QueryReader(sql))
                {
                    lstResult = reader.ToList<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Query方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return lstResult;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            // 定义返回参数
            List<T> lstResult = null;

            try
            {
                Type type = typeof(T);

                // 表名
                string tableName = type.Name;
                // 查询条件
                string sqlWhere = string.Empty;
                // 排序
                string ordering = string.Empty;

                Oracle.SqlGenerator expression = new Oracle.SqlGenerator();

                sqlWhere = expression.GenerateSql(predicate);

                // 查询语句
                string querySql = string.Format("select * from {0} where 1=1 {1}", tableName, sqlWhere);

                using (DbDataReader reader = QueryReader(querySql))
                {
                    lstResult = reader.ToList<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Query方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return lstResult;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate参数不能为空！");
            }

            int updateRows = 0;

            try
            {
                Type type = typeof(T);

                // 表名
                string tableName = type.Name;
                // 查询条件
                string sqlWhere = string.Empty;
                // 排序
                string ordering = string.Empty;

                Oracle.SqlGenerator expression = new Oracle.SqlGenerator();

                sqlWhere = expression.GenerateSql(predicate);

                // 查询语句
                string querySql = string.Format("delete from {0} where 1=1 {1}", tableName, sqlWhere);

                updateRows = ExecuteNonQuery(querySql);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return updateRows;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class, new()
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity参数不能为空！");
            }

            // 取得表名
            string tableName = typeof(T).Name;

            // 删除表名
            int updateRows = Delete(entity, tableName);

            return updateRows;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int Delete(dynamic entity,string tableName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity参数不能为空！");
            }

            // 更新记录数
            int updateRows = 0;

            //(T)Convert.ChangeType(propValue, typeof(T));

            try
            {
                // 获取类型
                Type type = entity.GetType();
                // 查询条件
                string sqlWhere = string.Empty;
                // 排序
                string ordering = string.Empty;

                foreach (var item in type.GetProperties())
                {
                    string propName = item.Name;
                    string propValue = string.Empty;

                    // 获取属性值             
                    object objValue = entity.GetType().GetProperty(propName).GetValue(entity, null);

                    if (type.Name == tableName && objValue == null)
                    {
                        continue;
                    }

                    Type propType = item.PropertyType;

                    if (propType == typeof(DateTime))
                    {

                        if (DateTime.MinValue.ToString() == objValue.ToString() && type.Name == tableName)
                        {
                            continue;
                        }
                        else if (DateTime.MinValue.ToString() == objValue.ToString())
                        {
                            propValue = null;
                        }
                        else
                        {
                            propValue = string.Format("to_date('{0}','yyyy-mm-dd hh24:mm:ss')", propValue.ToString());
                        }
                    }
                    else if (propType == typeof(int) || propType == typeof(decimal))
                    { 
                        propValue = objValue.ToString();

                        if (propValue == "0")
                        {
                            propValue = null;
                        }
                    }
                    else
                    {
                        propValue = string.Format("'{0}'", objValue.ToString());
                    }

                    if (propValue != null)
                    {  
                        sqlWhere += string.Format(" and {0}={1}", propName, propValue);
                    }

                }

                // 查询语句
                string querySql = string.Format("delete from {0} where 1=1 {1}", tableName, sqlWhere);

                updateRows = ExecuteNonQuery(querySql);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return updateRows;
        }
    }
}
