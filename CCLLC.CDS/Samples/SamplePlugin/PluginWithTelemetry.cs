


namespace SamplePlugin
{
    using System;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using CCLLC.Core;
    using CCLLC.CDS.Sdk;

    /// <summary>
    ///  Sample plugin built on top of the <see cref="InstrumentedCDSPlugin"/> abstract class. It demonstrates various features of the 
    ///  CCLLC.CDS.Sdk.Sources and CCLLC.CDS.Sdk.Instrumented.Sources Nuget packages. The code for these packages as well as supporting classes are 
    ///  imported into the App_PackageFolder by Nuget.
    ///  
    ///  To build this plugin you will need to do the following:
    ///  1. Add reference to System.Runtime.Caching.
    ///  2. If not using VS2019 - set C# language version to a minimum of 7.1
    ///  3. Install Nuget package Microsoft.CrmSdk.CoreAssemblies 9.0.2.23 or later.
    ///  3. Install Nuget package CCLLC.CDS.Sdk.Instrumented.Sources 1.1.2 or later.
    ///  
    ///  To fully see the results of telemetry tracking you will need to setup an ApplicationInsights instance in Azure and set the DefaultIntrumentationKey
    ///  value in the plugin constructor.
    ///
    /// </summary>
    [CrmPluginRegistration(MessageNameEnum.Create, "account", StageEnum.PostOperation,
      ExecutionModeEnum.Synchronous, null, "Sample PostOp Account Create", 1001, IsolationModeEnum.Sandbox,
      Id = "{542E2780-E569-4B95-988D-EC36C5238FF4}")]
    [CrmPluginRegistration(MessageNameEnum.Update,"account", StageEnum.PostOperation,
      ExecutionModeEnum.Synchronous, null, "Sample PostOp Account Update", 1001, IsolationModeEnum.Sandbox,
      Id = "{3B694AE4-63EF-42A6-98A7-33771E784901}")]
    [CrmPluginRegistration(MessageNameEnum.Update, "none", StageEnum.PreOperation,
      ExecutionModeEnum.Synchronous, null, "Sample PreOp Entity Update", 1001, IsolationModeEnum.Sandbox,
      Id = "{A2EB72E2-3364-4A5A-8659-73D45947C428}")]
    public class PluginWithTelemetry : InstrumentedCDSPlugin
    {

        /// <summary>
        /// Plugins built on the CCLLC.CDS.Sdk require a constructor. The constructor is called when the plugin is
        /// first loaded by the CDS platform. Typically a plugin is cached by the CDS platform until it has not been
        /// used for some period of time. Therefore the constructor is not executed at each plugin execution.
        /// </summary>
        /// <param name="unsecureConfig"></param>
        /// <param name="secureConfig"></param>
        public PluginWithTelemetry(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {

            #region Set Execution Properties
            /* 
             *  Plugin Execution Properties 
             */

            // RunAs - Determines whether the plugin organization service runs under the security profile of the plugin user or
            // under the context of the System user. Default is User and it is set here only to show availability of the property.
            this.RunAs = eRunAs.User;

            // DefaultInstrumentationKey (for InstrumetendCDSPlugin only)  - Sets the ApplicationInsights instrumentation key. This value is set in code 
            // for developer convenience but in practice it should  be set in a CDS Environment Variable named 'Telemetry.InstrumentationKey'
            this.DefaultInstrumentationKey = "7a6ecb67-6c9c-4640-81d2-80ce76c3ca34";

            // TrackExecutionPerformance (for InstumentedCDSPlugin only)  - When set to true the plugin will capture execution performance time for each 
            // executed event handler. 
            this.TrackExecutionPerformance = true;

            #endregion


            #region Register Event Handlers
            /* 
             *  Link Event Handlers to Events
             * 
             *  Multiple handler can be linked to a single event and multiple events can be linked to a single handler needed. When
             *  the plugin executes, the handlers will be evaluated to see if the entity type, message name, and execution stage match
             *  and the handler will be called if there is a match.
             *  
             *  Handlers are evaluated in the order they are registered.
             *  
             *  Each handler uses the same execution context.
             *  
             *  An optional "id" parameter can be set during event handler registration. This id is sent to ApplicaitonInsights to assist 
             *  in analyzing the plugin execution.
             *  
             *  Sdk Messages must still be created to link the actual events in the CDS platform to the plugin. This can be done manually
             *  or using tools such as spkl.
             *  
             */

            // Execute the workingWithEntities handler in the PreOpStage of an Account update.
            RegisterEventHandler("account", MessageNames.Update, ePluginStage.PreOperation, workingWithEntities);

            // Execute the demonstrateWebRequest handler in the Post Operation stage of an account creation.
            RegisterEventHandler("account", MessageNames.Create, ePluginStage.PostOperation, demonstrateWebRequest);

            // Execute the workingWithOrgService handler in the Post Operation stage of an account update.
            RegisterEventHandler("account", MessageNames.Update, ePluginStage.PostOperation, workingWithOrgService);

            // Call demonstrateTelemetry handler in the PreOperation stage of any entity update. Send a specific id tag to telemetry.
            RegisterEventHandler(null, MessageNames.Update, ePluginStage.PreOperation, demonstrateTelemetry, "OnUpdateHandler_TelemetryDemonstration");

            #endregion
        }


        /// <summary>
        /// Demonstrates some of the shortcuts and features around working with entities.
        /// </summary>
        /// <param name="executionContext"></param>
        private void workingWithEntities(ICDSPluginExecutionContext executionContext)
        {
            // TargetEntity - Provides access to the entity in the InputParameters Target parameter if it exists. Returns null if it does not exist.
            Entity target = executionContext.TargetEntity;

            // TargetReference - Provides and entity reference for the entity in the InputParameters Target parameter. Return null if the entity does not exist.
            EntityReference targetRef = executionContext.TargetReference;

            // PreImage - Provides access to the first entity image in the PreEntityImages collection. Returns null if a image does not exist. 
            Entity preImage = executionContext.PreImage;

            // PreMergedTarget - Provides an entity with attributes from the TargetEntity merged into the PreImage entity. If an attribute exists in 
            // both the TargetEntity and the PreImage, the merged target will show the attribute from the Target.
            Entity mergedTarget = executionContext.PreMergedTarget;


            // GetRecord is a simple retrieve shortcut that accepts an EntityReference, an optional list of columns, 
            // and/or a cache timeout value for caching the result, and returns an earlybound entity. 
            var myRecord1 = executionContext.GetRecord<Account>(targetRef); //Get record with all columns.
            var myRecord2 = executionContext.GetRecord<Account>(targetRef, new string[] {"name", "accountnumber" }); //retrieve 2 columns
            var myRecord3 = executionContext.GetRecord<Account>(targetRef, TimeSpan.FromSeconds(10)); //retrieve and cache for 10 seconds

            // ContainsAny Entity extension - Checks for existence of at least one of multiple attributes. Easier to read and understand than 
            // target.Contains("name") || target.Contains("accountnumber") especially with a large set of attributes.
            if (target.ContainsAny(new string[]{ "name","accountnumber"}))
            {
                executionContext.Trace("Target contains at least one matching attribute");  // short cut to tracing service Trace method.
            }


            
        }


        /// <summary>
        /// Demonstrates a simple GET operation using built in web request support. Will automatically
        /// generate dependency telemetry.
        /// </summary>
        /// <param name="executionContext"></param>
        private void demonstrateWebRequest(ICDSPluginExecutionContext executionContext)
        {
            Uri googleUri = new Uri("https://www.google.com");
            using(var webRequest = executionContext.CreateWebRequest(googleUri))
            {
                var result = webRequest.Get();
                executionContext.Trace("Returned {0} characters", result.Content.Length);
            }
        }


        /// <summary>
        /// Demonstrates various features associated with the Organization Service 
        /// </summary>
        /// <param name="executionContext"></param>
        private void workingWithOrgService(ICDSPluginExecutionContext executionContext)
        {
            // OrganizationService - Provides easy access to the organization service. 
            // This instance of the organization service runs as user or system based on
            // the plugin RunAs execution property.
            var orgService = executionContext.OrganizationService;

            // ElevatedOrganizationService - Provides easy access to an organization service
            // instance that runs under the security context of the System User regardless
            // of plugin RunAs execution property.
            var elevatedOrgService = executionContext.ElevatedOrganizationService;


            // FluentQuery OrganizationService extension generates more readable queries than
            // standard query syntax. 
            var records = orgService.Query<Account>()
                .Select(cols => new { cols.Name, cols.AccountNumber })
                .WhereAll(e => e
                    .IsActive()
                    .Attribute("name").Is<string>(ConditionOperator.BeginsWith,"C"))
                .OrderByAsc("name","accountnumber")
                .Retrieve();
                
        }

        /// <summary>
        /// Demonstrates basic tracing and tracking methods built into the execution context.
        /// </summary>
        /// <param name="executionContext"></param>
        private void demonstrateTelemetry(ICDSPluginExecutionContext executionContext)
        {
            // Information message written to the standard tracing service and also to ApplicationInsights.
            executionContext.Trace("The primary entity type is '{0}'", executionContext.PrimaryEntityName);

            // Waring message written to the standard tracing service and also to ApplicationInsights.
            executionContext.Trace(eSeverityLevel.Warning, "A warning message for record '{0}'", executionContext.PrimaryEntityId);

            // Create an Event marker in ApplicationInsights
            executionContext.TrackEvent("Event_1");

            // Capture exception information for a handled exception in ApplicationInsights
            // Note that capturing exception telemetry for unhandled exceptions is taken care of
            // in the InstrumentedCDSPlugin abstract class.
            try
            {
                throw new Exception("Sample exception");
            }
            catch(Exception ex)
            {
                executionContext.TrackException(ex);
                // do something to handle exception
                
            }

        }
    }


}
