using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Vic.Data
{
    /// <summary>
    /// SQL生成
    /// </summary>
    public abstract class SqlGeneratorBase
    {
        public abstract List<string> GenerateDeleteSql<T>(Expression exp) where T : class, new();
    }
}
