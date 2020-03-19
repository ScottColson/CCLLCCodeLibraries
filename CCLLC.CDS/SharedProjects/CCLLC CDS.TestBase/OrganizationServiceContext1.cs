namespace TestProxy
{

	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.41")]
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
		/// Gets a binding to the set of all <see cref="TestProxy.BusinessUnit"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.BusinessUnit> BusinessUnitSet
		{
			get
			{
				return this.CreateQuery<TestProxy.BusinessUnit>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="TestProxy.ccllc_extensionsettings"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.ccllc_extensionsettings> ccllc_extensionsettingsSet
		{
			get
			{
				return this.CreateQuery<TestProxy.ccllc_extensionsettings>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="TestProxy.SystemUser"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.SystemUser> SystemUserSet
		{
			get
			{
				return this.CreateQuery<TestProxy.SystemUser>();
			}
		}
	}
}
