using System;
using CCLLC.CDS.Test;
using CCLLC.CDS.Test.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using TestProxy;

namespace CCLLC.CDS.Sdk.UnitTest
{
    [TestClass]
    public class QuerySettingsTests
    {
        #region QueryExpressionLock_Should_DefaultToNoLock

        [TestMethod]
        public void Test_QueryExpressionLock_Should_DefaultToNoLock()
        {
            new QueryExpressionLock_Should_DefaultToNoLock().Test();
        }

        private class QueryExpressionLock_Should_DefaultToNoLock : TestMethodClassBase
        {      
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .Build();

                Assert.AreEqual(true, qryExpression.NoLock);
            }
        }

        #endregion QueryExpressionLock_Should_DefaultToNoLock

        #region QueryExpressionTopCount_Should_DefultToNull

        [TestMethod]
        public void Test_QueryExpressionTopCount_Should_DefultToNull()
        {
            new QueryExpressionTopCount_Should_DefultToNull().Test();
        }

        private class QueryExpressionTopCount_Should_DefultToNull : TestMethodClassBase
        {
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .Build();

                Assert.AreEqual(null, qryExpression.TopCount);
            }
        }

        #endregion QueryExpressionTopCount_Should_DefultToNull

        #region WithDatabaseLock_Should_SetNoLockFalse

        [TestMethod]
        public void Test_WithDatabaseLock_Should_SetNoLockFalse()
        {
            new WithDatabaseLock_Should_SetNoLockFalse().Test();
        }

        private class WithDatabaseLock_Should_SetNoLockFalse : TestMethodClassBase
        {        
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .With.DatabaseLock()
                    .Build();

                Assert.AreEqual(false, qryExpression.NoLock);
            }
        }

        #endregion WithDatabaseLock_Should_SetNoLockFalse

        #region WithRecordLimit_Should_SetTopCount

        [TestMethod]
        public void Test_WithRecordLimit_Should_SetTopCount()
        {
            new WithRecordLimit_Should_SetTopCount().Test();
        }

        private class WithRecordLimit_Should_SetTopCount : TestMethodClassBase
        {
            protected override void Test(IOrganizationService service)
            {
                var qryExpression = new QueryExpressionBuilder<Account>()
                    .With.RecordLimit(1)
                    .Build();

                Assert.AreEqual(1, qryExpression.TopCount);
            }
        }

        #endregion WithRecordLimit_Should_SetTopCount

    }
}
