// 
// Code generated by a template.
//
using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace CCLLC.CDS.Sdk.Metadata.Proxy 
{
	[EntityLogicalName("sdkmessagerequest")]
	public partial class SdkMessageRequest : Entity
	{
		public static string EntityLogicalName => "sdkmessagerequest";

		public SdkMessageRequest()
			: base("sdkmessagerequest") {}


		#region Late Bound Field Constants
	
		public class Fields
		{			
			public const string Id = "sdkmessagerequestid";
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedOnBehalfByName = "createdonbehalfbyname";
			public const string CreatedOnBehalfByYomiName = "createdonbehalfbyyominame";
			public const string CustomizationLevel = "customizationlevel";
			public const string IntroducedVersion = "introducedversion";
			public const string IsManaged = "ismanaged";
			public const string IsManagedName = "ismanagedname";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedOnBehalfByName = "modifiedonbehalfbyname";
			public const string ModifiedOnBehalfByYomiName = "modifiedonbehalfbyyominame";
			public const string Name = "name";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string PrimaryObjectTypeCode = "primaryobjecttypecode";
			public const string SdkMessagePairId = "sdkmessagepairid";
			public const string SdkMessageRequestId = "sdkmessagerequestid";
			public const string SdkMessageRequestIdUnique = "sdkmessagerequestidunique";
			public const string SolutionId = "solutionid";
			public const string SupportingSolutionId = "supportingsolutionid";
			public const string VersionNumber = "versionnumber";
		}

		#endregion

		[AttributeLogicalName("sdkmessagerequestid")]
		public override Guid Id
		{
			get => base.Id; 
			set => SdkMessageRequestId = value;
		}

		[AttributeLogicalName("componentstate")]
		public virtual GlobalEnums.eComponentstate? ComponentState
		{
			get 
			{
				var value = GetAttributeValue<OptionSetValue>("componentstate"); 
				if(value is null) return null;
				return (GlobalEnums.eComponentstate?)value.Value;
			}
		}
		[AttributeLogicalName("createdby")]
		public virtual EntityReference CreatedBy
		{
			get => GetAttributeValue<EntityReference>("createdby"); 
		}
		[AttributeLogicalName("createdon")]
		public virtual DateTime? CreatedOn
		{
			get => GetAttributeValue<DateTime>("createdon"); 
		}
		[AttributeLogicalName("createdonbehalfby")]
		public virtual EntityReference CreatedOnBehalfBy
		{
			get => GetAttributeValue<EntityReference>("createdonbehalfby"); 
		}
		[AttributeLogicalName("createdonbehalfbyname")]
		public virtual string CreatedOnBehalfByName
		{
			get => GetAttributeValue<string>("createdonbehalfbyname"); 
		}
		[AttributeLogicalName("createdonbehalfbyyominame")]
		public virtual string CreatedOnBehalfByYomiName
		{
			get => GetAttributeValue<string>("createdonbehalfbyyominame"); 
		}
		[AttributeLogicalName("customizationlevel")]
		public virtual int? CustomizationLevel
		{
			get => GetAttributeValue<int>("customizationlevel"); 
		}
		[AttributeLogicalName("introducedversion")]
		public virtual string IntroducedVersion
		{
			get => GetAttributeValue<string>("introducedversion"); 
			set => SetAttributeValue("introducedversion", value); 
		}
		[AttributeLogicalName("ismanaged")]
		public virtual bool? IsManaged
		{
			get => GetAttributeValue<bool>("ismanaged"); 
		}
		[AttributeLogicalName("ismanagedname")]
		public virtual string IsManagedName
		{
			get => GetAttributeValue<string>("ismanagedname"); 
		}
		[AttributeLogicalName("modifiedby")]
		public virtual EntityReference ModifiedBy
		{
			get => GetAttributeValue<EntityReference>("modifiedby"); 
		}
		[AttributeLogicalName("modifiedon")]
		public virtual DateTime? ModifiedOn
		{
			get => GetAttributeValue<DateTime>("modifiedon"); 
		}
		[AttributeLogicalName("modifiedonbehalfby")]
		public virtual EntityReference ModifiedOnBehalfBy
		{
			get => GetAttributeValue<EntityReference>("modifiedonbehalfby"); 
		}
		[AttributeLogicalName("modifiedonbehalfbyname")]
		public virtual string ModifiedOnBehalfByName
		{
			get => GetAttributeValue<string>("modifiedonbehalfbyname"); 
		}
		[AttributeLogicalName("modifiedonbehalfbyyominame")]
		public virtual string ModifiedOnBehalfByYomiName
		{
			get => GetAttributeValue<string>("modifiedonbehalfbyyominame"); 
		}
		[AttributeLogicalName("name")]
		public virtual string Name
		{
			get => GetAttributeValue<string>("name"); 
			set => SetAttributeValue("name", value); 
		}
		[AttributeLogicalName("organizationid")]
		public virtual EntityReference OrganizationId
		{
			get => GetAttributeValue<EntityReference>("organizationid"); 
		}
		[AttributeLogicalName("overwritetime")]
		public virtual DateTime? OverwriteTime
		{
			get => GetAttributeValue<DateTime>("overwritetime"); 
		}
		[AttributeLogicalName("primaryobjecttypecode")]
		public virtual string PrimaryObjectTypeCode
		{
			get => GetAttributeValue<string>("primaryobjecttypecode"); 
		}
		[AttributeLogicalName("sdkmessagepairid")]
		public virtual EntityReference SdkMessagePairId
		{
			get => GetAttributeValue<EntityReference>("sdkmessagepairid"); 
		}
		[AttributeLogicalName("sdkmessagerequestid")]
		public virtual Guid SdkMessageRequestId
		{
			get => GetAttributeValue<Guid>("sdkmessagerequestid"); 
			set => SetAttributeValue("sdkmessagerequestid", value); 
		}
		[AttributeLogicalName("sdkmessagerequestidunique")]
		public virtual object SdkMessageRequestIdUnique
		{
			get => GetAttributeValue<object>("sdkmessagerequestidunique"); 
		}
		[AttributeLogicalName("solutionid")]
		public virtual object SolutionId
		{
			get => GetAttributeValue<object>("solutionid"); 
		}
		[AttributeLogicalName("supportingsolutionid")]
		public virtual object SupportingSolutionId
		{
			get => GetAttributeValue<object>("supportingsolutionid"); 
		}
		[AttributeLogicalName("versionnumber")]
		public virtual int? VersionNumber
		{
			get => GetAttributeValue<int>("versionnumber"); 
		}
	}
}
