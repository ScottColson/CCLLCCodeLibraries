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
    public class JoinTests
    {
        #region LeftJoin_Should_CreateLinkedEntity

        [TestMethod]
        public void Test_LeftJoin_Should_CreateLinkedEntity()
        {
            new LeftJoin_Should_CreateLinkedEntity().Test();
        }

        private class LeftJoin_Should_CreateLinkedEntity : TestMethodClassBase
        {           
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                     .Select(cols => new { cols.AccountNumber, cols.Name })
                     .LeftJoin<Contact>("primarycontactid", "contactid", c => c
                          .WithAlias("pc")
                          .Select(cols => new { cols.FullName }))
                     .Build();

                Assert.AreEqual(false, qryExpression.ColumnSet.AllColumns);
                Assert.AreEqual(2, qryExpression.ColumnSet.Columns.Count);
                Assert.AreEqual(1, qryExpression.LinkEntities.Count);

                var linkEntity = qryExpression.LinkEntities[0];

                Assert.AreEqual(JoinOperator.LeftOuter, linkEntity.JoinOperator);
                Assert.AreEqual("account", linkEntity.LinkFromEntityName);
                Assert.AreEqual("contact", linkEntity.LinkToEntityName);
                Assert.AreEqual("primarycontactid", linkEntity.LinkFromAttributeName);
                Assert.AreEqual("contactid", linkEntity.LinkToAttributeName);
                Assert.AreEqual("pc", linkEntity.EntityAlias);

            }
        }

        #endregion LeftJoin_Should_CreatesLinkedEntity


        #region InnerJoin_Should_CreateLinkedEntity

        [TestMethod]
        public void Test_InnerJoin_Should_CreateLinkedEntity()
        {
            new InnerJoin_Should_CreateLinkedEntity().Test();
        }

        private class InnerJoin_Should_CreateLinkedEntity : TestMethodClassBase
        {
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                     .Select(cols => new { cols.AccountNumber, cols.Name })
                     .InnerJoin<Contact>("primarycontactid", "contactid", c => c
                          .WithAlias("pc")
                          .Select(cols => new { cols.FullName }))
                     .Build();

                Assert.AreEqual(false, qryExpression.ColumnSet.AllColumns);
                Assert.AreEqual(2, qryExpression.ColumnSet.Columns.Count);
                Assert.AreEqual(1, qryExpression.LinkEntities.Count);

                var linkEntity = qryExpression.LinkEntities[0];

                Assert.AreEqual(JoinOperator.Inner, linkEntity.JoinOperator);
                Assert.AreEqual("account", linkEntity.LinkFromEntityName);
                Assert.AreEqual("contact", linkEntity.LinkToEntityName);
                Assert.AreEqual("primarycontactid", linkEntity.LinkFromAttributeName);
                Assert.AreEqual("contactid", linkEntity.LinkToAttributeName);
                Assert.AreEqual("pc", linkEntity.EntityAlias);

            }
        }

        #endregion InnerJoin_Should_CreateLinkedEntity

    }
}
