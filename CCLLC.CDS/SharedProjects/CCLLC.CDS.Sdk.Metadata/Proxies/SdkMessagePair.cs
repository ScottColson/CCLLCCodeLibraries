namespace CCLLC.CDS.Sdk.Metadata.Proxy
{
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("sdkmessagepair")]
	internal class SdkMessagePair : Microsoft.Xrm.Sdk.Entity
	{		
		public static class Fields
		{
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CustomizationLevel = "customizationlevel";
			public const string DeprecatedVersion = "deprecatedversion";
			public const string Endpoint = "endpoint";
			public const string IntroducedVersion = "introducedversion";
			public const string IsManaged = "ismanaged";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string Namespace = "namespace";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string SdkMessageBindingInformation = "sdkmessagebindinginformation";
			public const string SdkMessageId = "sdkmessageid";
			public const string SdkMessagePairId = "sdkmessagepairid";
			public const string Id = "sdkmessagepairid";
			public const string SdkMessagePairIdUnique = "sdkmessagepairidunique";
			public const string SolutionId = "solutionid";
			public const string VersionNumber = "versionnumber";
		}
		

		[System.Diagnostics.DebuggerNonUserCode()]
		public SdkMessagePair() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "sdkmessagepair";
		
		public const string EntitySchemaName = "SdkMessagePair";
		
		public const string PrimaryIdAttribute = "sdkmessagepairid";		
	
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("endpoint")]
		public string Endpoint
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("endpoint");
			}			
		}
				
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ismanaged")]
		public System.Nullable<bool> IsManaged
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("ismanaged");
			}
		}		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("namespace")]
		public string Namespace
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("namespace");
			}		
		}					
		
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagepairid")]
		public System.Nullable<System.Guid> SdkMessagePairId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("sdkmessagepairid");
			}			
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagepairid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}			
		}
		
		
	}
}