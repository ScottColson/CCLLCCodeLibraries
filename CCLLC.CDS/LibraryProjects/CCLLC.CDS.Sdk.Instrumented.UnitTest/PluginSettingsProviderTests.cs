using System;
using CCLLC.CDS.Test;
using CCLLC.CDS.Test.Builders;
using DLaB.Xrm.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using CCLLC.Core;

namespace CCLLC.CDS.Sdk.Instrumented.UnitTest
{
    [TestClass]
    public class PluginSettingsProviderTests
    {
        #region SettingsConnector_Should_LoadDefinitionsWithDefaultValues

        [TestMethod]
        public void Test_SettingsConnector_Should_LoadDefinitionsWithDefaultValues()
        {
            new SettingsConnector_Should_LoadDefinitionsWithDefaultValues().Test();
        }

       
        private class SettingsConnector_Should_LoadDefinitionsWithDefaultValues : TestMethodClassBase
        {

            private class TestPlugin : CDSPlugin
            {
                public TestPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
                {
                    this.RegisterEventHandler(null,null, ePluginStage.PostOperation, checkSettings);
                }

                private void checkSettings(IProcessExecutionContext executionContext)
                {
                    Assert.AreEqual(3, executionContext.Settings.Count);
                    var value = executionContext.Settings.GetValue<string>("ENV1");
                    Assert.AreEqual("Value1", executionContext.Settings
                        .GetValue<string>("ENV1"));
                    Assert.AreEqual("Value2", executionContext.Settings.GetValue<string>("env2"));
                    Assert.AreEqual(TimeSpan.FromTicks(0).Ticks, executionContext.Settings.GetValue<TimeSpan?>("CCLLC.SettingsCacheTimeOut").Value.Ticks);
                }
            }

            private struct Ids
            {
                public static readonly Id EnvVariableTimeout = new Id<TestProxy.EnvironmentVariableDefinition>("D7BADF54-2995-49F2-AF1B-6D5340E60AE9");
                public static readonly Id EnvVariableDef1 = new Id<TestProxy.EnvironmentVariableDefinition>("42E171A2-64FE-431B-B2CF-205E1A8B454A");
                public static readonly Id EnvVariableDef2 = new Id<TestProxy.EnvironmentVariableDefinition>("3D5EA79F-5166-4C94-847F-A0A842BBB407");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder()
                    .WithBuilder<EnvVariableDefinitionBuilder>(Ids.EnvVariableTimeout, b => b
                        .WithDisplayName("CCLLC.SettingsCacheTimeOut").WithDefaultValue("0"))
                    .WithBuilder<EnvVariableDefinitionBuilder>(Ids.EnvVariableDef1, b => b   
                        .WithDisplayName("ENV1").WithDefaultValue("Value1"))
                    .WithBuilder<EnvVariableDefinitionBuilder>(Ids.EnvVariableDef2, b => b
                        .WithDisplayName("ENV2").WithDefaultValue("Value2"))
                    .WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                service = new OrganizationServiceBuilder(service)
                    .Build();

                var plugin = new TestPlugin(null, null);

                var serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(40, "Create", "new_testentity")
                        .WithPrimaryEntityId(Guid.NewGuid())
                        .Build(),
                    new DebugLogger()).Build();

                plugin.Execute(serviceProvider);
            }

            
        }

        #endregion SettingsConnector_Should_LoadDefinitionsWithDefaultValues

        #region SettingConnector_Should_LoadOverrideValues

        [TestMethod]
        public void Test_SettingConnector_Should_LoadOverrideValues()
        {
            new SettingConnector_Should_LoadOverrideValues().Test();
        }

        // ReSharper disable once InconsistentNaming
        private class SettingConnector_Should_LoadOverrideValues : TestMethodClassBase
        {
            private class TestPlugin : CDSPlugin
            {
                public TestPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
                {
                    this.RegisterEventHandler(null, null, ePluginStage.PostOperation, checkSettings);
                }

                private void checkSettings(IProcessExecutionContext executionContext)
                {
                    Assert.AreEqual(3, executionContext.Settings.Count);

                    Assert.AreEqual("Value1", executionContext.Settings.GetValue<string>("ENV1"));
                    Assert.AreEqual("Override2", executionContext.Settings.GetValue<string>("ENV2"));
                }
            }

            private struct Ids
            {
                public static readonly Id EnvVariableTimeout = new Id<TestProxy.EnvironmentVariableDefinition>("2CC1F7EC-C546-40C5-9060-7E2287167B8A");
                public static readonly Id EnvVariableDef1 = new Id<TestProxy.EnvironmentVariableDefinition>("772547AD-D006-4413-932E-E9C2466117A2");
                public static readonly Id EnvVariableDef2 = new Id<TestProxy.EnvironmentVariableDefinition>("8A736A38-1EE5-4066-9869-7770D75C1886");
                public static readonly Id EnvVariableVal2 = new Id<TestProxy.EnvironmentVariableValue>("36B32AD3-A3B2-4296-BAD8-339B2B9E3F84");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder()
                    .WithBuilder<EnvVariableDefinitionBuilder>(Ids.EnvVariableTimeout, b => b
                        .WithDisplayName("CCLLC.SettingsCacheTimeOut").WithDefaultValue("0"))
                    .WithBuilder<EnvVariableDefinitionBuilder>(Ids.EnvVariableDef1, b => b
                        .WithDisplayName("ENV1").WithDefaultValue("Value1"))
                    .WithBuilder<EnvVariableDefinitionBuilder>(Ids.EnvVariableDef2, b => b
                        .WithDisplayName("ENV2").WithDefaultValue("Value2"))
                    .WithBuilder<EnvVariableValueBuilder>(Ids.EnvVariableVal2, b => b
                        .ForDefinition(Ids.EnvVariableDef2).WithValue("Override2"))
                    .WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                service = new OrganizationServiceBuilder(service)
                    .Build();

                var plugin = new TestPlugin(null, null);

                var serviceProvider = new ServiceProviderBuilder(
                    service,
                    new PluginExecutionContextBuilder()
                        .WithRegisteredEvent(40, "Create", "new_testentity")
                        .WithPrimaryEntityId(Guid.NewGuid())
                        .Build(),
                    new DebugLogger()).Build();

                plugin.Execute(serviceProvider);
            }
        }

        #endregion SettingConnector_Should_LoadOverrideValues

    }
}
