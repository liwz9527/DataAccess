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
            string conn = ConfigurationManager.ConnectionStrings["oracle"].ConnectionString;
            // 获取数据库驱动
            string prname = ConfigurationManager.ConnectionStrings["oracle"].ProviderName;
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
