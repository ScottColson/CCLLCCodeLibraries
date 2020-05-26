// 
// Code generated by a template.
//
using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace CCLLC.CDS.Sdk.Metadata.Proxy 
{

	[EntityLogicalNameAttribute("sdkmessagerequest")]
	public partial class SdkMessageRequest : Entity
	{
		public static string EntityLogicalName => "sdkmessagerequest";

		public SdkMessageRequest()
			: base("sdkmessagerequest")
		{
		}

		#region Late Bound Fields
	
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
			public const string SdkMessagePairId = "sdkmessagepairid";
			public const string SdkMessageRequestId = "sdkmessagerequestid";
			public const string SdkMessageRequestIdUnique = "sdkmessagerequestidunique";
			public const string SolutionId = "solutionid";
			public const string SupportingSolutionId = "supportingsolutionid";
			public const string VersionNumber = "versionnumber";
		}

		#endregion

		[AttributeLogicalNameAttribute("sdkmessagerequestid")]
		public override Guid Id
		{
			get { return base.Id; }
			set { SdkMessageRequestId = value; }
		}

		[AttributeLogicalNameAttribute("componentstate")]
		public virtual OptionSetValue ComponentState
		{
			get { return this.GetAttributeValue<OptionSetValue>("componentstate"); }
		}
		[AttributeLogicalNameAttribute("createdby")]
		public virtual EntityReference CreatedBy
		{
			get { return this.GetAttributeValue<EntityReference>("createdby"); }
		}
		[AttributeLogicalNameAttribute("createdon")]
		public virtual DateTime? CreatedOn
		{
			get { return this.GetAttributeValue<DateTime?>("createdon"); }
		}
		[AttributeLogicalNameAttribute("createdonbehalfby")]
		public virtual EntityReference CreatedOnBehalfBy
		{
			get { return this.GetAttributeValue<EntityReference>("createdonbehalfby"); }
		}
		[AttributeLogicalNameAttribute("createdonbehalfbyname")]
		public virtual string CreatedOnBehalfByName
		{
			get { return this.GetAttributeValue<string>("createdonbehalfbyname"); }
		}
		[AttributeLogicalNameAttribute("createdonbehalfbyyominame")]
		public virtual string CreatedOnBehalfByYomiName
		{
			get { return this.GetAttributeValue<string>("createdonbehalfbyyominame"); }
		}
		[AttributeLogicalNameAttribute("customizationlevel")]
		public virtual int? CustomizationLevel
		{
			get { return this.GetAttributeValue<int?>("customizationlevel"); }
		}
		[AttributeLogicalNameAttribute("introducedversion")]
		public virtual string IntroducedVersion
		{
			get { return this.GetAttributeValue<string>("introducedversion"); }
			set { this.SetAttributeValue("introducedversion", value); }
		}
		[AttributeLogicalNameAttribute("ismanaged")]
		public virtual bool? IsManaged
		{
			get { return this.GetAttributeValue<bool?>("ismanaged"); }
		}
		[AttributeLogicalNameAttribute("ismanagedname")]
		public virtual string IsManagedName
		{
			get { return this.GetAttributeValue<string>("ismanagedname"); }
		}
		[AttributeLogicalNameAttribute("modifiedby")]
		public virtual EntityReference ModifiedBy
		{
			get { return this.GetAttributeValue<EntityReference>("modifiedby"); }
		}
		[AttributeLogicalNameAttribute("modifiedon")]
		public virtual DateTime? ModifiedOn
		{
			get { return this.GetAttributeValue<DateTime?>("modifiedon"); }
		}
		[AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public virtual EntityReference ModifiedOnBehalfBy
		{
			get { return this.GetAttributeValue<EntityReference>("modifiedonbehalfby"); }
		}
		[AttributeLogicalNameAttribute("modifiedonbehalfbyname")]
		public virtual string ModifiedOnBehalfByName
		{
			get { return this.GetAttributeValue<string>("modifiedonbehalfbyname"); }
		}
		[AttributeLogicalNameAttribute("modifiedonbehalfbyyominame")]
		public virtual string ModifiedOnBehalfByYomiName
		{
			get { return this.GetAttributeValue<string>("modifiedonbehalfbyyominame"); }
		}
		[AttributeLogicalNameAttribute("name")]
		public virtual string Name
		{
			get { return this.GetAttributeValue<string>("name"); }
			set { this.SetAttributeValue("name", value); }
		}
		[AttributeLogicalNameAttribute("organizationid")]
		public virtual EntityReference OrganizationId
		{
			get { return this.GetAttributeValue<EntityReference>("organizationid"); }
		}
		[AttributeLogicalNameAttribute("overwritetime")]
		public virtual DateTime? OverwriteTime
		{
			get { return this.GetAttributeValue<DateTime?>("overwritetime"); }
		}
		[AttributeLogicalNameAttribute("sdkmessagepairid")]
		public virtual EntityReference SdkMessagePairId
		{
			get { return this.GetAttributeValue<EntityReference>("sdkmessagepairid"); }
		}
		[AttributeLogicalNameAttribute("sdkmessagerequestid")]
		public virtual Guid SdkMessageRequestId
		{
			get { return this.GetAttributeValue<Guid>("sdkmessagerequestid"); }
			set { this.SetAttributeValue("sdkmessagerequestid", value); }
		}
		[AttributeLogicalNameAttribute("sdkmessagerequestidunique")]
		public virtual object SdkMessageRequestIdUnique
		{
			get { return this.GetAttributeValue<object>("sdkmessagerequestidunique"); }
		}
		[AttributeLogicalNameAttribute("solutionid")]
		public virtual object SolutionId
		{
			get { return this.GetAttributeValue<object>("solutionid"); }
		}
		[AttributeLogicalNameAttribute("supportingsolutionid")]
		public virtual object SupportingSolutionId
		{
			get { return this.GetAttributeValue<object>("supportingsolutionid"); }
		}
		[AttributeLogicalNameAttribute("versionnumber")]
		public virtual int? VersionNumber
		{
			get { return this.GetAttributeValue<int?>("versionnumber"); }
		}
	}
}
