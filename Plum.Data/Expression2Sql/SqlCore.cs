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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Vic.Data;

namespace Expression2Sql
{
    [SuppressMessage("Rule Category", "CS1591")]
    public class SqlCore<T>
    {
        private SqlPack _sqlPack = new SqlPack();

        public string SqlStr { get { return this._sqlPack.ToString(); } }
        public Dictionary<string, object> DbParams { get { return this._sqlPack.DbParams; } }

        public SqlCore(DatabaseType dbType)
        {
            this._sqlPack.DatabaseType = dbType;
        }

        public void Clear()
        {
            this._sqlPack.Clear();
        }

        private string SelectParser(params Type[] ary)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = false;

            foreach (var item in ary)
            {
                //string tableName = item.Name;
                string tableName = item.GetEntityTableName();
                this._sqlPack.SetTableAlias(tableName);
            }

            //return "select {0} from " + typeof(T).Name + " " + this._sqlPack.GetTableAlias(typeof(T).Name);
            return "select {0} from " + typeof(T).GetEntityTableName() + " " + this._sqlPack.GetTableAlias(typeof(T).GetEntityTableName());
        }
        public SqlCore<T> Select(Expression<Func<T, object>> expression = null)
        {
            string sql = SelectParser(typeof(T));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2>(Expression<Func<T, T2, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3>(Expression<Func<T, T2, T3, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4, T5, T6, T7>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }
        public SqlCore<T> Select<T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression = null)
        {
            string sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));

            if (expression == null)
            {
                this._sqlPack.Sql.AppendFormat(sql, "*");
            }
            else
            {
                SqlProvider.Select(expression.Body, this._sqlPack);
                this._sqlPack.Sql.AppendFormat(sql, this._sqlPack.SelectFieldsStr);
            }

            return this;
        }

        private SqlCore<T> JoinParser<T2>(Expression<Func<T, T2, bool>> expression, string leftOrRightJoin = "")
        {
            //string joinTableName = typeof(T2).Name;
            string joinTableName = typeof(T2).GetEntityTableName();
            this._sqlPack.SetTableAlias(joinTableName);
            this._sqlPack.Sql.AppendFormat(" {0} join {1} on", leftOrRightJoin, joinTableName + " " + this._sqlPack.GetTableAlias(joinTableName));
            SqlProvider.Join(expression.Body, this._sqlPack);
            return this;
        }
        private SqlCore<T> JoinParser2<T2, T3>(Expression<Func<T2, T3, bool>> expression, string leftOrRightJoin = "")
        {
            //string joinTableName = typeof(T3).Name;
            string joinTableName = typeof(T3).GetEntityTableName();
            this._sqlPack.SetTableAlias(joinTableName);
            this._sqlPack.Sql.AppendFormat(" {0} join {1} on", leftOrRightJoin, joinTableName + " " + this._sqlPack.GetTableAlias(joinTableName));
            SqlProvider.Join(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Join<T2>(Expression<Func<T, T2, bool>> expression)
        {
            return JoinParser(expression);
        }
        public SqlCore<T> Join<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            return JoinParser2(expression);
        }

        public SqlCore<T> InnerJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            return JoinParser(expression, "inner ");
        }
        public SqlCore<T> InnerJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            return JoinParser2(expression, "inner ");
        }

        public SqlCore<T> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            return JoinParser(expression, "left ");
        }
        public SqlCore<T> LeftJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            return JoinParser2(expression, "left ");
        }

        public SqlCore<T> RightJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            return JoinParser(expression, "right ");
        }
        public SqlCore<T> RightJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            return JoinParser2(expression, "right ");
        }

        public SqlCore<T> FullJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            return JoinParser(expression, "full ");
        }
        public SqlCore<T> FullJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            return JoinParser2(expression, "full ");
        }

        public SqlCore<T> Where(Expression<Func<T, bool>> expression)
        {
            this._sqlPack += " where";
            SqlProvider.Where(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> GroupBy(Expression<Func<T, object>> expression)
        {
            this._sqlPack += " group by ";
            SqlProvider.GroupBy(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> OrderBy(Expression<Func<T, object>> expression)
        {
            this._sqlPack += " order by ";
            SqlProvider.OrderBy(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Max(Expression<Func<T, object>> expression)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            SqlProvider.Max(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Min(Expression<Func<T, object>> expression)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            SqlProvider.Min(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Avg(Expression<Func<T, object>> expression)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            SqlProvider.Avg(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Count(Expression<Func<T, object>> expression = null)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            if (expression == null)
            {
                //this._sqlPack.Sql.AppendFormat("select count(*) from {0}", typeof(T).Name);
                this._sqlPack.Sql.AppendFormat("select count(*) from {0}", typeof(T).GetEntityTableName());
            }
            else
            {
                SqlProvider.Count(expression.Body, this._sqlPack);
            }

            return this;
        }

        public SqlCore<T> Sum(Expression<Func<T, object>> expression)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            SqlProvider.Sum(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Delete()
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            //string tableName = typeof(T).Name;
            string tableName = typeof(T).GetEntityTableName();
            this._sqlPack.SetTableAlias(tableName);
            this._sqlPack += "delete " + tableName;
            return this;
        }

        public SqlCore<T> Update(Expression<Func<object>> expression = null)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            //this._sqlPack += "update " + typeof(T).Name + " set ";
            this._sqlPack += "update " + typeof(T).GetEntityTableName() + " set ";
            SqlProvider.Update(expression.Body, this._sqlPack);
            return this;
        }

        public SqlCore<T> Insert(Expression<Func<object>> expression)
        {
            this._sqlPack.Clear();
            this._sqlPack.IsSingleTable = true;
            //this._sqlPack += "insert into " + typeof(T).GetEntityTableName();
            this._sqlPack += "insert into " + typeof(T).GetEntityTableName();
            SqlProvider.Insert(expression.Body, this._sqlPack);
            return this;
        }
    }
}
