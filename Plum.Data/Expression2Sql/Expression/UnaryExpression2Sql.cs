#region License
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
#endregion

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Expression2Sql
{
    [SuppressMessage("Rule Category", "CS1591")]
    class UnaryExpression2Sql : BaseExpression2Sql<UnaryExpression>
	{
		protected override SqlPack Select(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Select(expression.Operand, sqlPack);
			return sqlPack;
		}

        protected override SqlPack Insert(UnaryExpression expression, SqlPack sqlPack)
        {
            SqlProvider.Insert(expression.Operand, sqlPack);
            return sqlPack;
        }

        protected override SqlPack Where(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Where(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack GroupBy(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.GroupBy(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack OrderBy(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.OrderBy(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack Max(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Max(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack Min(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Min(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack Avg(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Avg(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack Count(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Count(expression.Operand, sqlPack);
			return sqlPack;
		}

		protected override SqlPack Sum(UnaryExpression expression, SqlPack sqlPack)
		{
			SqlProvider.Sum(expression.Operand, sqlPack);
			return sqlPack;
		}
	}
}
