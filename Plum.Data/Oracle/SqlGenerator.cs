using System;
using System.Collections.Generic; 
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Vic.Data.Oracle
{
    /// <summary>
    /// 拉姆达表达式解析类
    /// </summary>
    public class SqlGenerator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sameIndex"></param>
        public SqlGenerator(int sameIndex = 1)
        {
            this.SameIndex = sameIndex;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SqlWhere = null;
        public List<DataParameter> Paras = new List<DataParameter>();
        private int SameIndex = 1;

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="re"></param>
        /// <param name="exp"></param>
        public void Expression(SqlGenerator re, Expression exp)
        {
            SqlGenerator.MemberType type = SqlGenerator.MemberType.None;
            var expStr = exp.ToString();
            var isNotBool = !expStr.Contains("True") && !expStr.Contains("False");
            var isContainsNot = expStr.Contains("Not");
            if (isContainsNot && Regex.Match(expStr,@" => Not\(.+?\)").Groups.Count>0 && !expStr.Contains("Contains"))
            {
                this.SqlWhere = string.Format(" AND {0}=0 or {0} is null ", Regex.Match(expStr, @" => Not\(.+\.(.+?)\)").Groups[1].Value);
            }
            else if (isNotBool)
            {
                this.SqlWhere = string.Format(" AND {0} ", re.CreateSqlElements(exp, ref type));
            }
            else
            {
                var isTrue = Regex.IsMatch(expStr, @"\=\> True$");
                var isFalse = Regex.IsMatch(expStr, @"\=\> False$");
                if (isFalse)
                {
                    this.SqlWhere = string.Format(" AND 1<>1 ");
                }
                else if (isTrue)
                {

                }
                else
                {
                    this.SqlWhere = string.Format(" AND {0} ", re.CreateSqlElements(exp, ref type));
                }
            }
        }

        /// <summary>
        /// 生成SQL
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GenerateSql(Expression exp)
        {
            string sqlWhere = string.Empty;

            SqlGenerator.MemberType type = SqlGenerator.MemberType.None;
            var expStr = exp.ToString();
            var isNotBool = !expStr.Contains("True") && !expStr.Contains("False");
            var isContainsNot = expStr.Contains("Not");
            if (isContainsNot && Regex.Match(expStr, @" => Not\(.+?\)").Groups.Count > 0 && !expStr.Contains("Contains"))
            {
                this.SqlWhere = string.Format(" AND {0}=0 or {0} is null ", Regex.Match(expStr, @" => Not\(.+\.(.+?)\)").Groups[1].Value);
            }
            else
            {
                sqlWhere = " AND " + CreateSqlElements(exp, ref type);
            }

            return sqlWhere;
        }

        /// <summary>
        /// 递归解析表达式路由计算
        /// </summary>
        /// <returns></returns>
        private string CreateSqlElements(Expression exp, ref MemberType type, bool isTure = true)
        {
            if (exp is LambdaExpression)
            {
                LambdaExpression lambda = exp as LambdaExpression;
                var expression = lambda.Body;
                MemberType EleType = MemberType.None;
                return CreateSqlElements(expression, ref EleType);
            }
            else if (exp is BinaryExpression)
            {
                var expression = exp as BinaryExpression;
                MemberType leftType = MemberType.None;
                MemberType rightType = MemberType.None;

                var left = CreateSqlElements(expression.Left, ref leftType);
                var right = CreateSqlElements(expression.Right, ref rightType);
                var oper = GetOperator(expression.NodeType);
                var isKeyOperValue = leftType == MemberType.Key && rightType == MemberType.Value;
                var isValueOperKey = rightType == MemberType.Key && leftType == MemberType.Value;
                #region 处理 null
                if (isKeyOperValue & (right == "null" || right == null) && oper.Trim() == "=")
                {
                    var oldLeft = AddParas(ref left, right);
                    return string.Format(" ({0} is null ) ", oldLeft);
                }
                else if (isKeyOperValue & (right == "null" || right == null) && oper.Trim() == "<>")
                {
                    var oldLeft = AddParas(ref left, right);
                    return string.Format(" ({0} is not null ) ", oldLeft);
                }
                else if (isValueOperKey & (left == "null" || left == null) && oper.Trim() == "=")
                {
                    return string.Format(" ({0} is null ) ", right);
                }
                else if (isValueOperKey & (left == "null" || left == null) && oper.Trim() == "<>")
                {
                    return string.Format(" ({0} is not null ) ", right);
                }
                #endregion
                else if (isKeyOperValue)
                {
                    var oldLeft = AddParas(ref left, right);
                    return string.Format(" {0} {1} {2} ", oldLeft, oper, right);
                }
                else if (isValueOperKey)
                {
                    var oldRight = AddParasReturnRight(left, ref  right);
                    return string.Format("( :{0} {1} {2} )", right, oper, oldRight);
                }
                else if (leftType == MemberType.Value && rightType == MemberType.Value)
                {
                    return string.Format("( '{0}' {1} '{2}' )", left, oper, right);
                }
                else
                {
                    return string.Format("( {0} {1} {2} )", left, oper, right);
                }

            }
            else if (exp is BlockExpression)
            {

            }
            else if (exp is ConditionalExpression)
            {

            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression mce = (MethodCallExpression)exp;
                string methodName = mce.Method.Name;
                if (methodName == "Contains")
                {
                    return Contains(methodName, mce, isTure);
                }
                else if (methodName == "StartsWith")
                {
                    return StartsWith(methodName, mce, isTure);
                }
                else if (methodName == "EndWith")
                {
                    return EndWith(methodName, mce, isTure);
                }
                else if (methodName == "ToString")
                {
                    type = MemberType.Value;
                    return MethodToString(methodName, mce, ref type);
                }
                else if (methodName.StartsWith("To"))
                {
                    type = MemberType.Value;
                    return MethodTo(methodName, mce, ref type);
                }
                else if (methodName.StartsWith("Parse"))
                {
                    type = MemberType.Value;
                    return MethodTo(methodName, mce, ref type);

                }

            }
            else if (exp is ConstantExpression)
            {
                type = MemberType.Value;
                ConstantExpression ce = ((ConstantExpression)exp);
                if (ce.Value == null)
                    return "null";
                else
                {
                    return ce.Value.ToString();
                }
            }
            else if (exp is MemberExpression)
            {
                MemberExpression me = ((MemberExpression)exp);
                if (me.Expression == null || me.Expression.NodeType.ToString() != "Parameter")
                {
                    type = MemberType.Value;
                    object dynInv = null;
                    try
                    {
                        // var dynInv = Expression.Lambda(exp).Compile().DynamicInvoke();原始写法性能极慢，下面写法性能提高了几十倍
                        // var dynInv= Expression.Lambda(me.Expression as ConstantExpression).Compile().DynamicInvoke();
                        var conExp = me.Expression as ConstantExpression;
                        if (conExp != null)
                        {
                            dynInv = (me.Member as System.Reflection.FieldInfo).GetValue((me.Expression as ConstantExpression).Value);
                        }
                        else
                        {

                            var memberInfos = new Stack<MemberInfo>();

                            // "descend" toward's the root object reference:
                            while (exp is MemberExpression)
                            {
                                var memberExpr = exp as MemberExpression;
                                memberInfos.Push(memberExpr.Member);
                                exp = memberExpr.Expression;
                            }

                            // fetch the root object reference:
                            var constExpr = exp as ConstantExpression;
                            var objReference = constExpr.Value;

                            // "ascend" back whence we came from and resolve object references along the way:
                            while (memberInfos.Count > 0)  // or some other break condition
                            {
                                var mi = memberInfos.Pop();
                                if (mi.MemberType == MemberTypes.Property)
                                {
                                    objReference = objReference.GetType()
                                                               .GetProperty(mi.Name)
                                                               .GetValue(objReference, null);
                                }
                                else if (mi.MemberType == MemberTypes.Field)
                                {
                                    objReference = objReference.GetType()
                                                               .GetField(mi.Name)
                                                               .GetValue(objReference);
                                }
                            }
                            dynInv = objReference;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (me.ToString() == "DateTime.Now")
                        {
                            return DateTime.Now.ToString();
                        }
                        throw new Exception("拉姆达解析发生异常，错误信息:{0} \r\n message:"+ ex.Message,ex);
                    }

                    if (dynInv == null) return null;
                    else
                        return dynInv.ToString();
                }
                else
                {
                    string name = me.Member.Name;
                    type = MemberType.Key;
                    return name;
                }
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);
                var mex = ue.Operand;
                return CreateSqlElements(mex, ref type, false);
            }
            return null;
        }

        private string MethodTo(string methodName, MethodCallExpression mce, ref MemberType type)
        {
            var value = CreateSqlElements(mce.Arguments[0], ref type);
            if (methodName == "ToDateTime" || mce.Type==typeof(DateTime))
            {
                //return Convert.ToDateTime(value).ToString();

                return string.Format("to_date('{0}','yyyy-mm-dd hh24:mi:ss')", value);
            }
            else if (methodName.StartsWith("ToInt"))
            {
                return Convert.ToInt32(value).ToString();
            }
            return value;
        }

        private string MethodToString(string methodName, MethodCallExpression mce, ref MemberType type)
        {
            return CreateSqlElements(mce.Object, ref type);
        }


        private string AddParas(ref string left, string right)
        {
            string oldLeft = left;
            left = left + SameIndex;
            SameIndex++;
            if (right == null)
            {
                this.Paras.Add(new DataParameter(":" + left, DBNull.Value));
            }
            else
            {
                this.Paras.Add(new DataParameter(":" + left, right));
            }
            return oldLeft;
        }
        private string AddParasReturnRight(string left, ref string right)
        {
            string oldRight = right;
            right = right + SameIndex;
            SameIndex++;
            if (left == null)
            {
                this.Paras.Add(new DataParameter(":" + right, DBNull.Value));
            }
            else
            {
                this.Paras.Add(new DataParameter(":" + right, left));
            }
            return oldRight;
        }

        private string StartsWith(string methodName, MethodCallExpression mce, bool isTure)
        {
            MemberType leftType = MemberType.None;
            MemberType rightType = MemberType.None;
            var left = CreateSqlElements(mce.Object, ref leftType);
            var right = CreateSqlElements(mce.Arguments[0], ref rightType);
            var oldLeft = AddParas(ref left, right);
            return string.Format("({0} {1} LIKE :{2}+'%')", oldLeft, isTure == false ? "  NOT " : null, left);
        }
        private string EndWith(string methodName, MethodCallExpression mce, bool isTure)
        {
            MemberType leftType = MemberType.None;
            MemberType rightType = MemberType.None;
            var left = CreateSqlElements(mce.Object, ref leftType);
            var right = CreateSqlElements(mce.Arguments[0], ref rightType);
            var oldLeft = AddParas(ref left, right);
            return string.Format("({0} {1} LIKE '%'+:{2})", oldLeft, isTure == false ? "  NOT " : null, left);
        }

        private string Contains(string methodName, MethodCallExpression mce, bool isTure)
        {
            MemberType leftType = MemberType.None;
            MemberType rightType = MemberType.None;
            var left = CreateSqlElements(mce.Object, ref leftType);
            var right = CreateSqlElements(mce.Arguments[0], ref rightType);
            //var oldLeft = AddParas(ref left, right);
            //return string.Format("({0} {1} LIKE '%'+:{2}+'%')", oldLeft, isTure == false ? "  NOT " : null, left);
            return string.Format("({0} {1} LIKE '%{2}%')", left, isTure == false ? "  NOT " : null, right);
        }


        /// <summary>
        /// 根据条件生成对应的sql查询操作符
        /// </summary>
        /// <param name="expressiontype"></param>
        /// <returns></returns>
        private string GetOperator(ExpressionType expressiontype)
        {
            switch (expressiontype)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " =";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " OR ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    throw new Exception(string.Format("不支持{0}此种运算符查找！" + expressiontype));
            }
        }

        public enum MemberType
        {
            None = 0,
            Key = 1,
            Value = 2
        }
    }
}
