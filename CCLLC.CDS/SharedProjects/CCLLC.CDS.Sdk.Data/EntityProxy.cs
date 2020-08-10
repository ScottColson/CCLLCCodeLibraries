using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;



[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]
namespace CCLLC.CDS.Sdk.EarlyBound
{
    public enum eTextOptions
    {
        /// <summary>Ignore and let CRM handle any issues with the value</summary>
        Ignore,
        /// <summary>If the length is greater than the max length, truncate the value to the max length</summary>
        Truncate,
        /// <summary>Throw an error if the length of the value is greater than the max length</summary>
        ThrowError
    }

    public enum eNumberOptions
    {
        /// <summary>Ignore and let CRM handle any issues with the value.</summary>
        Ignore,
        /// <summary>If the value is less than the min value set the value as the min value.<para>Let CRM handle any issues with the max value.</para></summary>
        CorrectMinIgnoreMax,
        /// <summary>If the value is less than the min value set the value as the min value.<para>Throw an error if the value is greater than the max value.</para></summary>
        CorrectMinThrowMax,
        /// <summary>If the value is greater than the max value set the value as the max value.<para>Let CRM handle any issues with the min value.</para></summary>
        CorrectMaxIgnoreMin,
        /// <summary>If the value is greater than the max value set the value as the max value.<para>Throw an error if the value is less than the min value.</para></summary>
        CorrectMaxThrowMin,
        /// <summary>If the value is less than the min value set the value as the min value.<para>If the value is greater than the max value set the value as the max value.</para></summary>
        CorrectMinAndMax,
        /// <summary>Throw an error if the value is less than the min or greater than the max</summary>
        ThrowError
    }

    public enum eErrorType
    {
        Text,
        Number
    }

    public abstract partial class EntityProxy : Entity, INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        Dictionary<string,object> changedValues = new Dictionary<string, object>();
        Dictionary<string,eTextOptions> textOptions = new Dictionary<string, eTextOptions>();
        Dictionary<string,eNumberOptions> numberOptions = new Dictionary<string, eNumberOptions>();
        Dictionary<string, string> errorText = new Dictionary<string, string>();

        AttributeEqualityComparer _equalityComparer = new AttributeEqualityComparer();

        public bool IsDirty => this.changedValues.Count > 0;
        public eNumberOptions NumberOptions { get; set; } = eNumberOptions.ThrowError;
        public eTextOptions TextOptions { get; set; } = eTextOptions.ThrowError;
        public string TextError { get; set; } = "The value for {0} exceeds the maximum length of {2}.";
        public string NumberError { get; set; } = "The value for {0} must be between {1} and {2}.";

        protected EntityProxy(string logicalName)
           : this(new Entity(logicalName)) { }

        protected EntityProxy(Entity original)
        {
            _ = original?.LogicalName ?? throw new ArgumentNullException(nameof(original));
           
            this.LogicalName = original.LogicalName;
            this.RelatedEntities.Clear();
            this.FormattedValues.Clear();
            this.Attributes.Clear();
            this.RelatedEntities.AddRange(original.RelatedEntities);
            this.FormattedValues.AddRange(original.FormattedValues);
            this.ExtensionData = original.ExtensionData;
            this.Attributes.AddRange(original.Attributes);
            this.EntityState = original.EntityState;
            this.Id = original.Id;
        }

        public void Save(IOrganizationService service)
        {
            if (this.Id != Guid.Empty) {
                this.Update(service); }
            else {
                this.Create(service); }
        }
        
        public Guid Create(IOrganizationService service)
        {
            this.Id = service.Create(this);
            changedValues.Clear();
            return this.Id;
        }

        public void Update(IOrganizationService service)
        {
            if (IsDirty)
            {
                service.Update(GetChangedEntity());
                changedValues.Clear();
            }
        }

        public void Delete(IOrganizationService service)
        {
            service.Delete(this.LogicalName, this.Id);
        }       
               
        public Entity GetChangedEntity()
        {
            var entity = new Entity(this.LogicalName);
            entity.Id = this.Id;
            foreach (string attributeName in changedValues.Keys)
                entity.Attributes[attributeName] = this.Attributes[attributeName];
            return entity;
        }

        public static implicit operator EntityReference(EntityProxy proxy)
        {
             return proxy?.ToEntityReference(); 
        }

        public T GetPropertyValue<T>(string name)
        {
            if (this.Contains(name))
            {
                return (T)this.Attributes[name];
            }
            return default(T);
        }
        
        public void SetPropertyValue<T>(string name, T value)
        {
            if (changedValues.ContainsKey(name))
            {
                var originalValue = changedValues[name];
                var currentValue = this.Contains(name) ? (object)this.GetPropertyValue<T>(name) : null;
                if (!_equalityComparer.Equals(currentValue, value))
                {
                    OnPropertyChanging(name);
                    if (_equalityComparer.Equals(originalValue, value)) { changedValues.Remove(name); }
                    this.Attributes[name] = value;
                    OnPropertyChanged(name);
                }
            }
            else
            {
                var currentValue = this.Contains(name) ? (object)this.GetPropertyValue<T>(name) : null;
                if (!_equalityComparer.Equals(currentValue, value))
                {
                    OnPropertyChanging(name);
                    changedValues.Add(name, currentValue);
                    this.Attributes[name] = value;
                    OnPropertyChanged(name);
                }
            }
        }

        public void SetPropertyValue(string name, string value, int maxLength)
        {
            var textOptions = GetTextOptions(name);
            if (textOptions != eTextOptions.Ignore && !string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                if (textOptions == eTextOptions.Truncate) { value = value.Substring(0, maxLength); }
                else { throw new Exception(string.Format(GetErrorText(name, eErrorType.Text), name, value.Length, maxLength)); }
            }
            SetPropertyValue<string>(name, value);
        }

        public void SetPropertyValue(string name, int? value, int minValue, int maxValue)
        {
            var numberOptions = GetNumberOptions(name);
            if (numberOptions != eNumberOptions.Ignore && (value < minValue || value > maxValue))
            {
                bool throwError = false;
                if (numberOptions == eNumberOptions.CorrectMinAndMax) { value = (value < minValue) ? minValue : maxValue; }
                else if (numberOptions == eNumberOptions.CorrectMinIgnoreMax) { value = (value < minValue) ? minValue : value; }
                else if (numberOptions == eNumberOptions.CorrectMinThrowMax && value < minValue) { value = minValue; }
                else if (numberOptions == eNumberOptions.CorrectMaxIgnoreMin) { value = (value > maxValue) ? maxValue : value; }
                else if (numberOptions == eNumberOptions.CorrectMaxThrowMin && value > maxValue) { value = maxValue; }
                else { throwError = true; }
                if (throwError) { throw new Exception(string.Format(GetErrorText(name, eErrorType.Number), name, value, minValue, maxValue)); }
            }
            SetPropertyValue<int?>(name, value);
        }

        public void SetPropertyValue(string name, decimal? value, decimal minValue, decimal maxValue)
        {
            var numberOptions = GetNumberOptions(name);
            if (numberOptions != eNumberOptions.Ignore && (value < minValue || value > maxValue))
            {
                bool throwError = false;
                if (numberOptions == eNumberOptions.CorrectMinAndMax) { value = (value < minValue) ? minValue : maxValue; }
                else if (numberOptions == eNumberOptions.CorrectMinIgnoreMax) { value = (value < minValue) ? minValue : value; }
                else if (numberOptions == eNumberOptions.CorrectMinThrowMax && value < minValue) { value = minValue; }
                else if (numberOptions == eNumberOptions.CorrectMaxIgnoreMin) { value = (value > maxValue) ? maxValue : value; }
                else if (numberOptions == eNumberOptions.CorrectMaxThrowMin && value > maxValue) { value = maxValue; }
                else { throwError = true; }
                if (throwError) { throw new Exception(string.Format(GetErrorText(name, eErrorType.Number), name, value, minValue, maxValue)); }
            }
            SetPropertyValue<decimal?>(name, value);
        }

        public void SetPropertyValue(string name, double? value, double minValue, double maxValue)
        {
            var numberOptions = GetNumberOptions(name);
            if (numberOptions != eNumberOptions.Ignore && (value < minValue || value > maxValue))
            {
                bool throwError = false;
                if (numberOptions == eNumberOptions.CorrectMinAndMax) { value = (value < minValue) ? minValue : maxValue; }
                else if (numberOptions == eNumberOptions.CorrectMinIgnoreMax) { value = (value < minValue) ? minValue : value; }
                else if (numberOptions == eNumberOptions.CorrectMinThrowMax && value < minValue) { value = minValue; }
                else if (numberOptions == eNumberOptions.CorrectMaxIgnoreMin) { value = (value > maxValue) ? maxValue : value; }
                else if (numberOptions == eNumberOptions.CorrectMaxThrowMin && value > maxValue) { value = maxValue; }
                else { throwError = true; }
                if (throwError) { throw new Exception(string.Format(GetErrorText(name, eErrorType.Number), name, value, minValue, maxValue)); }
            }
            SetPropertyValue<double?>(name, value);
        }

        public void SetPropertyValue(string name, Money value, decimal minValue, decimal maxValue)
        {
            var numberOptions = GetNumberOptions(name);
            if (value != null && numberOptions != eNumberOptions.Ignore && (value.Value < minValue || value.Value > maxValue))
            {
                bool throwError = false;
                if (numberOptions == eNumberOptions.CorrectMinAndMax) { value.Value = (value.Value < minValue) ? minValue : maxValue; }
                else if (numberOptions == eNumberOptions.CorrectMinIgnoreMax) { value.Value = (value.Value < minValue) ? minValue : value.Value; }
                else if (numberOptions == eNumberOptions.CorrectMinThrowMax && value.Value < minValue) { value.Value = minValue; }
                else if (numberOptions == eNumberOptions.CorrectMaxIgnoreMin) { value.Value = (value.Value > maxValue) ? maxValue : value.Value; }
                else if (numberOptions == eNumberOptions.CorrectMaxThrowMin && value.Value > maxValue) { value.Value = maxValue; }
                else { throwError = true; }
                if (throwError) { throw new Exception(string.Format(GetErrorText(name, eErrorType.Number), name, value.Value, minValue, maxValue)); }
            }
            SetPropertyValue<Money>(name, value);
        }

        private void OnPropertyChanging(string propertyName)
        {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected eTextOptions GetTextOptions(string logicalName)
        {
            if (textOptions.ContainsKey(logicalName)) { return textOptions[logicalName]; }
            return TextOptions;
        }

        protected eNumberOptions GetNumberOptions(string logicalName)
        {
            if (numberOptions.ContainsKey(logicalName)) { return numberOptions[logicalName]; }
            return NumberOptions;
        }

        protected string GetErrorText(string attributeName, eErrorType defaultError)
        {
            if (errorText.ContainsKey(attributeName)) return errorText[attributeName];

            return defaultError == eErrorType.Number ? NumberError : TextError;
        }

        private class AttributeEqualityComparer : IEqualityComparer
        {
            public new bool Equals(object x, object y)
            {
                if ((x == null || (x.GetType() == typeof(string) && string.IsNullOrEmpty(x as string))) && (y == null || (y.GetType() == typeof(string) && string.IsNullOrEmpty(y as string))))
                    return true;
                else
                {
                    if (x == null && y == null) { return true; }
                    else if (x == null && y != null) { return false; }
                    else if (x != null && y == null) { return false; }
                    else if (x.GetType() == y.GetType())
                    {
                        if (x.GetType() == typeof(OptionSetValue)) { return ((OptionSetValue)x).Value == ((OptionSetValue)y).Value; }
                        else if (x.GetType() == typeof(BooleanManagedProperty)) { return ((BooleanManagedProperty)x).Value == ((BooleanManagedProperty)y).Value; }
                        else if (x.GetType() == typeof(EntityReference))
                        {
                            if (((EntityReference)x).LogicalName == ((EntityReference)y).LogicalName) { return ((EntityReference)x).Id == ((EntityReference)y).Id; }
                            else { return false; }
                        }
                        else if (x.GetType() == typeof(Money)) { return (((Money)x).Value == ((Money)y).Value); }
                        else if (x.GetType() == typeof(DateTime) || x.GetType() == typeof(DateTime?))
                        {                            
                            return Math.Abs(((DateTime)x - (DateTime)y).TotalSeconds) < 1;
                        }
                        else { return x.Equals(y); }
                    }
                    else { return false; }
                }
            }
            public int GetHashCode(object obj)
            {
                return obj.GetHashCode();
            }
        }
        
    }

    public static partial class ExtensionMethods
    {
        public static Guid Create(this IOrganizationService service, EntityProxy proxy)
        {
            proxy.Id = service.Create(proxy);
            return proxy.Id;
        }

        public static void Update(this IOrganizationService service, EntityProxy proxy)
        {
            proxy.Update(service);
        }

        public static void Delete(this IOrganizationService service, EntityProxy proxy)
        {
            service.Delete(proxy.LogicalName, proxy.Id);
        }

        public static void SetState(this IOrganizationService service, EntityProxy proxy, int state, int status)
        {
            service.SetState(proxy, new OptionSetValue(state), new OptionSetValue(status));
        }

        public static void SetState(this IOrganizationService service, EntityProxy proxy, OptionSetValue state, OptionSetValue status)
        {
            var request = new SetStateRequest() { EntityMoniker = proxy, State = state, Status = status };
            service.Execute(request);
        }

        public static T ToProxy<T>(this EntityReference reference) where T : EntityProxy
        {
            if (reference == null) { return null; }
            return reference.ToEntity().ToProxy<T>();
        }

        public static Entity ToEntity(this EntityReference reference)
        {
            if (reference == null) { return null; }
            return new Entity() { LogicalName = reference.LogicalName, Id = reference.Id };
        }

        private static T ToProxy<T>(this Entity entity, ConstructorInfo construcor) where T : EntityProxy
        {
            return construcor.Invoke(new object[] { entity }) as T;
        }
        public static AttributeMetadata GetAttributeMetadata(this IOrganizationService service, string entityLogicalName, string attributeLogicalName)
        {
            var request = new RetrieveAttributeRequest() { EntityLogicalName = entityLogicalName, LogicalName = attributeLogicalName };
            return (service.Execute(request) as RetrieveAttributeResponse).AttributeMetadata;
        }
        public static OptionMetadata GetOptionMetadata(this OptionSetValue value, IOrganizationService service, EntityProxy entity, string attributeLogicalName)
        {
            var attributeMeta = service.GetAttributeMetadata(entity.LogicalName, attributeLogicalName);
            if (attributeMeta is EnumAttributeMetadata)
                return ((EnumAttributeMetadata)attributeMeta).GetOptionMetadata(value.Value);
            else { throw new Exception("The attribute is not an Enum type attribute"); }
        }
        public static OptionMetadata GetOptionMetadata(this EnumAttributeMetadata enumMeta, int value)
        {
            return (from meta in enumMeta.OptionSet.Options where meta.Value == value select meta).FirstOrDefault();
        }
        public static string GetOptionSetText(this EnumAttributeMetadata enumMeta, int value)
        {
            return enumMeta.GetOptionMetadata(value).GetOptionSetText();
        }
        public static string GetOptionSetText(this OptionSetValue value, IOrganizationService service, EntityProxy entity, string attributeLogicalName)
        {
            var optionMeta = GetOptionMetadata(value, service, entity, attributeLogicalName);
            return optionMeta.GetOptionSetText();
        }
        public static string GetOptionSetText(this OptionMetadata optionMeta)
        {
            if (optionMeta != null && optionMeta.Label != null && optionMeta.Label.UserLocalizedLabel != null) { return optionMeta.Label.UserLocalizedLabel.Label; }
            return string.Empty;
        }

        public static T ToProxy<T>(this Entity entity) where T : EntityProxy
        {
            if (entity != null)
            {
                var constructor = typeof(T).GetConstructor(new Type[] { typeof(Entity) });
                return entity.ToProxy<T>(constructor);
            }
            return null;
        }
        public static List<T> ToProxies<T>(this EntityCollection entities) where T : EntityProxy
        {
            return entities.Entities.ToProxies<T>();
        }
        public static List<T> ToProxies<T>(this IEnumerable<Entity> entities) where T : EntityProxy
        {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(Entity) });
            return (from entity in entities select entity.ToProxy<T>(constructor)).ToList();
        }

    }
}