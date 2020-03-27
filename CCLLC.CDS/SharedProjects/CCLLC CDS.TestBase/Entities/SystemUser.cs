namespace TestProxy
{

	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("systemuser")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.41")]
	public partial class SystemUser : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public SystemUser() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "systemuser";
		
		public const int EntityTypeCode = 8;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		private void OnPropertyChanged(string propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void OnPropertyChanging(string propertyName)
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
			}
		}
		
		/// <summary>
		/// Type of user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("accessmode")]
		public System.Nullable<TestProxy.systemuser_accessmode> AccessMode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("accessmode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_accessmode)(System.Enum.ToObject(typeof(TestProxy.systemuser_accessmode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("AccessMode");
				if ((value == null))
				{
					this.SetAttributeValue("accessmode", null);
				}
				else
				{
					this.SetAttributeValue("accessmode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("AccessMode");
			}
		}
		
		/// <summary>
		/// Unique identifier for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_addressid")]
		public System.Nullable<System.Guid> Address1_AddressId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("address1_addressid");
			}
			set
			{
				this.OnPropertyChanging("Address1_AddressId");
				this.SetAttributeValue("address1_addressid", value);
				this.OnPropertyChanged("Address1_AddressId");
			}
		}
		
		/// <summary>
		/// Type of address for address 1, such as billing, shipping, or primary address.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_addresstypecode")]
		public System.Nullable<TestProxy.systemuser_address1_addresstypecode> Address1_AddressTypeCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("address1_addresstypecode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_address1_addresstypecode)(System.Enum.ToObject(typeof(TestProxy.systemuser_address1_addresstypecode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("Address1_AddressTypeCode");
				if ((value == null))
				{
					this.SetAttributeValue("address1_addresstypecode", null);
				}
				else
				{
					this.SetAttributeValue("address1_addresstypecode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("Address1_AddressTypeCode");
			}
		}
		
		/// <summary>
		/// City name for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_city")]
		public string Address1_City
		{
			get
			{
				return this.GetAttributeValue<string>("address1_city");
			}
			set
			{
				this.OnPropertyChanging("Address1_City");
				this.SetAttributeValue("address1_city", value);
				this.OnPropertyChanged("Address1_City");
			}
		}
		
		/// <summary>
		/// Shows the complete primary address.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_composite")]
		public string Address1_Composite
		{
			get
			{
				return this.GetAttributeValue<string>("address1_composite");
			}
		}
		
		/// <summary>
		/// Country/region name in address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_country")]
		public string Address1_Country
		{
			get
			{
				return this.GetAttributeValue<string>("address1_country");
			}
			set
			{
				this.OnPropertyChanging("Address1_Country");
				this.SetAttributeValue("address1_country", value);
				this.OnPropertyChanged("Address1_Country");
			}
		}
		
		/// <summary>
		/// County name for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_county")]
		public string Address1_County
		{
			get
			{
				return this.GetAttributeValue<string>("address1_county");
			}
			set
			{
				this.OnPropertyChanging("Address1_County");
				this.SetAttributeValue("address1_county", value);
				this.OnPropertyChanged("Address1_County");
			}
		}
		
		/// <summary>
		/// Fax number for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_fax")]
		public string Address1_Fax
		{
			get
			{
				return this.GetAttributeValue<string>("address1_fax");
			}
			set
			{
				this.OnPropertyChanging("Address1_Fax");
				this.SetAttributeValue("address1_fax", value);
				this.OnPropertyChanged("Address1_Fax");
			}
		}
		
		/// <summary>
		/// Latitude for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_latitude")]
		public System.Nullable<double> Address1_Latitude
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<double>>("address1_latitude");
			}
			set
			{
				this.OnPropertyChanging("Address1_Latitude");
				this.SetAttributeValue("address1_latitude", value);
				this.OnPropertyChanged("Address1_Latitude");
			}
		}
		
		/// <summary>
		/// First line for entering address 1 information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_line1")]
		public string Address1_Line1
		{
			get
			{
				return this.GetAttributeValue<string>("address1_line1");
			}
			set
			{
				this.OnPropertyChanging("Address1_Line1");
				this.SetAttributeValue("address1_line1", value);
				this.OnPropertyChanged("Address1_Line1");
			}
		}
		
		/// <summary>
		/// Second line for entering address 1 information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_line2")]
		public string Address1_Line2
		{
			get
			{
				return this.GetAttributeValue<string>("address1_line2");
			}
			set
			{
				this.OnPropertyChanging("Address1_Line2");
				this.SetAttributeValue("address1_line2", value);
				this.OnPropertyChanged("Address1_Line2");
			}
		}
		
		/// <summary>
		/// Third line for entering address 1 information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_line3")]
		public string Address1_Line3
		{
			get
			{
				return this.GetAttributeValue<string>("address1_line3");
			}
			set
			{
				this.OnPropertyChanging("Address1_Line3");
				this.SetAttributeValue("address1_line3", value);
				this.OnPropertyChanged("Address1_Line3");
			}
		}
		
		/// <summary>
		/// Longitude for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_longitude")]
		public System.Nullable<double> Address1_Longitude
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<double>>("address1_longitude");
			}
			set
			{
				this.OnPropertyChanging("Address1_Longitude");
				this.SetAttributeValue("address1_longitude", value);
				this.OnPropertyChanged("Address1_Longitude");
			}
		}
		
		/// <summary>
		/// Name to enter for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_name")]
		public string Address1_Name
		{
			get
			{
				return this.GetAttributeValue<string>("address1_name");
			}
			set
			{
				this.OnPropertyChanging("Address1_Name");
				this.SetAttributeValue("address1_name", value);
				this.OnPropertyChanged("Address1_Name");
			}
		}
		
		/// <summary>
		/// ZIP Code or postal code for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_postalcode")]
		public string Address1_PostalCode
		{
			get
			{
				return this.GetAttributeValue<string>("address1_postalcode");
			}
			set
			{
				this.OnPropertyChanging("Address1_PostalCode");
				this.SetAttributeValue("address1_postalcode", value);
				this.OnPropertyChanged("Address1_PostalCode");
			}
		}
		
		/// <summary>
		/// Post office box number for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_postofficebox")]
		public string Address1_PostOfficeBox
		{
			get
			{
				return this.GetAttributeValue<string>("address1_postofficebox");
			}
			set
			{
				this.OnPropertyChanging("Address1_PostOfficeBox");
				this.SetAttributeValue("address1_postofficebox", value);
				this.OnPropertyChanged("Address1_PostOfficeBox");
			}
		}
		
		/// <summary>
		/// Method of shipment for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_shippingmethodcode")]
		public System.Nullable<TestProxy.systemuser_address1_shippingmethodcode> Address1_ShippingMethodCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("address1_shippingmethodcode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_address1_shippingmethodcode)(System.Enum.ToObject(typeof(TestProxy.systemuser_address1_shippingmethodcode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("Address1_ShippingMethodCode");
				if ((value == null))
				{
					this.SetAttributeValue("address1_shippingmethodcode", null);
				}
				else
				{
					this.SetAttributeValue("address1_shippingmethodcode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("Address1_ShippingMethodCode");
			}
		}
		
		/// <summary>
		/// State or province for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_stateorprovince")]
		public string Address1_StateOrProvince
		{
			get
			{
				return this.GetAttributeValue<string>("address1_stateorprovince");
			}
			set
			{
				this.OnPropertyChanging("Address1_StateOrProvince");
				this.SetAttributeValue("address1_stateorprovince", value);
				this.OnPropertyChanged("Address1_StateOrProvince");
			}
		}
		
		/// <summary>
		/// First telephone number associated with address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_telephone1")]
		public string Address1_Telephone1
		{
			get
			{
				return this.GetAttributeValue<string>("address1_telephone1");
			}
			set
			{
				this.OnPropertyChanging("Address1_Telephone1");
				this.SetAttributeValue("address1_telephone1", value);
				this.OnPropertyChanged("Address1_Telephone1");
			}
		}
		
		/// <summary>
		/// Second telephone number associated with address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_telephone2")]
		public string Address1_Telephone2
		{
			get
			{
				return this.GetAttributeValue<string>("address1_telephone2");
			}
			set
			{
				this.OnPropertyChanging("Address1_Telephone2");
				this.SetAttributeValue("address1_telephone2", value);
				this.OnPropertyChanged("Address1_Telephone2");
			}
		}
		
		/// <summary>
		/// Third telephone number associated with address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_telephone3")]
		public string Address1_Telephone3
		{
			get
			{
				return this.GetAttributeValue<string>("address1_telephone3");
			}
			set
			{
				this.OnPropertyChanging("Address1_Telephone3");
				this.SetAttributeValue("address1_telephone3", value);
				this.OnPropertyChanged("Address1_Telephone3");
			}
		}
		
		/// <summary>
		/// United Parcel Service (UPS) zone for address 1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_upszone")]
		public string Address1_UPSZone
		{
			get
			{
				return this.GetAttributeValue<string>("address1_upszone");
			}
			set
			{
				this.OnPropertyChanging("Address1_UPSZone");
				this.SetAttributeValue("address1_upszone", value);
				this.OnPropertyChanged("Address1_UPSZone");
			}
		}
		
		/// <summary>
		/// UTC offset for address 1. This is the difference between local time and standard Coordinated Universal Time.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address1_utcoffset")]
		public System.Nullable<int> Address1_UTCOffset
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("address1_utcoffset");
			}
			set
			{
				this.OnPropertyChanging("Address1_UTCOffset");
				this.SetAttributeValue("address1_utcoffset", value);
				this.OnPropertyChanged("Address1_UTCOffset");
			}
		}
		
		/// <summary>
		/// Unique identifier for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_addressid")]
		public System.Nullable<System.Guid> Address2_AddressId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("address2_addressid");
			}
			set
			{
				this.OnPropertyChanging("Address2_AddressId");
				this.SetAttributeValue("address2_addressid", value);
				this.OnPropertyChanged("Address2_AddressId");
			}
		}
		
		/// <summary>
		/// Type of address for address 2, such as billing, shipping, or primary address.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_addresstypecode")]
		public System.Nullable<TestProxy.systemuser_address2_addresstypecode> Address2_AddressTypeCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("address2_addresstypecode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_address2_addresstypecode)(System.Enum.ToObject(typeof(TestProxy.systemuser_address2_addresstypecode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("Address2_AddressTypeCode");
				if ((value == null))
				{
					this.SetAttributeValue("address2_addresstypecode", null);
				}
				else
				{
					this.SetAttributeValue("address2_addresstypecode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("Address2_AddressTypeCode");
			}
		}
		
		/// <summary>
		/// City name for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_city")]
		public string Address2_City
		{
			get
			{
				return this.GetAttributeValue<string>("address2_city");
			}
			set
			{
				this.OnPropertyChanging("Address2_City");
				this.SetAttributeValue("address2_city", value);
				this.OnPropertyChanged("Address2_City");
			}
		}
		
		/// <summary>
		/// Shows the complete secondary address.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_composite")]
		public string Address2_Composite
		{
			get
			{
				return this.GetAttributeValue<string>("address2_composite");
			}
		}
		
		/// <summary>
		/// Country/region name in address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_country")]
		public string Address2_Country
		{
			get
			{
				return this.GetAttributeValue<string>("address2_country");
			}
			set
			{
				this.OnPropertyChanging("Address2_Country");
				this.SetAttributeValue("address2_country", value);
				this.OnPropertyChanged("Address2_Country");
			}
		}
		
		/// <summary>
		/// County name for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_county")]
		public string Address2_County
		{
			get
			{
				return this.GetAttributeValue<string>("address2_county");
			}
			set
			{
				this.OnPropertyChanging("Address2_County");
				this.SetAttributeValue("address2_county", value);
				this.OnPropertyChanged("Address2_County");
			}
		}
		
		/// <summary>
		/// Fax number for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_fax")]
		public string Address2_Fax
		{
			get
			{
				return this.GetAttributeValue<string>("address2_fax");
			}
			set
			{
				this.OnPropertyChanging("Address2_Fax");
				this.SetAttributeValue("address2_fax", value);
				this.OnPropertyChanged("Address2_Fax");
			}
		}
		
		/// <summary>
		/// Latitude for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_latitude")]
		public System.Nullable<double> Address2_Latitude
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<double>>("address2_latitude");
			}
			set
			{
				this.OnPropertyChanging("Address2_Latitude");
				this.SetAttributeValue("address2_latitude", value);
				this.OnPropertyChanged("Address2_Latitude");
			}
		}
		
		/// <summary>
		/// First line for entering address 2 information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_line1")]
		public string Address2_Line1
		{
			get
			{
				return this.GetAttributeValue<string>("address2_line1");
			}
			set
			{
				this.OnPropertyChanging("Address2_Line1");
				this.SetAttributeValue("address2_line1", value);
				this.OnPropertyChanged("Address2_Line1");
			}
		}
		
		/// <summary>
		/// Second line for entering address 2 information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_line2")]
		public string Address2_Line2
		{
			get
			{
				return this.GetAttributeValue<string>("address2_line2");
			}
			set
			{
				this.OnPropertyChanging("Address2_Line2");
				this.SetAttributeValue("address2_line2", value);
				this.OnPropertyChanged("Address2_Line2");
			}
		}
		
		/// <summary>
		/// Third line for entering address 2 information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_line3")]
		public string Address2_Line3
		{
			get
			{
				return this.GetAttributeValue<string>("address2_line3");
			}
			set
			{
				this.OnPropertyChanging("Address2_Line3");
				this.SetAttributeValue("address2_line3", value);
				this.OnPropertyChanged("Address2_Line3");
			}
		}
		
		/// <summary>
		/// Longitude for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_longitude")]
		public System.Nullable<double> Address2_Longitude
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<double>>("address2_longitude");
			}
			set
			{
				this.OnPropertyChanging("Address2_Longitude");
				this.SetAttributeValue("address2_longitude", value);
				this.OnPropertyChanged("Address2_Longitude");
			}
		}
		
		/// <summary>
		/// Name to enter for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_name")]
		public string Address2_Name
		{
			get
			{
				return this.GetAttributeValue<string>("address2_name");
			}
			set
			{
				this.OnPropertyChanging("Address2_Name");
				this.SetAttributeValue("address2_name", value);
				this.OnPropertyChanged("Address2_Name");
			}
		}
		
		/// <summary>
		/// ZIP Code or postal code for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_postalcode")]
		public string Address2_PostalCode
		{
			get
			{
				return this.GetAttributeValue<string>("address2_postalcode");
			}
			set
			{
				this.OnPropertyChanging("Address2_PostalCode");
				this.SetAttributeValue("address2_postalcode", value);
				this.OnPropertyChanged("Address2_PostalCode");
			}
		}
		
		/// <summary>
		/// Post office box number for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_postofficebox")]
		public string Address2_PostOfficeBox
		{
			get
			{
				return this.GetAttributeValue<string>("address2_postofficebox");
			}
			set
			{
				this.OnPropertyChanging("Address2_PostOfficeBox");
				this.SetAttributeValue("address2_postofficebox", value);
				this.OnPropertyChanged("Address2_PostOfficeBox");
			}
		}
		
		/// <summary>
		/// Method of shipment for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_shippingmethodcode")]
		public System.Nullable<TestProxy.systemuser_address2_shippingmethodcode> Address2_ShippingMethodCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("address2_shippingmethodcode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_address2_shippingmethodcode)(System.Enum.ToObject(typeof(TestProxy.systemuser_address2_shippingmethodcode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("Address2_ShippingMethodCode");
				if ((value == null))
				{
					this.SetAttributeValue("address2_shippingmethodcode", null);
				}
				else
				{
					this.SetAttributeValue("address2_shippingmethodcode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("Address2_ShippingMethodCode");
			}
		}
		
		/// <summary>
		/// State or province for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_stateorprovince")]
		public string Address2_StateOrProvince
		{
			get
			{
				return this.GetAttributeValue<string>("address2_stateorprovince");
			}
			set
			{
				this.OnPropertyChanging("Address2_StateOrProvince");
				this.SetAttributeValue("address2_stateorprovince", value);
				this.OnPropertyChanged("Address2_StateOrProvince");
			}
		}
		
		/// <summary>
		/// First telephone number associated with address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_telephone1")]
		public string Address2_Telephone1
		{
			get
			{
				return this.GetAttributeValue<string>("address2_telephone1");
			}
			set
			{
				this.OnPropertyChanging("Address2_Telephone1");
				this.SetAttributeValue("address2_telephone1", value);
				this.OnPropertyChanged("Address2_Telephone1");
			}
		}
		
		/// <summary>
		/// Second telephone number associated with address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_telephone2")]
		public string Address2_Telephone2
		{
			get
			{
				return this.GetAttributeValue<string>("address2_telephone2");
			}
			set
			{
				this.OnPropertyChanging("Address2_Telephone2");
				this.SetAttributeValue("address2_telephone2", value);
				this.OnPropertyChanged("Address2_Telephone2");
			}
		}
		
		/// <summary>
		/// Third telephone number associated with address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_telephone3")]
		public string Address2_Telephone3
		{
			get
			{
				return this.GetAttributeValue<string>("address2_telephone3");
			}
			set
			{
				this.OnPropertyChanging("Address2_Telephone3");
				this.SetAttributeValue("address2_telephone3", value);
				this.OnPropertyChanged("Address2_Telephone3");
			}
		}
		
		/// <summary>
		/// United Parcel Service (UPS) zone for address 2.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_upszone")]
		public string Address2_UPSZone
		{
			get
			{
				return this.GetAttributeValue<string>("address2_upszone");
			}
			set
			{
				this.OnPropertyChanging("Address2_UPSZone");
				this.SetAttributeValue("address2_upszone", value);
				this.OnPropertyChanged("Address2_UPSZone");
			}
		}
		
		/// <summary>
		/// UTC offset for address 2. This is the difference between local time and standard Coordinated Universal Time.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("address2_utcoffset")]
		public System.Nullable<int> Address2_UTCOffset
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("address2_utcoffset");
			}
			set
			{
				this.OnPropertyChanging("Address2_UTCOffset");
				this.SetAttributeValue("address2_utcoffset", value);
				this.OnPropertyChanged("Address2_UTCOffset");
			}
		}
		
		/// <summary>
		/// The identifier for the application. This is used to access data in another application.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("applicationid")]
		public System.Nullable<System.Guid> ApplicationId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("applicationid");
			}
			set
			{
				this.OnPropertyChanging("ApplicationId");
				this.SetAttributeValue("applicationid", value);
				this.OnPropertyChanged("ApplicationId");
			}
		}
		
		/// <summary>
		/// The URI used as a unique logical identifier for the external app. This can be used to validate the application.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("applicationiduri")]
		public string ApplicationIdUri
		{
			get
			{
				return this.GetAttributeValue<string>("applicationiduri");
			}
		}
		
		/// <summary>
		/// This is the application directory object Id.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("azureactivedirectoryobjectid")]
		public System.Nullable<System.Guid> AzureActiveDirectoryObjectId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("azureactivedirectoryobjectid");
			}
		}
		
		/// <summary>
		/// Unique identifier of the business unit with which the user is associated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("businessunitid")]
		public Microsoft.Xrm.Sdk.EntityReference BusinessUnitId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("businessunitid");
			}
			set
			{
				this.OnPropertyChanging("BusinessUnitId");
				this.SetAttributeValue("businessunitid", value);
				this.OnPropertyChanged("BusinessUnitId");
			}
		}
		
		/// <summary>
		/// Fiscal calendar associated with the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("calendarid")]
		public Microsoft.Xrm.Sdk.EntityReference CalendarId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("calendarid");
			}
			set
			{
				this.OnPropertyChanging("CalendarId");
				this.SetAttributeValue("calendarid", value);
				this.OnPropertyChanged("CalendarId");
			}
		}
		
		/// <summary>
		/// License type of user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("caltype")]
		public System.Nullable<TestProxy.systemuser_caltype> CALType
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("caltype");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_caltype)(System.Enum.ToObject(typeof(TestProxy.systemuser_caltype), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("CALType");
				if ((value == null))
				{
					this.SetAttributeValue("caltype", null);
				}
				else
				{
					this.SetAttributeValue("caltype", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("CALType");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who created the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
		}
		
		/// <summary>
		/// Date and time when the user was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the systemuser.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
			}
		}
		
		/// <summary>
		/// Indicates if default outlook filters have been populated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("defaultfilterspopulated")]
		public System.Nullable<bool> DefaultFiltersPopulated
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("defaultfilterspopulated");
			}
		}
		
		/// <summary>
		/// Select the mailbox associated with this user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("defaultmailbox")]
		public Microsoft.Xrm.Sdk.EntityReference DefaultMailbox
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("defaultmailbox");
			}
		}
		
		/// <summary>
		/// Type a default folder name for the user's OneDrive For Business location.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("defaultodbfoldername")]
		public string DefaultOdbFolderName
		{
			get
			{
				return this.GetAttributeValue<string>("defaultodbfoldername");
			}
		}
		
		/// <summary>
		/// Reason for disabling the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("disabledreason")]
		public string DisabledReason
		{
			get
			{
				return this.GetAttributeValue<string>("disabledreason");
			}
		}
		
		/// <summary>
		/// Whether to display the user in service views.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("displayinserviceviews")]
		public System.Nullable<bool> DisplayInServiceViews
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("displayinserviceviews");
			}
			set
			{
				this.OnPropertyChanging("DisplayInServiceViews");
				this.SetAttributeValue("displayinserviceviews", value);
				this.OnPropertyChanged("DisplayInServiceViews");
			}
		}
		
		/// <summary>
		/// Active Directory domain of which the user is a member.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("domainname")]
		public string DomainName
		{
			get
			{
				return this.GetAttributeValue<string>("domainname");
			}
			set
			{
				this.OnPropertyChanging("DomainName");
				this.SetAttributeValue("domainname", value);
				this.OnPropertyChanged("DomainName");
			}
		}
		
		/// <summary>
		/// Shows the status of the primary email address.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("emailrouteraccessapproval")]
		public System.Nullable<TestProxy.systemuser_emailrouteraccessapproval> EmailRouterAccessApproval
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("emailrouteraccessapproval");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_emailrouteraccessapproval)(System.Enum.ToObject(typeof(TestProxy.systemuser_emailrouteraccessapproval), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("EmailRouterAccessApproval");
				if ((value == null))
				{
					this.SetAttributeValue("emailrouteraccessapproval", null);
				}
				else
				{
					this.SetAttributeValue("emailrouteraccessapproval", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("EmailRouterAccessApproval");
			}
		}
		
		/// <summary>
		/// Employee identifier for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("employeeid")]
		public string EmployeeId
		{
			get
			{
				return this.GetAttributeValue<string>("employeeid");
			}
			set
			{
				this.OnPropertyChanging("EmployeeId");
				this.SetAttributeValue("employeeid", value);
				this.OnPropertyChanged("EmployeeId");
			}
		}
		
		/// <summary>
		/// Shows the default image for the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimage")]
		public byte[] EntityImage
		{
			get
			{
				return this.GetAttributeValue<byte[]>("entityimage");
			}
			set
			{
				this.OnPropertyChanging("EntityImage");
				this.SetAttributeValue("entityimage", value);
				this.OnPropertyChanged("EntityImage");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimage_timestamp")]
		public System.Nullable<long> EntityImage_Timestamp
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("entityimage_timestamp");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimage_url")]
		public string EntityImage_URL
		{
			get
			{
				return this.GetAttributeValue<string>("entityimage_url");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimageid")]
		public System.Nullable<System.Guid> EntityImageId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("entityimageid");
			}
		}
		
		/// <summary>
		/// Exchange rate for the currency associated with the systemuser with respect to the base currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("exchangerate")]
		public System.Nullable<decimal> ExchangeRate
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<decimal>>("exchangerate");
			}
		}
		
		/// <summary>
		/// First name of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("firstname")]
		public string FirstName
		{
			get
			{
				return this.GetAttributeValue<string>("firstname");
			}
			set
			{
				this.OnPropertyChanging("FirstName");
				this.SetAttributeValue("firstname", value);
				this.OnPropertyChanged("FirstName");
			}
		}
		
		/// <summary>
		/// Full name of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("fullname")]
		public string FullName
		{
			get
			{
				return this.GetAttributeValue<string>("fullname");
			}
		}
		
		/// <summary>
		/// Government identifier for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("governmentid")]
		public string GovernmentId
		{
			get
			{
				return this.GetAttributeValue<string>("governmentid");
			}
			set
			{
				this.OnPropertyChanging("GovernmentId");
				this.SetAttributeValue("governmentid", value);
				this.OnPropertyChanged("GovernmentId");
			}
		}
		
		/// <summary>
		/// Home phone number for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("homephone")]
		public string HomePhone
		{
			get
			{
				return this.GetAttributeValue<string>("homephone");
			}
			set
			{
				this.OnPropertyChanging("HomePhone");
				this.SetAttributeValue("homephone", value);
				this.OnPropertyChanged("HomePhone");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("identityid")]
		public System.Nullable<int> IdentityId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("identityid");
			}
		}
		
		/// <summary>
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
		public System.Nullable<int> ImportSequenceNumber
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
			}
			set
			{
				this.OnPropertyChanging("ImportSequenceNumber");
				this.SetAttributeValue("importsequencenumber", value);
				this.OnPropertyChanged("ImportSequenceNumber");
			}
		}
		
		/// <summary>
		/// Incoming email delivery method for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("incomingemaildeliverymethod")]
		public System.Nullable<TestProxy.systemuser_incomingemaildeliverymethod> IncomingEmailDeliveryMethod
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("incomingemaildeliverymethod");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_incomingemaildeliverymethod)(System.Enum.ToObject(typeof(TestProxy.systemuser_incomingemaildeliverymethod), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("IncomingEmailDeliveryMethod");
				if ((value == null))
				{
					this.SetAttributeValue("incomingemaildeliverymethod", null);
				}
				else
				{
					this.SetAttributeValue("incomingemaildeliverymethod", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("IncomingEmailDeliveryMethod");
			}
		}
		
		/// <summary>
		/// Internal email address for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("internalemailaddress")]
		public string InternalEMailAddress
		{
			get
			{
				return this.GetAttributeValue<string>("internalemailaddress");
			}
			set
			{
				this.OnPropertyChanging("InternalEMailAddress");
				this.SetAttributeValue("internalemailaddress", value);
				this.OnPropertyChanged("InternalEMailAddress");
			}
		}
		
		/// <summary>
		/// User invitation status.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("invitestatuscode")]
		public System.Nullable<TestProxy.systemuser_invitestatuscode> InviteStatusCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("invitestatuscode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_invitestatuscode)(System.Enum.ToObject(typeof(TestProxy.systemuser_invitestatuscode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("InviteStatusCode");
				if ((value == null))
				{
					this.SetAttributeValue("invitestatuscode", null);
				}
				else
				{
					this.SetAttributeValue("invitestatuscode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("InviteStatusCode");
			}
		}
		
		/// <summary>
		/// Information about whether the user is enabled.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isdisabled")]
		public System.Nullable<bool> IsDisabled
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("isdisabled");
			}
			set
			{
				this.OnPropertyChanging("IsDisabled");
				this.SetAttributeValue("isdisabled", value);
				this.OnPropertyChanged("IsDisabled");
			}
		}
		
		/// <summary>
		/// Shows the status of approval of the email address by O365 Admin.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isemailaddressapprovedbyo365admin")]
		public System.Nullable<bool> IsEmailAddressApprovedByO365Admin
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("isemailaddressapprovedbyo365admin");
			}
		}
		
		/// <summary>
		/// Check if user is an integration user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isintegrationuser")]
		public System.Nullable<bool> IsIntegrationUser
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("isintegrationuser");
			}
			set
			{
				this.OnPropertyChanging("IsIntegrationUser");
				this.SetAttributeValue("isintegrationuser", value);
				this.OnPropertyChanged("IsIntegrationUser");
			}
		}
		
		/// <summary>
		/// Information about whether the user is licensed.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("islicensed")]
		public System.Nullable<bool> IsLicensed
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("islicensed");
			}
			set
			{
				this.OnPropertyChanging("IsLicensed");
				this.SetAttributeValue("islicensed", value);
				this.OnPropertyChanged("IsLicensed");
			}
		}
		
		/// <summary>
		/// Information about whether the user is synced with the directory.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("issyncwithdirectory")]
		public System.Nullable<bool> IsSyncWithDirectory
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("issyncwithdirectory");
			}
			set
			{
				this.OnPropertyChanging("IsSyncWithDirectory");
				this.SetAttributeValue("issyncwithdirectory", value);
				this.OnPropertyChanged("IsSyncWithDirectory");
			}
		}
		
		/// <summary>
		/// Job title of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("jobtitle")]
		public string JobTitle
		{
			get
			{
				return this.GetAttributeValue<string>("jobtitle");
			}
			set
			{
				this.OnPropertyChanging("JobTitle");
				this.SetAttributeValue("jobtitle", value);
				this.OnPropertyChanged("JobTitle");
			}
		}
		
		/// <summary>
		/// Last name of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("lastname")]
		public string LastName
		{
			get
			{
				return this.GetAttributeValue<string>("lastname");
			}
			set
			{
				this.OnPropertyChanging("LastName");
				this.SetAttributeValue("lastname", value);
				this.OnPropertyChanged("LastName");
			}
		}
		
		/// <summary>
		/// Middle name of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("middlename")]
		public string MiddleName
		{
			get
			{
				return this.GetAttributeValue<string>("middlename");
			}
			set
			{
				this.OnPropertyChanging("MiddleName");
				this.SetAttributeValue("middlename", value);
				this.OnPropertyChanged("MiddleName");
			}
		}
		
		/// <summary>
		/// Mobile alert email address for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("mobilealertemail")]
		public string MobileAlertEMail
		{
			get
			{
				return this.GetAttributeValue<string>("mobilealertemail");
			}
			set
			{
				this.OnPropertyChanging("MobileAlertEMail");
				this.SetAttributeValue("mobilealertemail", value);
				this.OnPropertyChanged("MobileAlertEMail");
			}
		}
		
		/// <summary>
		/// Items contained with a particular SystemUser.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("mobileofflineprofileid")]
		public Microsoft.Xrm.Sdk.EntityReference MobileOfflineProfileId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("mobileofflineprofileid");
			}
			set
			{
				this.OnPropertyChanging("MobileOfflineProfileId");
				this.SetAttributeValue("mobileofflineprofileid", value);
				this.OnPropertyChanged("MobileOfflineProfileId");
			}
		}
		
		/// <summary>
		/// Mobile phone number for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("mobilephone")]
		public string MobilePhone
		{
			get
			{
				return this.GetAttributeValue<string>("mobilephone");
			}
			set
			{
				this.OnPropertyChanging("MobilePhone");
				this.SetAttributeValue("mobilephone", value);
				this.OnPropertyChanged("MobilePhone");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who last modified the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
		}
		
		/// <summary>
		/// Date and time when the user was last modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public System.Nullable<System.DateTime> ModifiedOn
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who last modified the systemuser.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// Nickname of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("nickname")]
		public string NickName
		{
			get
			{
				return this.GetAttributeValue<string>("nickname");
			}
			set
			{
				this.OnPropertyChanging("NickName");
				this.SetAttributeValue("nickname", value);
				this.OnPropertyChanged("NickName");
			}
		}
		
		/// <summary>
		/// Unique identifier of the organization associated with the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		public System.Nullable<System.Guid> OrganizationId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("organizationid");
			}
		}
		
		/// <summary>
		/// Outgoing email delivery method for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("outgoingemaildeliverymethod")]
		public System.Nullable<TestProxy.systemuser_outgoingemaildeliverymethod> OutgoingEmailDeliveryMethod
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("outgoingemaildeliverymethod");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_outgoingemaildeliverymethod)(System.Enum.ToObject(typeof(TestProxy.systemuser_outgoingemaildeliverymethod), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("OutgoingEmailDeliveryMethod");
				if ((value == null))
				{
					this.SetAttributeValue("outgoingemaildeliverymethod", null);
				}
				else
				{
					this.SetAttributeValue("outgoingemaildeliverymethod", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("OutgoingEmailDeliveryMethod");
			}
		}
		
		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
		public System.Nullable<System.DateTime> OverriddenCreatedOn
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
			}
			set
			{
				this.OnPropertyChanging("OverriddenCreatedOn");
				this.SetAttributeValue("overriddencreatedon", value);
				this.OnPropertyChanged("OverriddenCreatedOn");
			}
		}
		
		/// <summary>
		/// Unique identifier of the manager of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("parentsystemuserid")]
		public Microsoft.Xrm.Sdk.EntityReference ParentSystemUserId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("parentsystemuserid");
			}
			set
			{
				this.OnPropertyChanging("ParentSystemUserId");
				this.SetAttributeValue("parentsystemuserid", value);
				this.OnPropertyChanged("ParentSystemUserId");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("passporthi")]
		public System.Nullable<int> PassportHi
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("passporthi");
			}
			set
			{
				this.OnPropertyChanging("PassportHi");
				this.SetAttributeValue("passporthi", value);
				this.OnPropertyChanged("PassportHi");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("passportlo")]
		public System.Nullable<int> PassportLo
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("passportlo");
			}
			set
			{
				this.OnPropertyChanging("PassportLo");
				this.SetAttributeValue("passportlo", value);
				this.OnPropertyChanged("PassportLo");
			}
		}
		
		/// <summary>
		/// Personal email address of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("personalemailaddress")]
		public string PersonalEMailAddress
		{
			get
			{
				return this.GetAttributeValue<string>("personalemailaddress");
			}
			set
			{
				this.OnPropertyChanging("PersonalEMailAddress");
				this.SetAttributeValue("personalemailaddress", value);
				this.OnPropertyChanged("PersonalEMailAddress");
			}
		}
		
		/// <summary>
		/// URL for the Website on which a photo of the user is located.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("photourl")]
		public string PhotoUrl
		{
			get
			{
				return this.GetAttributeValue<string>("photourl");
			}
			set
			{
				this.OnPropertyChanging("PhotoUrl");
				this.SetAttributeValue("photourl", value);
				this.OnPropertyChanged("PhotoUrl");
			}
		}
		
		/// <summary>
		/// User's position in hierarchical security model.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("positionid")]
		public Microsoft.Xrm.Sdk.EntityReference PositionId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("positionid");
			}
			set
			{
				this.OnPropertyChanging("PositionId");
				this.SetAttributeValue("positionid", value);
				this.OnPropertyChanged("PositionId");
			}
		}
		
		/// <summary>
		/// Preferred address for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("preferredaddresscode")]
		public System.Nullable<TestProxy.systemuser_preferredaddresscode> PreferredAddressCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("preferredaddresscode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_preferredaddresscode)(System.Enum.ToObject(typeof(TestProxy.systemuser_preferredaddresscode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("PreferredAddressCode");
				if ((value == null))
				{
					this.SetAttributeValue("preferredaddresscode", null);
				}
				else
				{
					this.SetAttributeValue("preferredaddresscode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("PreferredAddressCode");
			}
		}
		
		/// <summary>
		/// Preferred email address for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("preferredemailcode")]
		public System.Nullable<TestProxy.systemuser_preferredemailcode> PreferredEmailCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("preferredemailcode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_preferredemailcode)(System.Enum.ToObject(typeof(TestProxy.systemuser_preferredemailcode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("PreferredEmailCode");
				if ((value == null))
				{
					this.SetAttributeValue("preferredemailcode", null);
				}
				else
				{
					this.SetAttributeValue("preferredemailcode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("PreferredEmailCode");
			}
		}
		
		/// <summary>
		/// Preferred phone number for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("preferredphonecode")]
		public System.Nullable<TestProxy.systemuser_preferredphonecode> PreferredPhoneCode
		{
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("preferredphonecode");
				if ((optionSet != null))
				{
					return ((TestProxy.systemuser_preferredphonecode)(System.Enum.ToObject(typeof(TestProxy.systemuser_preferredphonecode), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.OnPropertyChanging("PreferredPhoneCode");
				if ((value == null))
				{
					this.SetAttributeValue("preferredphonecode", null);
				}
				else
				{
					this.SetAttributeValue("preferredphonecode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("PreferredPhoneCode");
			}
		}
		
		/// <summary>
		/// Shows the ID of the process.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("processid")]
		public System.Nullable<System.Guid> ProcessId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("processid");
			}
			set
			{
				this.OnPropertyChanging("ProcessId");
				this.SetAttributeValue("processid", value);
				this.OnPropertyChanged("ProcessId");
			}
		}
		
		/// <summary>
		/// Unique identifier of the default queue for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("queueid")]
		public Microsoft.Xrm.Sdk.EntityReference QueueId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("queueid");
			}
			set
			{
				this.OnPropertyChanging("QueueId");
				this.SetAttributeValue("queueid", value);
				this.OnPropertyChanged("QueueId");
			}
		}
		
		/// <summary>
		/// Salutation for correspondence with the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("salutation")]
		public string Salutation
		{
			get
			{
				return this.GetAttributeValue<string>("salutation");
			}
			set
			{
				this.OnPropertyChanging("Salutation");
				this.SetAttributeValue("salutation", value);
				this.OnPropertyChanged("Salutation");
			}
		}
		
		/// <summary>
		/// Check if user is a setup user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("setupuser")]
		public System.Nullable<bool> SetupUser
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("setupuser");
			}
			set
			{
				this.OnPropertyChanging("SetupUser");
				this.SetAttributeValue("setupuser", value);
				this.OnPropertyChanged("SetupUser");
			}
		}
		
		/// <summary>
		/// SharePoint Work Email Address
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sharepointemailaddress")]
		public string SharePointEmailAddress
		{
			get
			{
				return this.GetAttributeValue<string>("sharepointemailaddress");
			}
			set
			{
				this.OnPropertyChanging("SharePointEmailAddress");
				this.SetAttributeValue("sharepointemailaddress", value);
				this.OnPropertyChanged("SharePointEmailAddress");
			}
		}
		
		/// <summary>
		/// Skill set of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("skills")]
		public string Skills
		{
			get
			{
				return this.GetAttributeValue<string>("skills");
			}
			set
			{
				this.OnPropertyChanging("Skills");
				this.SetAttributeValue("skills", value);
				this.OnPropertyChanged("Skills");
			}
		}
		
		/// <summary>
		/// Shows the ID of the stage.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("stageid")]
		public System.Nullable<System.Guid> StageId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("stageid");
			}
			set
			{
				this.OnPropertyChanging("StageId");
				this.SetAttributeValue("stageid", value);
				this.OnPropertyChanged("StageId");
			}
		}
		
		/// <summary>
		/// Unique identifier for the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("systemuserid")]
		public System.Nullable<System.Guid> SystemUserId
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("systemuserid");
			}
			set
			{
				this.OnPropertyChanging("SystemUserId");
				this.SetAttributeValue("systemuserid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("SystemUserId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("systemuserid")]
		public override System.Guid Id
		{
			get
			{
				return base.Id;
			}
			set
			{
				this.SystemUserId = value;
			}
		}
		
		/// <summary>
		/// Unique identifier of the territory to which the user is assigned.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("territoryid")]
		public Microsoft.Xrm.Sdk.EntityReference TerritoryId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("territoryid");
			}
			set
			{
				this.OnPropertyChanging("TerritoryId");
				this.SetAttributeValue("territoryid", value);
				this.OnPropertyChanged("TerritoryId");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
		public System.Nullable<int> TimeZoneRuleVersionNumber
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("timezoneruleversionnumber");
			}
			set
			{
				this.OnPropertyChanging("TimeZoneRuleVersionNumber");
				this.SetAttributeValue("timezoneruleversionnumber", value);
				this.OnPropertyChanged("TimeZoneRuleVersionNumber");
			}
		}
		
		/// <summary>
		/// Title of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("title")]
		public string Title
		{
			get
			{
				return this.GetAttributeValue<string>("title");
			}
			set
			{
				this.OnPropertyChanging("Title");
				this.SetAttributeValue("title", value);
				this.OnPropertyChanged("Title");
			}
		}
		
		/// <summary>
		/// Unique identifier of the currency associated with the systemuser.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("transactioncurrencyid")]
		public Microsoft.Xrm.Sdk.EntityReference TransactionCurrencyId
		{
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("transactioncurrencyid");
			}
			set
			{
				this.OnPropertyChanging("TransactionCurrencyId");
				this.SetAttributeValue("transactioncurrencyid", value);
				this.OnPropertyChanged("TransactionCurrencyId");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("traversedpath")]
		public string TraversedPath
		{
			get
			{
				return this.GetAttributeValue<string>("traversedpath");
			}
			set
			{
				this.OnPropertyChanging("TraversedPath");
				this.SetAttributeValue("traversedpath", value);
				this.OnPropertyChanged("TraversedPath");
			}
		}
		
		/// <summary>
		/// Shows the type of user license.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("userlicensetype")]
		public System.Nullable<int> UserLicenseType
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("userlicensetype");
			}
			set
			{
				this.OnPropertyChanging("UserLicenseType");
				this.SetAttributeValue("userlicensetype", value);
				this.OnPropertyChanged("UserLicenseType");
			}
		}
		
		/// <summary>
		///  User PUID User Identifiable Information
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("userpuid")]
		public string UserPuid
		{
			get
			{
				return this.GetAttributeValue<string>("userpuid");
			}
		}
		
		/// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
		public System.Nullable<int> UTCConversionTimeZoneCode
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("utcconversiontimezonecode");
			}
			set
			{
				this.OnPropertyChanging("UTCConversionTimeZoneCode");
				this.SetAttributeValue("utcconversiontimezonecode", value);
				this.OnPropertyChanged("UTCConversionTimeZoneCode");
			}
		}
		
		/// <summary>
		/// Version number of the user.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
		public System.Nullable<long> VersionNumber
		{
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
			}
		}
		
		/// <summary>
		/// Windows Live ID
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("windowsliveid")]
		public string WindowsLiveID
		{
			get
			{
				return this.GetAttributeValue<string>("windowsliveid");
			}
			set
			{
				this.OnPropertyChanging("WindowsLiveID");
				this.SetAttributeValue("windowsliveid", value);
				this.OnPropertyChanged("WindowsLiveID");
			}
		}
		
		/// <summary>
		/// User's Yammer login email address
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("yammeremailaddress")]
		public string YammerEmailAddress
		{
			get
			{
				return this.GetAttributeValue<string>("yammeremailaddress");
			}
			set
			{
				this.OnPropertyChanging("YammerEmailAddress");
				this.SetAttributeValue("yammeremailaddress", value);
				this.OnPropertyChanged("YammerEmailAddress");
			}
		}
		
		/// <summary>
		/// User's Yammer ID
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("yammeruserid")]
		public string YammerUserId
		{
			get
			{
				return this.GetAttributeValue<string>("yammeruserid");
			}
			set
			{
				this.OnPropertyChanging("YammerUserId");
				this.SetAttributeValue("yammeruserid", value);
				this.OnPropertyChanged("YammerUserId");
			}
		}
		
		/// <summary>
		/// Pronunciation of the first name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("yomifirstname")]
		public string YomiFirstName
		{
			get
			{
				return this.GetAttributeValue<string>("yomifirstname");
			}
			set
			{
				this.OnPropertyChanging("YomiFirstName");
				this.SetAttributeValue("yomifirstname", value);
				this.OnPropertyChanged("YomiFirstName");
			}
		}
		
		/// <summary>
		/// Pronunciation of the full name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("yomifullname")]
		public string YomiFullName
		{
			get
			{
				return this.GetAttributeValue<string>("yomifullname");
			}
		}
		
		/// <summary>
		/// Pronunciation of the last name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("yomilastname")]
		public string YomiLastName
		{
			get
			{
				return this.GetAttributeValue<string>("yomilastname");
			}
			set
			{
				this.OnPropertyChanging("YomiLastName");
				this.SetAttributeValue("yomilastname", value);
				this.OnPropertyChanged("YomiLastName");
			}
		}
		
		/// <summary>
		/// Pronunciation of the middle name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("yomimiddlename")]
		public string YomiMiddleName
		{
			get
			{
				return this.GetAttributeValue<string>("yomimiddlename");
			}
			set
			{
				this.OnPropertyChanging("YomiMiddleName");
				this.SetAttributeValue("yomimiddlename", value);
				this.OnPropertyChanged("YomiMiddleName");
			}
		}
		
		/// <summary>
		/// 1:N contact_owning_user
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("contact_owning_user")]
		public System.Collections.Generic.IEnumerable<TestProxy.Contact> contact_owning_user
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Contact>("contact_owning_user", null);
			}
			set
			{
				this.OnPropertyChanging("contact_owning_user");
				this.SetRelatedEntities<TestProxy.Contact>("contact_owning_user", null, value);
				this.OnPropertyChanged("contact_owning_user");
			}
		}
		
		/// <summary>
		/// 1:N lk_accountbase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_createdby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Account> lk_accountbase_createdby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Account>("lk_accountbase_createdby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_accountbase_createdby");
				this.SetRelatedEntities<TestProxy.Account>("lk_accountbase_createdby", null, value);
				this.OnPropertyChanged("lk_accountbase_createdby");
			}
		}
		
		/// <summary>
		/// 1:N lk_accountbase_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Account> lk_accountbase_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Account>("lk_accountbase_createdonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_accountbase_createdonbehalfby");
				this.SetRelatedEntities<TestProxy.Account>("lk_accountbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_accountbase_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_accountbase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Account> lk_accountbase_modifiedby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Account>("lk_accountbase_modifiedby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_accountbase_modifiedby");
				this.SetRelatedEntities<TestProxy.Account>("lk_accountbase_modifiedby", null, value);
				this.OnPropertyChanged("lk_accountbase_modifiedby");
			}
		}
		
		/// <summary>
		/// 1:N lk_accountbase_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Account> lk_accountbase_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Account>("lk_accountbase_modifiedonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_accountbase_modifiedonbehalfby");
				this.SetRelatedEntities<TestProxy.Account>("lk_accountbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_accountbase_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_businessunit_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunit_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.BusinessUnit> lk_businessunit_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunit_createdonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_businessunit_createdonbehalfby");
				this.SetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunit_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_businessunit_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_businessunit_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunit_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.BusinessUnit> lk_businessunit_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunit_modifiedonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_businessunit_modifiedonbehalfby");
				this.SetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunit_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_businessunit_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_businessunitbase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitbase_createdby")]
		public System.Collections.Generic.IEnumerable<TestProxy.BusinessUnit> lk_businessunitbase_createdby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunitbase_createdby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_businessunitbase_createdby");
				this.SetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunitbase_createdby", null, value);
				this.OnPropertyChanged("lk_businessunitbase_createdby");
			}
		}
		
		/// <summary>
		/// 1:N lk_businessunitbase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<TestProxy.BusinessUnit> lk_businessunitbase_modifiedby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunitbase_modifiedby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_businessunitbase_modifiedby");
				this.SetRelatedEntities<TestProxy.BusinessUnit>("lk_businessunitbase_modifiedby", null, value);
				this.OnPropertyChanged("lk_businessunitbase_modifiedby");
			}
		}
		
		/// <summary>
		/// 1:N lk_contact_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contact_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Contact> lk_contact_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Contact>("lk_contact_createdonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_contact_createdonbehalfby");
				this.SetRelatedEntities<TestProxy.Contact>("lk_contact_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_contact_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_contact_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contact_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Contact> lk_contact_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Contact>("lk_contact_modifiedonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_contact_modifiedonbehalfby");
				this.SetRelatedEntities<TestProxy.Contact>("lk_contact_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_contact_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_contactbase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contactbase_createdby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Contact> lk_contactbase_createdby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Contact>("lk_contactbase_createdby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_contactbase_createdby");
				this.SetRelatedEntities<TestProxy.Contact>("lk_contactbase_createdby", null, value);
				this.OnPropertyChanged("lk_contactbase_createdby");
			}
		}
		
		/// <summary>
		/// 1:N lk_contactbase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contactbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<TestProxy.Contact> lk_contactbase_modifiedby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Contact>("lk_contactbase_modifiedby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_contactbase_modifiedby");
				this.SetRelatedEntities<TestProxy.Contact>("lk_contactbase_modifiedby", null, value);
				this.OnPropertyChanged("lk_contactbase_modifiedby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariabledefinition_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_createdby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableDefinition> lk_environmentvariabledefinition_createdby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariabledefinition_createdby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdby", null, value);
				this.OnPropertyChanged("lk_environmentvariabledefinition_createdby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariabledefinition_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableDefinition> lk_environmentvariabledefinition_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariabledefinition_createdonbehalfby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_environmentvariabledefinition_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariabledefinition_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_modifiedby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableDefinition> lk_environmentvariabledefinition_modifiedby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariabledefinition_modifiedby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedby", null, value);
				this.OnPropertyChanged("lk_environmentvariabledefinition_modifiedby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariabledefinition_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableDefinition> lk_environmentvariabledefinition_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariabledefinition_modifiedonbehalfby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_environmentvariabledefinition_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariablevalue_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_createdby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableValue> lk_environmentvariablevalue_createdby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_createdby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariablevalue_createdby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_createdby", null, value);
				this.OnPropertyChanged("lk_environmentvariablevalue_createdby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariablevalue_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableValue> lk_environmentvariablevalue_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_createdonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariablevalue_createdonbehalfby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_environmentvariablevalue_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariablevalue_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_modifiedby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableValue> lk_environmentvariablevalue_modifiedby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariablevalue_modifiedby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedby", null, value);
				this.OnPropertyChanged("lk_environmentvariablevalue_modifiedby");
			}
		}
		
		/// <summary>
		/// 1:N lk_environmentvariablevalue_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableValue> lk_environmentvariablevalue_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedonbehalfby", null);
			}
			set
			{
				this.OnPropertyChanging("lk_environmentvariablevalue_modifiedonbehalfby");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_environmentvariablevalue_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_systemuser_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuser_createdonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referenced)]
		public System.Collections.Generic.IEnumerable<TestProxy.SystemUser> Referencedlk_systemuser_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.SystemUser>("lk_systemuser_createdonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referenced);
			}
			set
			{
				this.OnPropertyChanging("Referencedlk_systemuser_createdonbehalfby");
				this.SetRelatedEntities<TestProxy.SystemUser>("lk_systemuser_createdonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referenced, value);
				this.OnPropertyChanged("Referencedlk_systemuser_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_systemuser_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuser_modifiedonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referenced)]
		public System.Collections.Generic.IEnumerable<TestProxy.SystemUser> Referencedlk_systemuser_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.SystemUser>("lk_systemuser_modifiedonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referenced);
			}
			set
			{
				this.OnPropertyChanging("Referencedlk_systemuser_modifiedonbehalfby");
				this.SetRelatedEntities<TestProxy.SystemUser>("lk_systemuser_modifiedonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referenced, value);
				this.OnPropertyChanged("Referencedlk_systemuser_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// 1:N lk_systemuserbase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuserbase_createdby", Microsoft.Xrm.Sdk.EntityRole.Referenced)]
		public System.Collections.Generic.IEnumerable<TestProxy.SystemUser> Referencedlk_systemuserbase_createdby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.SystemUser>("lk_systemuserbase_createdby", Microsoft.Xrm.Sdk.EntityRole.Referenced);
			}
			set
			{
				this.OnPropertyChanging("Referencedlk_systemuserbase_createdby");
				this.SetRelatedEntities<TestProxy.SystemUser>("lk_systemuserbase_createdby", Microsoft.Xrm.Sdk.EntityRole.Referenced, value);
				this.OnPropertyChanged("Referencedlk_systemuserbase_createdby");
			}
		}
		
		/// <summary>
		/// 1:N lk_systemuserbase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuserbase_modifiedby", Microsoft.Xrm.Sdk.EntityRole.Referenced)]
		public System.Collections.Generic.IEnumerable<TestProxy.SystemUser> Referencedlk_systemuserbase_modifiedby
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.SystemUser>("lk_systemuserbase_modifiedby", Microsoft.Xrm.Sdk.EntityRole.Referenced);
			}
			set
			{
				this.OnPropertyChanging("Referencedlk_systemuserbase_modifiedby");
				this.SetRelatedEntities<TestProxy.SystemUser>("lk_systemuserbase_modifiedby", Microsoft.Xrm.Sdk.EntityRole.Referenced, value);
				this.OnPropertyChanged("Referencedlk_systemuserbase_modifiedby");
			}
		}
		
		/// <summary>
		/// 1:N system_user_accounts
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("system_user_accounts")]
		public System.Collections.Generic.IEnumerable<TestProxy.Account> system_user_accounts
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Account>("system_user_accounts", null);
			}
			set
			{
				this.OnPropertyChanging("system_user_accounts");
				this.SetRelatedEntities<TestProxy.Account>("system_user_accounts", null, value);
				this.OnPropertyChanged("system_user_accounts");
			}
		}
		
		/// <summary>
		/// 1:N system_user_contacts
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("system_user_contacts")]
		public System.Collections.Generic.IEnumerable<TestProxy.Contact> system_user_contacts
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Contact>("system_user_contacts", null);
			}
			set
			{
				this.OnPropertyChanging("system_user_contacts");
				this.SetRelatedEntities<TestProxy.Contact>("system_user_contacts", null, value);
				this.OnPropertyChanged("system_user_contacts");
			}
		}
		
		/// <summary>
		/// 1:N user_accounts
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_accounts")]
		public System.Collections.Generic.IEnumerable<TestProxy.Account> user_accounts
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.Account>("user_accounts", null);
			}
			set
			{
				this.OnPropertyChanging("user_accounts");
				this.SetRelatedEntities<TestProxy.Account>("user_accounts", null, value);
				this.OnPropertyChanged("user_accounts");
			}
		}
		
		/// <summary>
		/// 1:N user_environmentvariabledefinition
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_environmentvariabledefinition")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableDefinition> user_environmentvariabledefinition
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("user_environmentvariabledefinition", null);
			}
			set
			{
				this.OnPropertyChanging("user_environmentvariabledefinition");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableDefinition>("user_environmentvariabledefinition", null, value);
				this.OnPropertyChanged("user_environmentvariabledefinition");
			}
		}
		
		/// <summary>
		/// 1:N user_environmentvariablevalue
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_environmentvariablevalue")]
		public System.Collections.Generic.IEnumerable<TestProxy.EnvironmentVariableValue> user_environmentvariablevalue
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.EnvironmentVariableValue>("user_environmentvariablevalue", null);
			}
			set
			{
				this.OnPropertyChanging("user_environmentvariablevalue");
				this.SetRelatedEntities<TestProxy.EnvironmentVariableValue>("user_environmentvariablevalue", null, value);
				this.OnPropertyChanged("user_environmentvariablevalue");
			}
		}
		
		/// <summary>
		/// 1:N user_parent_user
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_parent_user", Microsoft.Xrm.Sdk.EntityRole.Referenced)]
		public System.Collections.Generic.IEnumerable<TestProxy.SystemUser> Referenceduser_parent_user
		{
			get
			{
				return this.GetRelatedEntities<TestProxy.SystemUser>("user_parent_user", Microsoft.Xrm.Sdk.EntityRole.Referenced);
			}
			set
			{
				this.OnPropertyChanging("Referenceduser_parent_user");
				this.SetRelatedEntities<TestProxy.SystemUser>("user_parent_user", Microsoft.Xrm.Sdk.EntityRole.Referenced, value);
				this.OnPropertyChanged("Referenceduser_parent_user");
			}
		}
		
		/// <summary>
		/// N:1 business_unit_system_users
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("businessunitid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_system_users")]
		public TestProxy.BusinessUnit business_unit_system_users
		{
			get
			{
				return this.GetRelatedEntity<TestProxy.BusinessUnit>("business_unit_system_users", null);
			}
			set
			{
				this.OnPropertyChanging("business_unit_system_users");
				this.SetRelatedEntity<TestProxy.BusinessUnit>("business_unit_system_users", null, value);
				this.OnPropertyChanged("business_unit_system_users");
			}
		}
		
		/// <summary>
		/// N:1 lk_systemuser_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuser_createdonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referencing)]
		public TestProxy.SystemUser Referencinglk_systemuser_createdonbehalfby
		{
			get
			{
				return this.GetRelatedEntity<TestProxy.SystemUser>("lk_systemuser_createdonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referencing);
			}
		}
		
		/// <summary>
		/// N:1 lk_systemuser_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuser_modifiedonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referencing)]
		public TestProxy.SystemUser Referencinglk_systemuser_modifiedonbehalfby
		{
			get
			{
				return this.GetRelatedEntity<TestProxy.SystemUser>("lk_systemuser_modifiedonbehalfby", Microsoft.Xrm.Sdk.EntityRole.Referencing);
			}
		}
		
		/// <summary>
		/// N:1 lk_systemuserbase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuserbase_createdby", Microsoft.Xrm.Sdk.EntityRole.Referencing)]
		public TestProxy.SystemUser Referencinglk_systemuserbase_createdby
		{
			get
			{
				return this.GetRelatedEntity<TestProxy.SystemUser>("lk_systemuserbase_createdby", Microsoft.Xrm.Sdk.EntityRole.Referencing);
			}
		}
		
		/// <summary>
		/// N:1 lk_systemuserbase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuserbase_modifiedby", Microsoft.Xrm.Sdk.EntityRole.Referencing)]
		public TestProxy.SystemUser Referencinglk_systemuserbase_modifiedby
		{
			get
			{
				return this.GetRelatedEntity<TestProxy.SystemUser>("lk_systemuserbase_modifiedby", Microsoft.Xrm.Sdk.EntityRole.Referencing);
			}
		}
		
		/// <summary>
		/// N:1 user_parent_user
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("parentsystemuserid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_parent_user", Microsoft.Xrm.Sdk.EntityRole.Referencing)]
		public TestProxy.SystemUser Referencinguser_parent_user
		{
			get
			{
				return this.GetRelatedEntity<TestProxy.SystemUser>("user_parent_user", Microsoft.Xrm.Sdk.EntityRole.Referencing);
			}
			set
			{
				this.OnPropertyChanging("Referencinguser_parent_user");
				this.SetRelatedEntity<TestProxy.SystemUser>("user_parent_user", Microsoft.Xrm.Sdk.EntityRole.Referencing, value);
				this.OnPropertyChanged("Referencinguser_parent_user");
			}
		}
	}
}
