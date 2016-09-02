using System;

namespace Vic.Data
{
    /// <summary>
    /// 表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class Table : Attribute
    {
        private string _name;

        /// <summary>
        /// 定义实体与数据库表的映射关系
        /// </summary>
        /// <param name="name">表名</param>
        public Table(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }
    }

    /// <summary>
    /// 字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class Field : Attribute
    {
        private string _name;

        /// <summary>
        /// 指定该属性绑定的字段名
        /// </summary>
        /// <param name="name">名称</param>
        public Field(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }
    }

    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Primarykey : Attribute
    {
        private bool _isPrimarykey = false;

        /// <summary>
        /// 指定是否主键
        /// </summary>
        public Primarykey()
        {
            this._isPrimarykey = true;
        }

        /// <summary>
        /// 返回一个布尔值，标示是否为主键。
        /// </summary>
        public bool IsPrimarykey
        {
            get { return this._isPrimarykey; }
        }
    }

    /// <summary>
    /// 标识(自增)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Identity : Attribute
    {
        private bool _isIdentity = false;

        /// <summary>
        /// 指定是否为标识
        /// </summary>
        public Identity()
        {
            this._isIdentity = true;
        }

        /// <summary>
        /// 返回一个布尔值，标示是否为标识。
        /// </summary>
        public bool IsIdentity
        {
            get { return this._isIdentity; }
        }
    }
}
