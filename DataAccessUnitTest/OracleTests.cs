#define NUNIT0

using System;
using System.Collections.Generic;
using System.Linq;
using Vic.Data;
using System.Configuration;
#if !NUNIT 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#else
using NUnit.Framework; 
using TestClass = NUnit.Framework.TestFixtureAttribute; 
using TestMethod = NUnit.Framework.TestAttribute; 
using TestInitialize = NUnit.Framework.SetUpAttribute; 
using TestCleanup = NUnit.Framework.TearDownAttribute; 
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute; 
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute; 
using TestContext = System.Object;  
#endif

namespace DataAccessTest.UnitTests
{
    [TestClass]
    public class OracleTests
    { 
        // 定义数据库连接
        private DataAccess dbAccess = null;

        [TestInitialize]
        public void Setup()
        {
            // 获取数据库连接
            string conn = ConfigurationManager.ConnectionStrings["oracle1"].ConnectionString;
            // 获取数据库驱动
            string prname = ConfigurationManager.ConnectionStrings["oracle1"].ProviderName;
            // 初始化
            dbAccess = new DataAccess(conn, prname);
        }

        /// <summary>
        /// 检测是否可用
        /// </summary>
        [TestMethod] 
        public void TestConnect()
        {
            bool isCheck = dbAccess.CheckConn(); 

            Assert.IsNotNull(dbAccess);
            Assert.AreEqual(isCheck, true);

        }

        /// <summary>
        /// 测试表达式查询
        /// </summary>
        [TestMethod]
        public void TestQuery()
        {
            // 查询数据
            //var lstProduct = dbAccess.Query<Product>(p => p.ProductId == 10);

            //Assert.IsNotNull(lstProduct);
            //Assert.IsNotNull(lstProduct.First());
            //Assert.AreEqual(lstProduct.First().ProductId, 10);
            string sql = @"SELECT t.jh,
               h.dmmc jb,
               a.dydm dy,
               --b.SCGS dy,
               c.orgna_name dw,
               d.lb cslb,
               to_char(t.sjrq, 'yyyy-mm-dd') sjrq,
               t.glsjnr fanr,
               decode(i.n,1,'','变更') sfybgfa,
               to_char(t.sgrq, 'yyyy-mm-dd') kgrq,
               to_char(t.wgrq, 'yyyy-mm-dd') wgrq,
               e.gxmc sgjd,
               to_char(f.jsksrq, 'yyyy-mm-dd') ksjsrq,
               round(g.ysfy, 2) yjzyf,
               round(f.sjjsje/10000, 2) jsje,
               round(f.jslwf/10000, 2) lwje,
               round(f.jsclf/10000, 2) clje,
               '查看' ck,
               '监控' jk,
               t.fadm,
               CASE WHEN nvl(f.sjjsje / (g.ysfy * 10000), 0) > 0.3 THEN '1' WHEN i.n >1 THEN '1' ELSE '0' END AS flag
          FROM ws002 t
          LEFT JOIN ys_daa01 a
            ON t.jh = a.jh
          LEFT JOIN dab12 b
            ON a.dydm = b.dydm
           AND b.nd = to_char(t.sjrq, 'yyyy')
          LEFT JOIN u_orgnization c
            ON t.fqdwdm = c.orgna_id
          LEFT JOIN fla16 d
            ON t.cslb = d.dm
          LEFT JOIN (SELECT *
                      FROM ys_ddb01 e1
                     WHERE e1.gxqssj = (SELECT MAX(gxqssj) FROM ys_ddb01 e2 WHERE e2.jh = e1.jh)
                       AND rq = (SELECT MAX(rq)
                                   FROM ys_ddb01 e3
                                  WHERE e3.jh = e1.jh
                                    AND e1.gxqssj = e3.gxqssj)
                       AND sgxh = (SELECT MAX(sgxh)
                                     FROM ys_ddb01 e3
                                    WHERE e3.jh = e1.jh
                                      AND e1.gxqssj = e3.gxqssj)) e
            ON e.kgrq = t.sgrq
           AND e.jh = t.jh
          LEFT JOIN (SELECT MAX(jsksrq) jsksrq, fadm, SUM(sjjsje) sjjsje, SUM(jslwf) jslwf, SUM(jsclf) jsclf FROM js005 f1 GROUP BY fadm) f
            ON f.fadm = t.fadm
          LEFT JOIN (SELECT jh, SUM(ysfy) ysfy FROM gcb_yxq_result WHERE sfxd = 1 GROUP BY jh) g
            ON g.jh = t.jh
         Left Join f_jb h on t.jb=h.dm
         
         left join (select count(1) n,substr(fadm,1,length(fadm)-2)mm  from ws002 group by substr(fadm,1,length(fadm)-2)) i
          on substr(t.fadm,1,length(t.fadm)-2)=i.mm
         WHERE 1=1 and t.sjrq >= DATE '2016-01-01'
           AND c.orgna_name = '采油管理二区' and t.fadm like '%*1%' 
 ORDER BY t.sjrq DESC";
            int n = 0;
            //dbAccess.QueryPage(sql, 10, 1, out n);
            dbAccess.QueryPage(sql, 10, 31,out n);

            //DbSQL[] sqls = null;
            //dbAccess.ExecuteSqlTran(sqls);
        }

        [TestMethod]
        public void TestDelete()
        {
            // 判断是否写入测试数据
            //int rows = dbAccess.Query<Product>(t => t.Name.Contains("apple")).Count();

            //if(rows == 0)
            //{
                // 插入测试数据
                List<DbSQL> lstSql = new List<DbSQL>() {
                new DbSQL() { SQLString="insert into product(productid,name,createdate)values(100,'apple1',to_date('2016-01-01','yyyy-mm-dd'))" },
                new DbSQL() { SQLString="insert into product(productid,name,createdate)values(101,'apple2',to_date('2016-01-02','yyyy-mm-dd'))" },
                new DbSQL() { SQLString="insert into product(productid,name,createdate)values(102,'apple3',to_date('2016-01-03','yyyy-mm-dd'))" },
                new DbSQL() { SQLString="insert into product(productid,name,createdate)values(103,'apple4',to_date('2016-01-04','yyyy-mm-dd'))" },
                new DbSQL() { SQLString="insert into product(productid,name,createdate)values(104,'apple5',to_date('2016-01-05','yyyy-mm-dd'))" }};
                dbAccess.ExecuteSqlTran(0,lstSql);
            //}

            // 更新记录数
            int updateRows = 0;

            //利用表达式条件删除指定数据
            updateRows = dbAccess.Delete<Product>(p => p.CreateDate == DateTime.Parse("2016-01-01"));
            Assert.AreEqual(updateRows, 1);

            updateRows = dbAccess.Delete<Product>(p => p.ProductId == 101 || p.ProductId == 102);
            Assert.AreEqual(updateRows, 2);

            // 删除指定实体
            Product entity = new Product();
            entity.ProductId = 103;
            updateRows = dbAccess.Delete<Product>(entity);
            Assert.AreEqual(updateRows, 1);

            //根据动态类型删除数据
            var product = new { ProductId = 104 };
            updateRows = dbAccess.Delete(product, "Product");
            Assert.AreEqual(updateRows, 1);
            
        }
    }

    /// <summary>
    /// 产品实体
    /// </summary>
    public class Product
    {
        [Primarykey]
        public decimal ProductId { get; set; }
        public string Name { get; set; }
        public decimal CategoryId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
