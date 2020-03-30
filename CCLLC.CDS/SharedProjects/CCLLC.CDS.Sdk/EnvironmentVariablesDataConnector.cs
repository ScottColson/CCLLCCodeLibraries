using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    
    /// <summary>
    /// Provides access to settings stored in CDS Environment Variable entities. Returns current 
    /// values as key/value dictionary.
    /// </summary>
    public class EnvironmentVariablesDataConnector : ISettingsProviderDataConnector
    {
        #region Proxies

        [System.Runtime.Serialization.DataContractAttribute()]
        [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("environmentvariabledefinition")]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.41")]
        class EnvironmentVariableDefinition : Microsoft.Xrm.Sdk.Entity
        {            
            public EnvironmentVariableDefinition() :
                    base(EntityLogicalName)
            {
            }

            public const string EntityLogicalName = "environmentvariabledefinition";

    
            public string DefaultValue
            {
                get
                {
                    return this.GetAttributeValue<string>("defaultvalue");
                }
                set
                {
                    this.SetAttributeValue("defaultvalue", value);
                }
            }

           
            public string DisplayName
            {
                get
                {
                    return this.GetAttributeValue<string>("displayname");
                }
                set
                {                    
                    this.SetAttributeValue("displayname", value);
                }
            }

           
            public System.Nullable<System.Guid> EnvironmentVariableDefinitionId
            {
                get
                {
                    return this.GetAttributeValue<System.Nullable<System.Guid>>("environmentvariabledefinitionid");
                }
                set
                {                    
                    this.SetAttributeValue("environmentvariabledefinitionid", value);
                    if (value.HasValue)
                    {
                        base.Id = value.Value;
                    }
                    else
                    {
                        base.Id = System.Guid.Empty;
                    }                    
                }
            }
                        
            public override System.Guid Id
            {
                get
                {
                    return base.Id;
                }
                set
                {
                    this.EnvironmentVariableDefinitionId = value;
                }
            }

        }

        [System.Runtime.Serialization.DataContractAttribute()]
        [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("environmentvariablevalue")]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.41")]
        class EnvironmentVariableValue : Microsoft.Xrm.Sdk.Entity
        {            
            public EnvironmentVariableValue() :
                    base(EntityLogicalName)
            {
            }

            public const string EntityLogicalName = "environmentvariablevalue";

           
            /// <summary>
            /// Unique identifier for Environment Variable Definition associated with Environment Variable Value.
            /// </summary>            
            public Microsoft.Xrm.Sdk.EntityReference EnvironmentVariableDefinitionId
            {
                get
                {
                    return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("environmentvariabledefinitionid");
                }
                set
                {                   
                    this.SetAttributeValue("environmentvariabledefinitionid", value);
                }
            }

            /// <summary>
            /// Unique identifier for entity instances
            /// </summary>           
            public System.Nullable<System.Guid> EnvironmentVariableValueId
            {
                get
                {
                    return this.GetAttributeValue<System.Nullable<System.Guid>>("environmentvariablevalueid");
                }
                set
                {                    
                    this.SetAttributeValue("environmentvariablevalueid", value);
                    if (value.HasValue)
                    {
                        base.Id = value.Value;
                    }
                    else
                    {
                        base.Id = System.Guid.Empty;
                    }
                }
            }
                        
            public override System.Guid Id
            {
                get
                {
                    return base.Id;
                }
                set
                {
                    this.EnvironmentVariableValueId = value;
                }
            }

           
            /// <summary>
            /// Contains the actual variable data.
            /// </summary>
            public string Value
            {
                get
                {
                    return this.GetAttributeValue<string>("value");
                }
                set
                {                   
                    this.SetAttributeValue("value", value);                    
                }
            }
        }

        #endregion Proxies

        public EnvironmentVariablesDataConnector() { }

        public IReadOnlyDictionary<string, string> LoadSettings(IDataService dataService)
        {
            var orgService = dataService.ToOrgService();

            // Load environment variable definitions
            var definitions = orgService.Query<EnvironmentVariableDefinition>()
                .Select(cols => new { cols.EnvironmentVariableDefinitionId, cols.DisplayName, cols.DefaultValue })
                .WhereAll(e => e.IsActive())
                .RetrieveAll();

            // Load any override values
            var overrideValues = orgService.Query<EnvironmentVariableValue>()
                .Select(cols => new { cols.EnvironmentVariableDefinitionId, cols.Value })
                .WhereAll(e => e.IsActive())
                .RetrieveAll();

            // Build a dictionary 
            Dictionary<string, string> entries = new Dictionary<string, string>(definitions.Count);
           
            foreach(var definition in definitions)
            {
                var key = definition.DisplayName;
               
                // Use override value if present otherwise default value from definition.
                var value = overrideValues
                    .Where(v => v.EnvironmentVariableDefinitionId.Id == definition.Id)
                    .FirstOrDefault()?.Value ?? definition.DefaultValue;
               
                // Only include in the returned data if value is not null or empty.
                if (!string.IsNullOrEmpty(value))
                {
                    entries.Add(key, value);
                }
            }

            return entries;
        }
    }
}
