using Expression2Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Vic.Data
{
    public partial class DataAccess
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public Expression2Sql.DatabaseType DatabaseType
        {
            get
            {
                switch (DbProviderType)
                {
                    case DbProviderType.SqlServer:
                    case DbProviderType.SqlServerCe_3_5:
                        return Expression2Sql.DatabaseType.SQLServer;
                    case DbProviderType.Oracle:
                    case DbProviderType.OracleClient:
                    case DbProviderType.OracleManaged:
                        return Expression2Sql.DatabaseType.Oracle;
                    case DbProviderType.SQLite:
                    case DbProviderType.SQLiteEF6:
                    case DbProviderType.SQLiteLinq:
                        return Expression2Sql.DatabaseType.SQLite;
                    case DbProviderType.MySql:
                        return Expression2Sql.DatabaseType.MySQL;
                    default:
                        throw new Exception(string.Format("{0}的扩展方法暂不支持数据库类型\"{1}\".", this.GetType().Name, DbProviderType.ToString()));
                }
            }
        }

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
        /// 向T对应的数据库表中增加一条记录
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="expression">列值</param>
        /// <returns></returns>
        public int Add<T>(Expression<Func<object>> expression) where T : class, new()
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression参数不能为空！");
            }

            int result = 0;

            try
            {
                Expre2Sql.Init(DatabaseType);
                SqlCore<T> sql = Expre2Sql.Insert<T>(expression);
                string sqlStr = sql.SqlStr;
                List<DbParameter> paras = GetDbParameters(sql.DbParams);
                result = ExecuteNonQuery(sqlStr, paras);
            }
            catch (Exception ex)
            {
                throw new Exception("Add方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return result;
        }

        public void Add<T>(List<T> listEntity)
        {
            if (listEntity == null)
            {
                throw new ArgumentNullException("listEntity参数不能为空！");
            }

            try
            {
                Expre2Sql.Init(DatabaseType);
                List<DbSQL> sqls = new List<DbSQL>();
                foreach (T t in listEntity)
                {
                    SqlCore<T> sql = Expre2Sql.Insert<T>(() => t);
                    string sqlStr = sql.SqlStr;
                    List<DbParameter> paras = GetDbParameters(sql.DbParams);
                    DbSQL dbSql = new DbSQL(sqlStr, paras.ToArray());
                    sqls.Add(dbSql);
                }
                ExecuteSqlTran(0, sqls);
            }
            catch (Exception ex)
            {
                throw new Exception("Add方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除T对应表的全部数据
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <returns></returns>
        public int Delete<T>() where T : class, new()
        {
            int result = 0;

            try
            {
                Expre2Sql.Init(DatabaseType);
                string sqlStr = Expre2Sql.Delete<T>().SqlStr;
                result = ExecuteNonQuery(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// 根据条件删除T对应表的数据
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate参数不能为空！");
            }

            int result = 0;

            try
            {
                Expre2Sql.Init(DatabaseType);
                SqlCore<T> sql = Expre2Sql.Delete<T>().Where(predicate);
                string sqlStr = sql.SqlStr;
                List<DbParameter> paras = GetDbParameters(sql.DbParams);
                result = ExecuteNonQuery(sqlStr, paras);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return result;
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
        public int Delete(dynamic entity, string tableName)
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

        /// <summary>
        /// 更新T对应表的全部数据
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="expression">列值</param>
        /// <returns></returns>
        public int Update<T>(Expression<Func<object>> expression) where T : class, new()
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression参数不能为空！");
            }

            int result = 0;

            try
            {
                Expre2Sql.Init(DatabaseType);
                SqlCore<T> sql = Expre2Sql.Update<T>(expression);
                string sqlStr = sql.SqlStr;
                List<DbParameter> paras = GetDbParameters(sql.DbParams);
                result = ExecuteNonQuery(sqlStr, paras);
            }
            catch (Exception ex)
            {
                throw new Exception("Update方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// 根据条件更新T对应表的数据
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="expression">列值</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public int Update<T>(Expression<Func<object>> expression, Expression<Func<T, bool>> predicate) where T : class, new()
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression参数不能为空！");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate参数不能为空！");
            }

            int result = 0;

            try
            {
                Expre2Sql.Init(DatabaseType);
                SqlCore<T> sql = Expre2Sql.Update<T>(expression).Where(predicate);
                string sqlStr = sql.SqlStr;
                List<DbParameter> paras = GetDbParameters(sql.DbParams);
                result = ExecuteNonQuery(sqlStr, paras);
            }
            catch (Exception ex)
            {
                throw new Exception("Update方法执行错误!" + Environment.NewLine + ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="dbParams">参数集合</param>
        /// <returns></returns>
        private List<DbParameter> GetDbParameters(Dictionary<string, object> dbParams)
        {
            List<DbParameter> paras = new List<DbParameter>();
            foreach (KeyValuePair<string, object> item in dbParams)
            {
                DbParameter dbParam = CreateParameter();
                dbParam.ParameterName = item.Key;
                dbParam.Value = item.Value;
                paras.Add(dbParam);
            }
            return paras;
        }
    }
}
