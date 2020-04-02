using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCLLC.CDS.Test;
using CCLLC.CDS.Test.Builders;
using DLaB.Xrm.Test;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace CCLLC.CDS.Sdk.Instrumented.UnitTest
{
    [TestClass]
    public class RegistrationTests
    {
        #region RegisterCreateEvent_Should_ExecuteCreateEventHandler

        [TestMethod]
        public void Test_RegisterCreateEvent_Should_ExecuteCreateEventHandler()
        {
            new RegisterCreateEvent_Should_ExecuteCreateEventHandler().Test();
        }

        private class RegisterCreateEvent_Should_ExecuteCreateEventHandler : TestMethodClassBase
        {
            private class TestPlugin : CDSPlugin
            {
                public TestPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
                {
                    RegisterCreateHandler<TestProxy.Account>(ePluginStage.PreOperation, createPreOpTest);
                    RegisterCreateHandler<TestProxy.Account>(ePluginStage.PostOperation, createPostOpTest);
                }

                private void createPostOpTest(ICDSPluginExecutionContext executionContext, TestProxy.Account target, EntityReference response)
                {
                    Assert.AreEqual(Ids.Account, target.Id);
                    Assert.AreEqual(default(Guid), response.Id);

                    //alter the response to see if it gets saved to the outputs collection.
                    response.Id = Guid.NewGuid();
                }

                private void createPreOpTest(ICDSPluginExecutionContext executionContext, TestProxy.Account target, EntityReference response)
                {
                    Assert.AreEqual(Ids.Account, target.Id);
                    Assert.IsNull(response);
                    
                    //alter the target to see if it get saved to the input parameters collection.
                    target.AccountNumber = "1";

                }
            }

            private struct Ids
            {
                public static readonly Id Account = new Id<TestProxy.Account>("{C52889CB-97A3-4225-8F9A-8C9EF65DAB4E}");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                var plugin = new TestPlugin(null, null);

                var serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(20, "Create", "account")
                        .WithPrimaryEntityId(Ids.Account)
                        .WithTarget(Ids.Account)                        
                        .Build(),
                    new DebugLogger()).Build();


                var executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                var target = executionContext.InputParameters["Target"] as Entity;

                Assert.AreEqual(2, target.Attributes.Count);

                plugin.Execute(serviceProvider);

                // input parameter was changed
                Assert.AreEqual(3, target.Attributes.Count);


                serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(40, "Create", "account")
                        .WithPrimaryEntityId(Ids.Account)
                        .WithTarget(Ids.Account)
                        .WithOutputParameter("id",default(Guid))
                        .Build(),
                    new DebugLogger()).Build();

                executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                plugin.Execute(serviceProvider);

                // output parameter was changed.
                Assert.AreNotEqual(default(Guid), (Guid)executionContext.OutputParameters["id"]);
            }
        }

        #endregion RegisterCreateEvent_Should_ExecuteCreateEventHandler


        #region RegisterUpdateEvent_Should_ExecuteUpdateEventHandler

        [TestMethod]
        public void Test_RegisterUpdateEvent_Should_ExecuteUpdateEventHandler()
        {
            new RegisterUpdateEvent_Should_ExecuteUpdateEventHandler().Test();
        }

        private class RegisterUpdateEvent_Should_ExecuteUpdateEventHandler : TestMethodClassBase
        {
            private class TestPlugin : CDSPlugin
            {
                public TestPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
                {
                    RegisterUpdateHandler<TestProxy.Account>(ePluginStage.PreOperation, updatePreOpTest);
                    RegisterUpdateHandler<TestProxy.Account>(ePluginStage.PostOperation, updatePostOpTest);
                }

                private void updatePostOpTest(ICDSPluginExecutionContext executionContext, TestProxy.Account target)
                {
                    Assert.AreEqual(Ids.Account, target.Id);                  
                }

                private void updatePreOpTest(ICDSPluginExecutionContext executionContext, TestProxy.Account target)
                {
                    Assert.AreEqual(Ids.Account, target.Id);
                   
                    //alter the target to see if it get saved to the input parameters collection.
                    target.AccountNumber = "1";
                }
            }

            private struct Ids
            {
                public static readonly Id Account = new Id<TestProxy.Account>("{164A530F-60AA-4E91-A6CA-D3A99AF83EC6}");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                var plugin = new TestPlugin(null, null);

                var serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(20, "Update", "account")
                        .WithPrimaryEntityId(Ids.Account)
                        .WithTarget(Ids.Account)
                        .Build(),
                    new DebugLogger()).Build();


                var executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                var target = executionContext.InputParameters["Target"] as Entity;

                Assert.AreEqual(2, target.Attributes.Count);

                plugin.Execute(serviceProvider);

                // input parameter was changed
                Assert.AreEqual(3, target.Attributes.Count);

                serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(40, "Update", "account")
                        .WithPrimaryEntityId(Ids.Account)
                        .WithTarget(Ids.Account)                       
                        .Build(),
                    new DebugLogger()).Build();

                executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                plugin.Execute(serviceProvider);
            }
        }

        #endregion RegisterUpdateEvent_Should_ExecuteUpdateEventHandler


        #region RegisterDeleteEvent_Should_ExecuteDeleteEventHandler

        [TestMethod]
        public void Test_RegisterDeleteEvent_Should_ExecuteDeleteEventHandler()
        {
            new RegisterDeleteEvent_Should_ExecuteDeleteEventHandler().Test();
        }

        private class RegisterDeleteEvent_Should_ExecuteDeleteEventHandler : TestMethodClassBase
        {
            private class TestPlugin : CDSPlugin
            {
                public TestPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
                {
                    RegisterDeleteHandler<TestProxy.Account>(ePluginStage.PreOperation, deletePreOpTest);
                    RegisterDeleteHandler<TestProxy.Account>(ePluginStage.PostOperation, deletePostOpTest);
                }

                private void deletePostOpTest(ICDSPluginExecutionContext executionContext, EntityReference target)
                {
                    Assert.AreEqual(Ids.Account, target.Id);
                }

                private void deletePreOpTest(ICDSPluginExecutionContext executionContext, EntityReference target)
                {
                    Assert.AreEqual(Ids.Account, target.Id);                    
                }
            }

            private struct Ids
            {
                public static readonly Id Account = new Id<TestProxy.Account>("{164A530F-60AA-4E91-A6CA-D3A99AF83EC6}");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                var plugin = new TestPlugin(null, null);

                var serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(20, "Delete", "account")
                        .WithPrimaryEntityId(Ids.Account)
                        .WithTarget(Ids.Account)
                        .Build(),
                    new DebugLogger()).Build();


                var executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                var target = executionContext.InputParameters["Target"] as EntityReference;

               
                plugin.Execute(serviceProvider);
                

                serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(40, "Delete", "account")
                        .WithPrimaryEntityId(Ids.Account)
                        .WithTarget(Ids.Account)
                        .Build(),
                    new DebugLogger()).Build();

                executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                plugin.Execute(serviceProvider);
            }
        }

        #endregion RegisterDeleteEvent_Should_ExecuteDeleteEventHandler

    }
}
