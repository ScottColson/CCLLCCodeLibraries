namespace CCLLC.CDS.Sdk.Metadata.Proxy
{
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("sdkmessageresponsefield")]
	internal class SdkMessageResponseField : Microsoft.Xrm.Sdk.Entity
	{
		
		public static class Fields
		{
			public const string ClrFormatter = "clrformatter";
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CustomizationLevel = "customizationlevel";
			public const string Formatter = "formatter";
			public const string IntroducedVersion = "introducedversion";
			public const string IsManaged = "ismanaged";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string Name = "name";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string ParameterBindingInformation = "parameterbindinginformation";
			public const string Position = "position";
			public const string PublicName = "publicname";
			public const string SdkMessageResponseFieldId = "sdkmessageresponsefieldid";
			public const string Id = "sdkmessageresponsefieldid";
			public const string SdkMessageResponseFieldIdUnique = "sdkmessageresponsefieldidunique";
			public const string SdkMessageResponseId = "sdkmessageresponseid";
			public const string SolutionId = "solutionid";
			public const string Value = "value";
			public const string VersionNumber = "versionnumber";
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public SdkMessageResponseField() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "sdkmessageresponsefield";
		
		public const string EntitySchemaName = "SdkMessageResponseField";
		
		public const string PrimaryIdAttribute = "sdkmessageresponsefieldid";
		
		
		/// <summary>
		/// Common language runtime (CLR)-based formatter of the SDK message response field.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("clrformatter")]
		public string ClrFormatter
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("clrformatter");
			}			
		}
		
		
		/// <summary>
		/// Customization level of the SDK message response field.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("customizationlevel")]
		public System.Nullable<int> CustomizationLevel
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("customizationlevel");
			}
		}
		
		/// <summary>
		/// Formatter for the SDK message response field.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("formatter")]
		public string Formatter
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("formatter");
			}			
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("name")]
		public string Name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("name");
			}
		}
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("position")]
		public System.Nullable<int> Position
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("position");
			}
		}
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("publicname")]
		public string PublicName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("publicname");
			}			
		}
		
		/// <summary>
		/// Unique identifier of the SDK message response field entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessageresponsefieldid")]
		public System.Nullable<System.Guid> SdkMessageResponseFieldId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("sdkmessageresponsefieldid");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessageresponsefieldid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}
		}
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("value")]
		public string Value
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("value");
			}
		}	
		
	}
}