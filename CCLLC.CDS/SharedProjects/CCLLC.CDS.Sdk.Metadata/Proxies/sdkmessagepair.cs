// 
// Code generated by a template.
//
using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace CCLLC.CDS.Sdk.Metadata.Proxy 
{
	[EntityLogicalName("sdkmessagepair")]
	public partial class SdkMessagePair : Entity
	{
		public static string EntityLogicalName => "sdkmessagepair";

		public SdkMessagePair()
			: base("sdkmessagepair") {}


		#region Late Bound Field Constants
	
		public class Fields
		{			
			public const string Id = "sdkmessagepairid";
			public const string Namespace = "namespace";
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedOnBehalfByName = "createdonbehalfbyname";
			public const string CreatedOnBehalfByYomiName = "createdonbehalfbyyominame";
			public const string CustomizationLevel = "customizationlevel";
			public const string DeprecatedVersion = "deprecatedversion";
			public const string Endpoint = "endpoint";
			public const string IntroducedVersion = "introducedversion";
			public const string IsManaged = "ismanaged";
			public const string IsManagedName = "ismanagedname";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedOnBehalfByName = "modifiedonbehalfbyname";
			public const string ModifiedOnBehalfByYomiName = "modifiedonbehalfbyyominame";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string SdkMessageBindingInformation = "sdkmessagebindinginformation";
			public const string SdkMessageId = "sdkmessageid";
			public const string SdkMessagePairId = "sdkmessagepairid";
			public const string SdkMessagePairIdUnique = "sdkmessagepairidunique";
			public const string SolutionId = "solutionid";
			public const string SupportingSolutionId = "supportingsolutionid";
			public const string VersionNumber = "versionnumber";
		}

		#endregion

		[AttributeLogicalName("sdkmessagepairid")]
		public override Guid Id
		{
			get => base.Id; 
			set => SdkMessagePairId = value;
		}

		[AttributeLogicalName("namespace")]
		public virtual string Namespace
		{
			get => GetAttributeValue<string>("namespace"); 
			set => SetAttributeValue("namespace", value); 
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
		[AttributeLogicalName("deprecatedversion")]
		public virtual string DeprecatedVersion
		{
			get => GetAttributeValue<string>("deprecatedversion"); 
			set => SetAttributeValue("deprecatedversion", value); 
		}
		[AttributeLogicalName("endpoint")]
		public virtual string Endpoint
		{
			get => GetAttributeValue<string>("endpoint"); 
			set => SetAttributeValue("endpoint", value); 
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
		[AttributeLogicalName("sdkmessagebindinginformation")]
		public virtual string SdkMessageBindingInformation
		{
			get => GetAttributeValue<string>("sdkmessagebindinginformation"); 
			set => SetAttributeValue("sdkmessagebindinginformation", value); 
		}
		[AttributeLogicalName("sdkmessageid")]
		public virtual EntityReference SdkMessageId
		{
			get => GetAttributeValue<EntityReference>("sdkmessageid"); 
		}
		[AttributeLogicalName("sdkmessagepairid")]
		public virtual Guid SdkMessagePairId
		{
			get => GetAttributeValue<Guid>("sdkmessagepairid"); 
			set => SetAttributeValue("sdkmessagepairid", value); 
		}
		[AttributeLogicalName("sdkmessagepairidunique")]
		public virtual object SdkMessagePairIdUnique
		{
			get => GetAttributeValue<object>("sdkmessagepairidunique"); 
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