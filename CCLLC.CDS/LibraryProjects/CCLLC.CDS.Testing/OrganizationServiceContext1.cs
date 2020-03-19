namespace S3.D365.Testing
{

	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.0.0.9154")]
	public partial class OrganizationServiceContext1 : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		public OrganizationServiceContext1(Microsoft.Xrm.Sdk.IOrganizationService service) : 
				base(service)
		{
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.Account"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.Account> AccountSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.Account>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.BusinessUnit"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.BusinessUnit> BusinessUnitSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.BusinessUnit>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.Contact"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.Contact> ContactSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.Contact>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.CustomerAddress"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.CustomerAddress> CustomerAddressSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.CustomerAddress>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_agent"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_agent> s3_agentSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_agent>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_agentauthorizedcustomer"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_agentauthorizedcustomer> s3_agentauthorizedcustomerSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_agentauthorizedcustomer>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_agentprohibitedcustomer"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_agentprohibitedcustomer> s3_agentprohibitedcustomerSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_agentprohibitedcustomer>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_agentrole"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_agentrole> s3_agentroleSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_agentrole>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_alternatebranch"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_alternatebranch> s3_alternatebranchSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_alternatebranch>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_appliedfee"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_appliedfee> s3_appliedfeeSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_appliedfee>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_channel"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_channel> s3_channelSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_channel>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_device"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_device> s3_deviceSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_device>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_evaluatortype"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_evaluatortype> s3_evaluatortypeSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_evaluatortype>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_fee"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_fee> s3_feeSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_fee>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_location"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_location> s3_locationSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_location>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_partner"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_partner> s3_partnerSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_partner>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_partnerworker"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_partnerworker> s3_partnerworkerSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_partnerworker>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_processauthorizedchannel"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_processauthorizedchannel> s3_processauthorizedchannelSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_processauthorizedchannel>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_processstep"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_processstep> s3_processstepSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_processstep>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_processstepauthorizedchannel"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_processstepauthorizedchannel> s3_processstepauthorizedchannelSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_processstepauthorizedchannel>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_processsteprequirement"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_processsteprequirement> s3_processsteprequirementSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_processsteprequirement>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_processsteptype"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_processsteptype> s3_processsteptypeSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_processsteptype>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_requirementwaiverrole"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_requirementwaiverrole> s3_requirementwaiverroleSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_requirementwaiverrole>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_role"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_role> s3_roleSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_role>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_stephistory"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_stephistory> s3_stephistorySet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_stephistory>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transaction"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transaction> s3_transactionSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transaction>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactiongroup"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactiongroup> s3_transactiongroupSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactiongroup>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactioninitialfee"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactioninitialfee> s3_transactioninitialfeeSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactioninitialfee>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactionprocess"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactionprocess> s3_transactionprocessSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactionprocess>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactionrequirement"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactionrequirement> s3_transactionrequirementSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactionrequirement>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactionrequirementwaiverrole"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactionrequirementwaiverrole> s3_transactionrequirementwaiverroleSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactionrequirementwaiverrole>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactiontype"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactiontype> s3_transactiontypeSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactiontype>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactiontypeauthorizedchannel"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactiontypeauthorizedchannel> s3_transactiontypeauthorizedchannelSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactiontypeauthorizedchannel>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactiontypeauthorizedrole"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactiontypeauthorizedrole> s3_transactiontypeauthorizedroleSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactiontypeauthorizedrole>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_transactiontypecontext"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_transactiontypecontext> s3_transactiontypecontextSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_transactiontypecontext>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.s3_worksession"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.s3_worksession> s3_worksessionSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.s3_worksession>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="S3.D365.Testing.SystemUser"/> entities.
		/// </summary>
		public System.Linq.IQueryable<S3.D365.Testing.SystemUser> SystemUserSet
		{
			get
			{
				return this.CreateQuery<S3.D365.Testing.SystemUser>();
			}
		}
	}
}
