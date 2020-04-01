﻿using System;
using System.Collections.Generic;
using CCLLC.Core;

namespace CCLLC.CDS.Sdk
{
    /// <summary>
    /// Enhanced plugin definition for creating CDS plugins that provides support for dependency injection.
    /// </summary>
    public interface ICDSPlugin
    {
        /// <summary>
        /// Inversion of Control/Dependency Injection container used to inject various dependencies required during the execution 
        /// of the plugin.
        /// </summary>
        IIocContainer Container { get; }

        /// <summary>
        /// Execution flag that indicates whether the code runs under the security profile of the user or the system. When 
        /// set to System then the OrganizationService always runs with elevated permissions.
        /// </summary>
        eRunAs RunAs { get; set; }

        /// <summary>
        /// Mechanism to register a generic plugin event handler for a particular event signature.
        /// </summary>
        /// <param name="entityName">Logical name of the entity that triggered the execution</param>
        /// <param name="messageName">Message name for the call</param>
        /// <param name="stage">Plugin stage for the call</param>
        /// <param name="handler">The handler that will be executed</param>
        /// <param name="id">An ad that can be passed through to telemetry when used.</param>
        void RegisterEventHandler(string entityName, string messageName, ePluginStage stage, Action<ICDSPluginExecutionContext> handler, string id = "");
    }
}
