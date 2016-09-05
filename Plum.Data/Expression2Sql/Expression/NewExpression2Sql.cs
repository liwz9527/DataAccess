#region License

#endregion

using System.Collections.Generic;
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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Vic.Data;

namespace Expression2Sql
{
    [SuppressMessage("Rule Category", "CS1591")]
    class NewExpression2Sql : BaseExpression2Sql<NewExpression>
	{
        protected override SqlPack Insert(NewExpression expression, SqlPack sqlPack)
        {
            List<string> fields = new List<string>();
            List<object> values = new List<object>();
            for (int i = 0; i < expression.Members.Count; i++)
            {
                MemberInfo m = expression.Members[i];
                ConstantExpression c = expression.Arguments[i] as ConstantExpression;
                fields.Add(m.GetEntityFieldName());
                values.Add(c.Value);
            }

            sqlPack += "(" + string.Join(",", fields) + ")";
            sqlPack += " values (";
            foreach (object value in values)
            {
                sqlPack.AddDbParameter(values);
                sqlPack += ",";
            }
            if (sqlPack[sqlPack.Length - 1] == ',')
            {
                sqlPack.Sql.Remove(sqlPack.Length - 1, 1);
            }
            sqlPack += ")";
            return sqlPack;
        }

        protected override SqlPack Update(NewExpression expression, SqlPack sqlPack)
		{
			for (int i = 0; i < expression.Members.Count; i++)
			{
				MemberInfo m = expression.Members[i];
				ConstantExpression c = expression.Arguments[i] as ConstantExpression;
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

		protected override SqlPack Select(NewExpression expression, SqlPack sqlPack)
		{
			foreach (Expression item in expression.Arguments)
			{
				SqlProvider.Select(item, sqlPack);
			}
			return sqlPack;
		}

		protected override SqlPack GroupBy(NewExpression expression, SqlPack sqlPack)
		{
			foreach (Expression item in expression.Arguments)
			{
				SqlProvider.GroupBy(item, sqlPack);
			}
			return sqlPack;
		}

		protected override SqlPack OrderBy(NewExpression expression, SqlPack sqlPack)
		{
			foreach (Expression item in expression.Arguments)
			{
				SqlProvider.OrderBy(item, sqlPack);
			}
			return sqlPack;
		}
	}
}
