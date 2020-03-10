using System;
using System.Collections.Generic;
using CCLLC.Core;

namespace CCLLC.CDS.Sdk
{
    public interface ICDSPlugin 
    {
        /// <summary>
        /// IOC container used to instantiate various objects required during the execution of the plugin.
        /// </summary>
        IIocContainer Container { get; }
       
        eRunAs RunAs { get; set; }

        IReadOnlyList<PluginEvent> PluginEventHandlers { get; }
       
        void RegisterEventHandler(string entityName, string messageName, ePluginStage stage, Action<ICDSPluginExecutionContext> handler, string id = "");

        void RegisterContainerServices();

       
    }
}
