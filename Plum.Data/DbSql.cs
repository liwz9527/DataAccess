using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vic.Data
{
    /// <summary>
    /// SQL字符串带参数
    /// </summary>
    [Serializable]
    public struct DbSQL
    {
        /// <summary>
        /// SQL字符串
        /// </summary>
        public string SQLString;

        /// <summary>
        /// SQL字符串中对应的 DbParameter 参数
        /// </summary>
        public System.Data.Common.DbParameter[] DbParameters;

        /// <summary>
        /// 带DbParameters参数的SQL实例
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="dbParameters"></param>
        public DbSQL(string sqlString, params System.Data.Common.DbParameter[] dbParameters)
        {
            this.SQLString = sqlString;
            this.DbParameters = dbParameters;
        }
    }
}
