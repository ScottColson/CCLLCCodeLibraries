namespace CCLLC.CDS.Sdk.Metadata.Proxy
{
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("sdkmessagefilter")]
	internal class SdkMessageFilter : Microsoft.Xrm.Sdk.Entity
	{		
		public static class Fields
		{
			public const string Availability = "availability";		
			public const string CustomizationLevel = "customizationlevel";
			public const string IntroducedVersion = "introducedversion";
			public const string IsCustomProcessingStepAllowed = "iscustomprocessingstepallowed";
			public const string IsManaged = "ismanaged";
			public const string IsVisible = "isvisible";
			public const string PrimaryObjectTypeCode = "primaryobjecttypecode";
			public const string RestrictionLevel = "restrictionlevel";
			public const string SdkMessageFilterId = "sdkmessagefilterid";
			public const string SdkMessageFilterIdUnique = "sdkmessagefilteridunique";
			public const string SdkMessageId = "sdkmessageid";
			public const string SecondaryObjectTypeCode = "secondaryobjecttypecode";
			public const string SolutionId = "solutionid";
			public const string VersionNumber = "versionnumber";
		}
		
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public SdkMessageFilter() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "sdkmessagefilter";
		
		public const string EntitySchemaName = "SdkMessageFilter";
		
		public const string PrimaryIdAttribute = "sdkmessagefilterid";
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isvisible")]
		public System.Nullable<bool> IsVisible
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("isvisible");
			}
		}		
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("primaryobjecttypecode")]
		public string PrimaryObjectTypeCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("primaryobjecttypecode");
			}
		}
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagefilterid")]
		public System.Nullable<System.Guid> SdkMessageFilterId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("sdkmessagefilterid");
			}
			
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagefilterid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}			
		}
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("secondaryobjecttypecode")]
		public string SecondaryObjectTypeCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("secondaryobjecttypecode");
			}
		}	
	}
}