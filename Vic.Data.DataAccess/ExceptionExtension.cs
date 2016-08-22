using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Vic.Data
{
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public class ProviderTypeNoneException : Exception
    {
        private string dbProviderName = "";

        //public ProviderTypeNoneException()
        //    : base()//调用基类的构造器
        //{
        //}

        //public ProviderTypeNoneException(string message)
        //    : base(message)//调用基类的构造器
        //{
        //}

        //public ProviderTypeNoneException(string message, Exception innerException)
        //    : base(message, innerException)//调用基类的构造器
        //{
        //}

        public ProviderTypeNoneException(string dbProviderName)
            : base()//调用基类的构造器
        {
            this.dbProviderName = dbProviderName;
        }

        public ProviderTypeNoneException(string dbProviderName, string message)
            : base(message)//调用基类的构造器
        {
            this.dbProviderName = dbProviderName;
        }


        public ProviderTypeNoneException(string dbProviderName, string message, Exception innerException)
            : base(message, innerException)//调用基类的构造器
        {
            this.dbProviderName = dbProviderName;
        }

        public override string Message
        {
            get
            {
                StringBuilder explain = new StringBuilder();
                explain.AppendFormat(@"原因：""{0}""没有找到相关驱动DLL或版本错误。" + Environment.NewLine, dbProviderName);
                explain.AppendFormat(@"解决方法：以Oracle.DataAccess.Client.dll驱动为例" + Environment.NewLine);
                explain.AppendFormat(@"步骤1：app.config或Web.config文件中可能需要添加如下节点，同时需要注意Version。" + Environment.NewLine);
                explain.AppendFormat(@"<system.data>" + Environment.NewLine);
                explain.AppendFormat(@"  <DbProviderFactories>" + Environment.NewLine);
                explain.AppendFormat(@"    <add name=""Oracle Data Provider for .NET"" invariant=""Oracle.DataAccess.Client"" description=""Oracle Data Provider for .NET"" type=""Oracle.DataAccess.Client.OracleClientFactory, Oracle.DataAccess, Version=2.111.6.20, Culture=neutral, PublicKeyToken=89b483f429c47342""/>" + Environment.NewLine);
                explain.AppendFormat(@"  </DbProviderFactories>" + Environment.NewLine);
                explain.AppendFormat(@"</system.data>" + Environment.NewLine);
                explain.AppendFormat(@"步骤2：注册相关驱动DLL到GAC（如果步骤1有效可以忽略该操作）。" + Environment.NewLine);
                explain.AppendFormat(@"(1)查看GAC：gacutil.exe /l Oracle.DataAccess。" + Environment.NewLine);
                explain.AppendFormat(@"(2)注册GAC：gacutil.exe /i Oracle.DataAccess.dll。" + Environment.NewLine);
                explain.AppendFormat(@"(3)对于某些数据库只需将相关的DLL放到程序集路径下，无需注册。" + Environment.NewLine);
                return base.Message + Environment.NewLine + explain.ToString();
            }
        }
    }
}
