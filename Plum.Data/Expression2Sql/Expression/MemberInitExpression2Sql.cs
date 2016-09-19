#region License

#endregion

using System;
/**
* Copyright (c) 2015, 何志祥 (strangecity@qq.com).
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* without warranties or conditions of any kind, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Vic.Data;

namespace Expression2Sql
{
    [SuppressMessage("Rule Category", "CS1591")]
    class MemberInitExpression2Sql : BaseExpression2Sql<MemberInitExpression>
	{
        protected override SqlPack Insert(MemberInitExpression expression, SqlPack sqlPack)
        {
            List<string> fields = new List<string>();
            List<object> values = new List<object>();
            for (int i = 0; i < expression.Bindings.Count; i++)
            {
                MemberInfo m = expression.Bindings[i].Member;
                MemberAssignment memberAssignment = expression.Bindings[i] as MemberAssignment;
                ConstantExpression c = memberAssignment.Expression as ConstantExpression;
                fields.Add(m.GetEntityFieldName());
                values.Add(c.Value);
            }

            sqlPack += "(" + string.Join(",", fields) + ")";
            sqlPack += " values (";
            foreach(object value in values)
            {
                sqlPack.AddDbParameter(value);
                sqlPack += ",";
            }
            if (sqlPack[sqlPack.Length - 1] == ',')
            {
                sqlPack.Sql.Remove(sqlPack.Length - 1, 1);
            }
            sqlPack += ")";
            return sqlPack;
        }

        protected override SqlPack Update(MemberInitExpression expression, SqlPack sqlPack)
        {
            for (int i = 0; i < expression.Bindings.Count; i++)
            {
                MemberInfo m = expression.Bindings[i].Member;
                MemberAssignment memberAssignment = expression.Bindings[i] as MemberAssignment;
                ConstantExpression c = memberAssignment.Expression as ConstantExpression;
                //sqlPack += m.Name + " =";
                sqlPack += m.GetEntityFieldName() + " =";
                sqlPack.AddDbParameter(c.Value);
                sqlPack += ",";
            }
           
            if (sqlPack[sqlPack.Length - 1] == ',')
            {
                sqlPack.Sql.Remove(sqlPack.Length - 1, 1);
            }
            return sqlPack;
        }
    }
}