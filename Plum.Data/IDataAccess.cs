using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Vic.Data
{
    /// <summary>
    /// 通用数据库访问接口类
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// 数据库链接串
        /// </summary>
        string ConnectionString
        {
            get;
            //set;
        }

        /// <summary>
        /// 数据库驱动名称
        /// </summary>
        string DbProviderName
        {
            get;
            //set;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        DbProviderType DbProviderType
        {
            get;
        }

        /// <summary>
        /// 包含有关实现 System.Data.Common.DbProviderFactory 的所有已安装提供程序的信息
        /// </summary>
        DataTable FactoryClasses
        {
            get;
        }

        /// <summary>
        /// 资源是否已被释放过
        /// </summary>
        bool IsDisposed
        {
            get;
        }

        /// <summary>
        /// 获取一个数据工厂实例
        /// </summary>
        /// <returns></returns>
        DbProviderFactory GetDbProviderFactory();

        /// <summary>
        /// 创建一个DbConnection实例
        /// </summary>
        /// <returns></returns>
        DbConnection CreateConnection();

        /// <summary>
        /// 创建一个DbConnection对象的DbCommand实例
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        DbCommand CreateCommand(DbConnection dbConnection);

        /// <summary>
        /// 创建一个DbParameter实例
        /// </summary>
        /// <returns></returns>
        DbParameter CreateParameter();

        /// <summary>
        /// 创建一个DbDataAdapter实例
        /// </summary>
        /// <returns></returns>
        DbDataAdapter CreateDataAdapter();

        /// <summary>
        /// 创建一个CreateCommandBuilder实例
        /// </summary>
        /// <returns></returns>
        DbCommandBuilder CreateCommandBuilder();

        /// <summary>
        /// 检测数据库链接。
        /// </summary>
        /// <returns></returns>
        bool CheckConn();

        /// <summary>
        /// 检测数据库链接。
        /// </summary>
        /// <param name="exMessage">异常错误信息</param>
        /// <returns></returns>
        bool CheckConn(out string exMessage);

        /// <summary>
        /// 获取最后一次执行带DbParameter参数的方法的一个参数值
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns>object</returns>
        object GetParamValue(string parameterName);

        /// <summary>
        /// 获取最后一次执行带DbParameter参数的方法的一个参数值
        /// </summary>
        /// <param name="index">参数索引,从0开始</param>
        /// <returns>object</returns>
        object GetParamValue(int index);

        /// <summary>     
        /// 执行无返回数据集的SQL，返回受影响的行数。     
        /// </summary>     
        /// <param name="sql">SQL语句</param>
        /// <returns>Int</returns>  
        int ExecuteNonQuery(string sql);

        /// <summary>
        /// 执行无返回数据集的SQL，返回受影响的行数。 
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>Int</returns>
        int ExecuteNonQuery(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 执行无返回数据集的SQL，返回受影响的行数。 
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>Int</returns>
        int ExecuteNonQuery(string sql, IList<DbParameter> parameters);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <returns></returns>
        int ExecuteSqlTran(params string[] sqls);

        /// <summary>
        /// 执行多条SQL语句(带 DbParameter 参数)，实现数据库事务。
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <returns></returns>
        int ExecuteSqlTran(params DbSQL[] sqls);

        /// <summary>
        /// 执行多条SQL语句(带 DbParameter 参数)，实现数据库事务。
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <returns></returns>
        int ExecuteSqlTran(IList<DbSQL> sqls);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="commits">指定执行多少条SQL后提交一次，小于或等于0为不指定即执行所有SQL后再提交。</param>
        /// <param name="sqls">SQL语句</param>
        /// <returns></returns>
        int ExecuteSqlTran(int commits, params string[] sqls);

        /// <summary>
        /// 执行多条SQL语句(带 DbParameter 参数)，实现数据库事务。
        /// </summary>
        /// <param name="commits">指定执行多少条SQL后提交一次，小于或等于0为不指定即执行所有SQL后再提交。</param>
        /// <param name="sqls">SQL语句</param>
        /// <returns></returns>
        int ExecuteSqlTran(int commits, params DbSQL[] sqls);

        /// <summary>
        /// 执行多条SQL语句(带 DbParameter 参数)，实现数据库事务。
        /// </summary>
        /// <param name="commits">指定执行多少条SQL后提交一次，小于或等于0为不指定即执行所有SQL后再提交。</param>
        /// <param name="sqls">SQL语句</param>
        /// <returns></returns>
        int ExecuteSqlTran(int commits, IList<DbSQL> sqls);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        DataSet Query(string sql);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataSet</returns>
        DataSet Query(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataSet</returns>
        DataSet Query(string sql, IList<DbParameter> parameters);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <returns>DataSet</returns>
        DataSet Query(params string[] sqls);

        /// <summary>
        /// 执行查询语句(带 DbParameter 参数)
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <returns></returns>
        DataSet Query(params DbSQL[] sqls);

        /// <summary>
        /// 执行查询语句(带 DbParameter 参数)
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <returns></returns>
        DataSet Query(IList<DbSQL> sqls);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataTable</returns>
        DataTable QueryTable(string sql);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryTable(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryTable(string sql, IList<DbParameter> parameters);

        /// <summary>
        /// 执行查询语句，返回DataReader，用完后要调用DbDataReader的Close()方法关闭实例
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DbDataReader</returns>
        DbDataReader QueryReader(string sql);

        /// <summary>
        /// 执行查询语句，返回DataReader，用完后要调用DbDataReader的Close()方法关闭实例
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DbDataReader</returns>
        DbDataReader QueryReader(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 执行查询语句，返回DataReader，用完后要调用DbDataReader的Close()方法关闭实例
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DbDataReader</returns>
        DbDataReader QueryReader(string sql, IList<DbParameter> parameters);

        /// <summary>
        /// 执行分页查询(DataReader方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage(string sql, int pageSize, int currPageIndex);

        /// <summary>
        /// 执行分页查询(DataReader方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage(string sql, int pageSize, int currPageIndex, params DbParameter[] parameters);

        /// <summary>
        /// 执行分页查询(DataReader方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage(string sql, int pageSize, int currPageIndex, IList<DbParameter> parameters);

        /// <summary>
        /// 执行分页查询(DataReader方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage(string sql, int pageSize, int currPageIndex, out int allRowsCount);

        /// <summary>
        /// 执行分页查询(DataReader方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage(string sql, int pageSize, int currPageIndex, out int allRowsCount, params DbParameter[] parameters);

        /// <summary>
        /// 执行分页查询(DataReader方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage(string sql, int pageSize, int currPageIndex, out int allRowsCount, IList<DbParameter> parameters);

        /// <summary>
        /// 执行分页查询(DataTable方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage2(string sql, int pageSize, int currPageIndex, out int allRowsCount);

        /// <summary>
        /// 执行分页查询(DataTable方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage2(string sql, int pageSize, int currPageIndex, out int allRowsCount, params DbParameter[] parameters);

        /// <summary>
        /// 执行分页查询(DataTable方式)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns>DataTable</returns>
        DataTable QueryPage2(string sql, int pageSize, int currPageIndex, out int allRowsCount, IList<DbParameter> parameters);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <returns></returns>
        void ExecProcedureNonQuery(string storedProcName);

        /// <summary>
        /// 执行存储过程,带参数
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程的 DbParameter 类型参数</param>
        /// <returns></returns>
        void ExecProcedureNonQuery(string storedProcName, params DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程,带参数
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程的 DbParameter 类型参数</param>
        /// <returns></returns>
        void ExecProcedureNonQuery(string storedProcName, IList<DbParameter> parameters);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <returns></returns>
        DataSet ExecProcedure(string storedProcName);

        /// <summary>
        /// 执行存储过程,带参数
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程的 DbParameter 类型参数</param>
        /// <returns></returns>
        DataSet ExecProcedure(string storedProcName, params DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程,带参数
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程的 DbParameter 类型参数</param>
        /// <returns></returns>
        DataSet ExecProcedure(string storedProcName, IList<DbParameter> parameters);

        /// <summary>
        /// 执行存储过程,并返回指定表数据集
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="tableNames">表名</param>
        /// <returns>DataSet</returns>
        DataSet ExecProcedure(string storedProcName, string[] tableNames);

        /// <summary>
        /// 执行存储过程(带 DbParameter 参数),并返回指定表数据集
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程的 DbParameter 类型参数</param>
        /// <param name="tableNames">表名</param>
        /// <returns>DataSet</returns>
        DataSet ExecProcedure(string storedProcName, DbParameter[] parameters, string[] tableNames);

        /// <summary>
        /// 执行存储过程(带 DbParameter 参数),并返回指定表数据集
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程的 DbParameter 类型参数</param>
        /// <param name="tableNames">表名</param>
        /// <returns>DataSet</returns>
        DataSet ExecProcedure(string storedProcName, IList<DbParameter> parameters, string[] tableNames);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="dataTable">DataTable，必须设置主键。
        /// ExtendedProperties 属性中需要包含 "SelectSqlForUpdate(用于 DbDataAdapter 调用 Update(DataTable) 方法的 SelectCommand 属性命令)"、
        /// "ScrTableName(源表名称)"、"BaseTable.n(由DbDataAdapter的Fill填充时自动生成的键值)三个特定键值中的其中一个，如没有设置则采用 TableName 属性值作为源表名称，
        /// 优先级 SelectSqlForUpdate > ScrTableName > BaseTable.n > TableName"，键值名称区分大小写。
        /// </param>
        /// <returns></returns>
        void Update(DataTable dataTable);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="dataTable">DataTable，必须设置主键。</param>
        /// <param name="sql">Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void Update(DataTable dataTable, string sql);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="dataTable">DataTable，必须设置主键。</param>
        /// <param name="sql">Table对应的SQL，必须包含主键列。</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns></returns>
        void Update(DataTable dataTable, string sql, params DbParameter[] parameters);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="dataTable">DataTable，必须设置主键。</param>
        /// <param name="sql">Table对应的SQL，必须包含主键列。</param>
        /// <param name="parameters">SQL语句的 DbParameter 类型参数</param>
        /// <returns></returns>
        void Update(DataTable dataTable, string sql, IList<DbParameter> parameters);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="dataSet">DataSet。
        /// 集合中的每个 DataTable 的 ExtendedProperties 属性中需要包含 "SelectSqlForUpdate(用于 DbDataAdapter 调用 Update(DataTable) 方法的 SelectCommand 属性命令)"、
        /// "ScrTableName(源表名称)"、"BaseTable.n(由DbDataAdapter的Fill填充时自动生成的键值)三个特定键值中的其中一个，如没有设置则采用 TableName 属性值作为源表名称，
        /// 优先级 SelectSqlForUpdate > ScrTableName > BaseTable.n > TableName"，键值名称区分大小写。
        /// </param>
        /// <returns></returns>
        void Update(DataSet dataSet);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="dataSet">DataSet，必须设置主键，多表时需要设置每个的TableName。</param>
        /// <param name="sqls">每个Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void Update(DataSet dataSet, params string[] sqls);

        /// <summary>
        /// 更新数据库(带 DbParameter 参数)
        /// </summary>
        /// <param name="dataSet">DataSet，必须设置主键，多表时需要设置每个的TableName。</param>
        /// <param name="sqls">每个Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void Update(DataSet dataSet, params DbSQL[] sqls);

        /// <summary>
        /// 更新数据库(带 DbParameter 参数)
        /// </summary>
        /// <param name="dataSet">DataSet，必须设置主键，多表时需要设置每个的TableName。</param>
        /// <param name="sqls">每个Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void Update(DataSet dataSet, IList<DbSQL> sqls);

        /// <summary>
        /// 更新数据库(事务)
        /// </summary>
        /// <param name="dataSet">DataSet。
        /// 集合中的每个 DataTable 的 ExtendedProperties 属性中需要包含 "SelectSqlForUpdate(用于 DbDataAdapter 调用 Update(DataTable) 方法的 SelectCommand 属性命令)"、
        /// "ScrTableName(源表名称)"、"BaseTable.n(由DbDataAdapter的Fill填充时自动生成的键值)三个特定键值中的其中一个，如没有设置则采用 TableName 属性值作为源表名称，
        /// 优先级 SelectSqlForUpdate > ScrTableName > BaseTable.n > TableName"，键值名称区分大小写。
        /// </param>
        /// <returns></returns>
        void UpdateTran(DataSet dataSet);

        /// <summary>
        /// 更新数据库(事务)
        /// </summary>
        /// <param name="dataSet">DataSet，必须设置主键，多表时需要设置每个的TableName。</param>
        /// <param name="sqls">每个Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void UpdateTran(DataSet dataSet, params string[] sqls);

        /// <summary>
        /// 更新数据库(事务,带 DbParameter 参数)
        /// </summary>
        /// <param name="dataSet">DataSet，必须设置主键，多表时需要设置每个的TableName。</param>
        /// <param name="sqls">每个Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void UpdateTran(DataSet dataSet, params DbSQL[] sqls);

        /// <summary>
        /// 更新数据库(事务,带 DbParameter 参数)
        /// </summary>
        /// <param name="dataSet">DataSet，必须设置主键，多表时需要设置每个的TableName。</param>
        /// <param name="sqls">每个Table对应的SQL，必须包含主键列。</param>
        /// <returns></returns>
        void UpdateTran(DataSet dataSet, IList<DbSQL> sqls);
    }
}
