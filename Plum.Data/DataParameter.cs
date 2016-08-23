using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vic.Data
{
    /// <summary>
    /// 参数对象
    /// </summary>
    public class DataParameter
    {
        string _name;
        object _value;
        Type _type;

        public DataParameter()
        {
        }
        public DataParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public DataParameter(string name, object value, Type type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
        }
        public string Name { get { return this._name; } set { this._name = value; } }
        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                if (value != null)
                    this._type = value.GetType();
            }
        }
        public Type Type { get { return this._type; } set { this._type = value; } }
        public static DataParameter Create<T>(string name, T value)
        {
            var param = new DataParameter(name, value);
            if (value == null)
                param.Type = typeof(T);
            return param;
        }
        public static DataParameter Create(string name, object value)
        {
            return new DataParameter(name, value);
        }
        public static DataParameter Create(string name, object value, Type type)
        {
            return new DataParameter(name, value, type);
        }
    }
}