using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Vic.Data
{
    public class ExpressionBuilder
    {
        /// <summary>
        /// 根据Entity实体生成MemberInitExpression
        /// </summary>
        /// <param name="entity">实体<param>
        /// <returns></returns>
        public static MemberInitExpression GenMemberInitExpression(object entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] propertys = type.GetProperties();
            NewExpression newExpression = Expression.New(type);
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (PropertyInfo p in propertys)
            {
                if (p.MemberType == MemberTypes.Property)
                {
                    MemberInfo member = (MemberInfo)p;
                    MemberBinding memberBinding = Expression.Bind(member, Expression.Constant(p.GetValue(entity, null)));
                    memberBindings.Add(memberBinding);
                }
            }
            MemberInitExpression expression = Expression.MemberInit(newExpression, memberBindings.ToArray());
            return expression;
        }
    }
}
