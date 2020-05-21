namespace CCLLC.CDS.Sdk.Metadata.Proxy
{	
	
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("sdkmessagerequest")]
	internal class SdkMessageRequest : Microsoft.Xrm.Sdk.Entity
	{
		
		public static class Fields
		{
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CustomizationLevel = "customizationlevel";
			public const string IntroducedVersion = "introducedversion";
			public const string IsManaged = "ismanaged";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string Name = "name";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string PrimaryObjectTypeCode = "primaryobjecttypecode";
			public const string SdkMessagePairId = "sdkmessagepairid";
			public const string SdkMessageRequestId = "sdkmessagerequestid";
			public const string Id = "sdkmessagerequestid";
			public const string SdkMessageRequestIdUnique = "sdkmessagerequestidunique";
			public const string SolutionId = "solutionid";
			public const string VersionNumber = "versionnumber";
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public SdkMessageRequest() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "sdkmessagerequest";
		
		public const string EntitySchemaName = "SdkMessageRequest";
		
		public const string PrimaryIdAttribute = "sdkmessagerequestid";
		
		
		/// <summary>
		/// Name of the SDK message request.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("name")]
		public string Name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("name");
			}			
		}
		
		/// <summary>
		/// Unique identifier of the SDK message request entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagerequestid")]
		public System.Nullable<System.Guid> SdkMessageRequestId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("sdkmessagerequestid");
			}
			
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagerequestid")]
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