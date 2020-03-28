using System;
using CCLLC.CDS.Test;
using CCLLC.CDS.Test.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using TestProxy;

namespace CCLLC.CDS.FluentQuery.UnitTest
{
    [TestClass]
    public class OrderTests
    {
        #region OrderAsc_Should_CreateAscendingOrderExpression

        [TestMethod]
        public void Test_OrderAsc_Should_CreateAscendingOrderExpression()
        {
            new OrderAsc_Should_CreateAscendingOrderExpression().Test();
        }

        private class OrderAsc_Should_CreateAscendingOrderExpression : TestMethodClassBase
        {
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .Select(cols => new { cols.AccountNumber, cols.Name })
                    .OrderByAsc("accountnumber","name")
                    .Build();

                Assert.AreEqual(2, qryExpression.Orders.Count);
                Assert.AreEqual("accountnumber", qryExpression.Orders[0].AttributeName);
                Assert.AreEqual(OrderType.Ascending, qryExpression.Orders[0].OrderType);
                Assert.AreEqual("name", qryExpression.Orders[1].AttributeName);
                Assert.AreEqual(OrderType.Ascending, qryExpression.Orders[1].OrderType);
            }
        }

        #endregion OrderAsc_Should_CreateAscendingOrderExpression


        #region OrderByDesc_Should_CreateDescendingOrderExpression

        [TestMethod]
        public void Test_OrderByDesc_Should_CreateDescendingOrderExpression()
        {
            new OrderByDesc_Should_CreateDescendingOrderExpression().Test();
        }

        private class OrderByDesc_Should_CreateDescendingOrderExpression : TestMethodClassBase
        {
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .Select(cols => new { cols.AccountNumber, cols.Name })
                    .OrderByDesc("name", "accountnumber")
                    .Build();

                Assert.AreEqual(2, qryExpression.Orders.Count);
                Assert.AreEqual("name", qryExpression.Orders[0].AttributeName);
                Assert.AreEqual(OrderType.Descending, qryExpression.Orders[0].OrderType);
                Assert.AreEqual("accountnumber", qryExpression.Orders[1].AttributeName);
                Assert.AreEqual(OrderType.Descending, qryExpression.Orders[1].OrderType);
            }
        }

        #endregion OrderByDesc_Should_CreateDescendingOrderExpression

    }
}
