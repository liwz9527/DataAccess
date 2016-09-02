using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vic.Data
{
    /// <summary>
    /// Entity自定义特性解析
    /// </summary>
    public static class EntityAttributeAnalyze
    {
        /// <summary>
        /// 获取实体对应的数据库表名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetEntityTableName(this Type type)
        {
            string tableName = "";
            object[] objs = type.GetCustomAttributes(typeof(Table), true);
            foreach (object obj in objs)
            {
                Table attr = obj as Table;
                if (attr != null)
                {
                    tableName = attr.Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = type.Name;
            }
            return tableName;
        }

        /// <summary>
        /// 获取实体属性对应的数据库字段名
        /// </summary>
        /// <param name="member">属性</param>
        /// <returns></returns>
        public static string GetEntityFieldName(this MemberInfo member)
        {
            string fieldName = "";
            object[] objs = member.GetCustomAttributes(typeof(Field), true);
            foreach (object obj in objs)
            {
                Field attr = obj as Field;
                if (attr != null)
                {
                    fieldName = attr.Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                fieldName = member.Name;
            }
            return fieldName;
        }

        /// <summary>
        /// 获取实体属性是否主键
        /// </summary>
        /// <param name="member">属性</param>
        /// <returns></returns>
        public static bool GetIsPrimarykey(this MemberInfo member)
        {
            bool isPrimarykey = false;
            object[] objs = member.GetCustomAttributes(typeof(Primarykey), true);
            foreach (object obj in objs)
            {
                Primarykey attr = obj as Primarykey;
                if (attr != null)
                {
                    isPrimarykey = attr.IsPrimarykey;
                    break;
                }
            }
            return isPrimarykey;
        }

        /// <summary>
        /// 获取实体属性是否为标识
        /// </summary>
        /// <param name="member">属性</param>
        /// <returns></returns>
        public static bool GetIsIdentity(this MemberInfo member)
        {
            bool isIdentity = false;
            object[] objs = member.GetCustomAttributes(typeof(Identity), true);
            foreach (object obj in objs)
            {
                Identity attr = obj as Identity;
                if (attr != null)
                {
                    isIdentity = attr.IsIdentity;
                    break;
                }
            }
            return isIdentity;
        }
    }
}
