using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vic.Data
{
    /// <summary>
    /// 实体
    /// </summary>
    [Serializable]
    internal class EntityDescribeInfo
    {
        private string _tableName;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }

        private string[] _primarykey;
        /// <summary>
        /// 主键字段
        /// </summary>
        public string[] Primarykey
        {
            get { return this._primarykey; }
            set { this._primarykey = value; }
        }

        private string _identity;
        /// <summary>
        /// 标识（自增）字段
        /// </summary>
        public string Identity
        {
            get { return this._identity; }
            set { this._identity = value; }
        }
    }
}
