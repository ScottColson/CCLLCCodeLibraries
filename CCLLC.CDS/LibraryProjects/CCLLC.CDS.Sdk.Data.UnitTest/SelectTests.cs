using System;
using CCLLC.CDS.Test;
using CCLLC.CDS.Test.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using TestProxy;

namespace CCLLC.CDS.FluentQuery.UnitTest
{
    [TestClass]
    public class SelectTests
    {

        #region SelectAll_Should_GenerateAllColumns

        [TestMethod]
        public void Test_SelectAll_Should_GenerateAllColumns()
        {
            new SelectAll_Should_GenerateAllColumns().Test();
        }

        private class SelectAll_Should_GenerateAllColumns : TestMethodClassBase
        {      
            protected override void Test(IOrganizationService service)
            {

                var qryExpression = new QueryExpressionBuilder<Account>()
                     .SelectAll()
                     .Build();

                Assert.AreEqual(true, qryExpression.ColumnSet.AllColumns);

               
            }
        }

        #endregion SelectAll_Should_GenerateAllColumns


        #region WildCard_Should_GenerateAllColumns

        [TestMethod]
        public void Test_WildCard_Should_GenerateAllColumns()
        {
            new WildCard_Should_GenerateAllColumns().Test();
        }

        private class WildCard_Should_GenerateAllColumns : TestMethodClassBase
        {
           

            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                     .Select("*")
                     .Build();

                Assert.AreEqual(true, qryExpression.ColumnSet.AllColumns);
            }
        }

        #endregion WildCard_Should_GenerateAllColumns


        #region StringValues_Should_GenerateColumnSetColumns

        [TestMethod]
        public void Test_StringValues_Should_GenerateColumnSetColumns()
        {
            new StringValues_Should_GenerateColumnSetColumns().Test();
        }

        private class StringValues_Should_GenerateColumnSetColumns : TestMethodClassBase
        {
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                     .Select("accountnumber","name")
                     .Build();

                Assert.AreEqual(false, qryExpression.ColumnSet.AllColumns);
                Assert.AreEqual(2, qryExpression.ColumnSet.Columns.Count);

            }
        }

        #endregion StringValues_Should_GenerateColumnSetColumns


        #region ProxyProjection_Should_GenerateColumnSetColumns

        [TestMethod]
        public void Test_ProxyProjection_Should_GenerateColumnSetColumns()
        {
            new ProxyProjection_Should_GenerateColumnSetColumns().Test();
        }

        private class ProxyProjection_Should_GenerateColumnSetColumns : TestMethodClassBase
        {    
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                      .Select(a => new { a.Id, a.Name, a.AccountNumber})
                      .Build();

                Assert.AreEqual(false, qryExpression.ColumnSet.AllColumns);
                Assert.AreEqual(3, qryExpression.ColumnSet.Columns.Count);
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("accountid"));
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("name"));
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("accountnumber"));
            }
        }

        #endregion ProxyProjection_Should_GenerateColumnSetColumns


        #region MultipleSelects_Should_MergeWithUniqueColumns

        [TestMethod]
        public void Test_MultipleSelects_Should_MergeWithUniqueColumns()
        {
            new MultipleSelects_Should_MergeWithUniqueColumns().Test();
        }

        private class MultipleSelects_Should_MergeWithUniqueColumns : TestMethodClassBase
        {          

            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                      .Select(a => new { a.Id, a.Name, a.AccountNumber })
                      .Select("accountid", "accountratingcode")
                      .Build();

                Assert.AreEqual(false, qryExpression.ColumnSet.AllColumns);
                Assert.AreEqual(4, qryExpression.ColumnSet.Columns.Count);
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("accountid"));
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("name"));
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("accountnumber"));
                Assert.IsTrue(qryExpression.ColumnSet.Columns.Contains("accountratingcode"));
            }
        }

        #endregion MultipleSelects_Should_MergeWithUniqueColumns


       
       
    }
}
