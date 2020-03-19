using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CCLLC.CDS.Sdk.Instrumented.UnitTest
{
    using CCLLC.CDS.Test;
    using CCLLC.CDS.Test.Builders;
    using DLaB.Xrm.Test;
    using Microsoft.Xrm.Sdk;

    [TestClass]
    public class PluginTelemtryTests
    {
       
        #region ExecutingPlugin_Should_CaptureTelemetry

        /// <summary>
        /// This test exercises the telemetry tracking capabilities of the 
        /// <see cref="InstrumentedCDSPlugin"/> base class and writes the results to the
        /// ApplicationInsights site identified by the Instrumentation Key. The following
        /// telemetry capture mechanisms are tested:
        /// 
        ///  - Plugin Performance Tracking
        ///  - Event Tracking
        ///  - Message Tracking
        ///  - Exception Tracking (Handled and unhandled exceptions)
        ///  - Web request dependency tracking. 
        ///  
        /// Test will execute the plugin 10 times.  Test should run without error but 
        /// user must verify tracking results in ApplicationInsights.        /// 
        /// </summary>
        [TestMethod]
        public void Test_ExecutingPlugin_Should_CaptureTelemetry()
        {
            new ExecutingPlugin_Should_CaptureTelemetry().Test();
        }

        private class ExecutingPlugin_Should_CaptureTelemetry : TestMethodClassBase
        {
            private struct TestData
            {
                public static readonly string AIKey = "7a6ecb67-6c9c-4640-81d2-80ce76c3ca34";
            }
            private struct Ids
            {            
                
            }

            /// <summary>
            /// Test plugin executed by unit test. Plugin exercises various tracing and telemetry
            /// tracking methods that should write to Application Insights.
            /// </summary>
            class TestPlugin : InstrumentedCDSPlugin
            {

                public TestPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
                {

                    this.DefaultInstrumentationKey = TestData.AIKey ; // AI Instrumentation Key

                    this.TrackExecutionPerformance = true; // Enable plugin performance tracking telemetry.      

                    RegisterEventHandler(null, null, ePluginStage.PostOperation, doNothing, "doNothingId");
                }


                private void doNothing(ICDSPluginExecutionContext executionContext)
                {
                    // Event Tracking.
                    executionContext.TrackEvent("Initiated Handler");
                                        
                    // Exception tracking handled by code.
                    try
                    {
                        throw new Exception("Handled Exception");
                    }
                    catch (Exception ex)
                    {
                        executionContext.TrackException(ex);
                    }
                    
                    // Message Telemetry
                    executionContext.Trace("A simple message - {0}", executionContext.PrimaryEntityName);
                    executionContext.Trace(Core.eSeverityLevel.Warning, "A warning message {0}", executionContext.UserId);
                    executionContext.Trace(Core.eSeverityLevel.Critical, "A critical message");

                    // Web request dependency tracking.
                    using (var webReq = executionContext.CreateWebRequest(new Uri("http://google.com"), "myWebRequestTest"))
                    {
                        var data = webReq.Get();
                    }

                    // Unhandled exception tracking logged by base plugin.
                    throw new Exception("Throw Unhandled Exception");
                }
            }



            protected override void InitializeTestData(IOrganizationService service)
            {               
                    new CrmEnvironmentBuilder()     
                        .Create(service);               
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

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        plugin.Execute(serviceProvider);
                    }
                    catch (Exception ex)
                    {
                        // Process if it is not the expected exception we are throwing to test telemetry.
                        if (ex.Message != "Unhandled Plugin Exception Throw Unhandled Exception")
                        {
                            throw;
                        }
                    }
                }

               
            }
        }

        #endregion ExecutingPlugin_Should_CaptureTelemetry


    }
}
