namespace CCLLC.CDS.Sdk.Metadata.Proxy
{	
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("sdkmessagerequestfield")]
	internal class SdkMessageRequestField : Microsoft.Xrm.Sdk.Entity
	{
		
		public static class Fields
		{
			public const string ClrParser = "clrparser";
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CustomizationLevel = "customizationlevel";
			public const string FieldMask = "fieldmask";
			public const string IntroducedVersion = "introducedversion";
			public const string IsManaged = "ismanaged";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string Name = "name";
			public const string Optional = "optional";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string ParameterBindingInformation = "parameterbindinginformation";
			public const string Parser = "parser";
			public const string Position = "position";
			public const string PublicName = "publicname";
			public const string SdkMessageRequestFieldId = "sdkmessagerequestfieldid";
			public const string Id = "sdkmessagerequestfieldid";
			public const string SdkMessageRequestFieldIdUnique = "sdkmessagerequestfieldidunique";
			public const string SdkMessageRequestId = "sdkmessagerequestid";
			public const string SolutionId = "solutionid";
			public const string VersionNumber = "versionnumber";
		}
		
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public SdkMessageRequestField() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "sdkmessagerequestfield";
		
		public const string EntitySchemaName = "SdkMessageRequestField";
		
		public const string PrimaryIdAttribute = "sdkmessagerequestfieldid";
			
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("clrparser")]
		public string ClrParser
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("clrparser");
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
		
		/// <summary>
		/// Information about whether SDK message request field is optional.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("optional")]
		public System.Nullable<bool> Optional
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("optional");
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
		
		/// <summary>
		/// Public name of the SDK message request field.
		/// </summary>
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
		/// Unique identifier of the SDK message request field entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagerequestfieldid")]
		public System.Nullable<System.Guid> SdkMessageRequestFieldId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("sdkmessagerequestfieldid");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sdkmessagerequestfieldid")]
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