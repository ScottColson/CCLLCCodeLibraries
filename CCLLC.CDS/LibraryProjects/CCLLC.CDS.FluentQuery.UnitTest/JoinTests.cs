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


        #region Join_Should_SelectColumns

        [TestMethod]
        public void Test_Join_Should_SelectColumns()
        {
            new Join_Should_SelectColumns().Test();
        }

        private class Join_Should_SelectColumns : TestMethodClassBase
        {
           
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .Select(cols => new { cols.AccountNumber, cols.Name })
                    .LeftJoin<Contact>("primarycontactid", "contactid", c => c
                         .WithAlias("pc")
                         .Select(cols => new { cols.FullName }))
                    .Build();
                               
                var linkEntity = qryExpression.LinkEntities[0];
                var columnSet = linkEntity.Columns;

                Assert.AreEqual(1, columnSet.Columns.Count);
                Assert.AreEqual("fullname", columnSet.Columns[0]);
            }
        }

        #endregion Join_Should_SelectColumns


        #region JoinWithWhere_Should_CreateLinkEntityFilter

        [TestMethod]
        public void Test_JoinWithWhere_Should_CreateLinkEntityFilter()
        {
            new JoinWithWhere_Should_CreateLinkEntityFilter().Test();
        }

        private class JoinWithWhere_Should_CreateLinkEntityFilter : TestMethodClassBase
        {

            protected override void Test(IOrganizationService service)
            {
               var qryExpression = new QueryExpressionBuilder<Account>()
                     .Select(cols => new { cols.AccountNumber, cols.Name })
                     .LeftJoin<Contact>("primarycontactid", "contactid", c => c
                          .WithAlias("pc")
                          .Select(cols => new { cols.FullName })
                          .WhereAll(c1 => c1
                            .IsActive()
                            .Attribute("fullname").IsNotNull()))
                     .Build();

                var linkEntity = qryExpression.LinkEntities[0];
                var filter = linkEntity.LinkCriteria;

                Assert.AreEqual(LogicalOperator.And, filter.FilterOperator);
                Assert.AreEqual(2, filter.Conditions.Count);
                Assert.AreEqual("statecode", filter.Conditions[0].AttributeName);
            }
        }

        #endregion JoinWithWhere_Should_CreateLinkEntityFilter


        #region NestedJoin_Should_CreateNestedLinkEntity

        [TestMethod]
        public void Test_NestedJoin_Should_CreateNestedLinkEntity()
        {
            new NestedJoin_Should_CreateNestedLinkEntity().Test();
        }

        private class NestedJoin_Should_CreateNestedLinkEntity : TestMethodClassBase
        {
           

            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .Select(cols => new { cols.AccountNumber, cols.Name })
                    .LeftJoin<Contact>("primarycontactid", "contactid", c => c
                        .InnerJoin<SystemUser>("ownerid","systemuserid", u => u
                            .WithAlias("owner")
                            .Select(cols => new { cols.SystemUserId } )))
                    .Build();

                var linkEntity = qryExpression.LinkEntities[0];
                linkEntity = linkEntity.LinkEntities[0];

                Assert.AreEqual(JoinOperator.Inner, linkEntity.JoinOperator);
                Assert.AreEqual("contact", linkEntity.LinkFromEntityName);
                Assert.AreEqual("systemuser", linkEntity.LinkToEntityName);
                Assert.AreEqual("ownerid", linkEntity.LinkFromAttributeName);
                Assert.AreEqual("systemuserid", linkEntity.LinkToAttributeName);
                Assert.AreEqual("owner", linkEntity.EntityAlias);
            }
        }

        #endregion NestedJoin_Should_CreateNestedLinkEntity
    }
}
