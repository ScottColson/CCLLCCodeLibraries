namespace CCLLC.CDS.Sdk.Metadata.Proxy
{

	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("sdkmessageresponse")]	
	internal class SdkMessageResponse : Microsoft.Xrm.Sdk.Entity
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
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string SdkMessageRequestId = "sdkmessagerequestid";
			public const string SdkMessageResponseId = "sdkmessageresponseid";
			public const string Id = "sdkmessageresponseid";
			public const string SdkMessageResponseIdUnique = "sdkmessageresponseidunique";
			public const string SolutionId = "solutionid";
			public const string VersionNumber = "versionnumber";
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public SdkMessageResponse() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "sdkmessageresponse";
		
		public const string EntitySchemaName = "SdkMessageResponse";
		
		public const string PrimaryIdAttribute = "sdkmessageresponseid";	

		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessageresponseid")]
		public System.Nullable<System.Guid> SdkMessageResponseId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("sdkmessageresponseid");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessageresponseid")]
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