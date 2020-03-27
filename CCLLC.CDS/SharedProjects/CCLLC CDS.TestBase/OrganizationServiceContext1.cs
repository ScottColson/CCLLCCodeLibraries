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
		/// Gets a binding to the set of all <see cref="TestProxy.Account"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.Account> AccountSet
		{
			get
			{
				return this.CreateQuery<TestProxy.Account>();
			}
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
		/// Gets a binding to the set of all <see cref="TestProxy.Contact"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.Contact> ContactSet
		{
			get
			{
				return this.CreateQuery<TestProxy.Contact>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="TestProxy.EnvironmentVariableDefinition"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.EnvironmentVariableDefinition> EnvironmentVariableDefinitionSet
		{
			get
			{
				return this.CreateQuery<TestProxy.EnvironmentVariableDefinition>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="TestProxy.EnvironmentVariableValue"/> entities.
		/// </summary>
		public System.Linq.IQueryable<TestProxy.EnvironmentVariableValue> EnvironmentVariableValueSet
		{
			get
			{
				return this.CreateQuery<TestProxy.EnvironmentVariableValue>();
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
