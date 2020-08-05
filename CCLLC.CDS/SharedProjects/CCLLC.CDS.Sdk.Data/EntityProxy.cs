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
    public abstract partial class EntityProxy : Entity, INotifyPropertyChanged, INotifyPropertyChanging
    {
        Dictionary<string, object> _changedValues = new Dictionary<string, object>();
        AttributeEqualityComparer _equalityComparer = new AttributeEqualityComparer();

        protected EntityProxy(string logicalName)
           : this(new Entity(logicalName)) { }

        protected EntityProxy(Entity original)
        {
            if (string.IsNullOrEmpty(original.LogicalName)) { throw new Exception("Please specify the 'logicalName' on the entity when using a proxy class."); }
            this.LogicalName = GetLogicalName(this.GetType());
            if (this.LogicalName != original.LogicalName) { throw new Exception("Please make sure that the entity logical name matches that of the proxy class you are creating."); }

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

        public static bool ReturnDatesInLocalTime = false;

        public Guid Create(IOrganizationService service)
        {
            this.Id = service.Create(this);
            _changedValues.Clear();
            return this.Id;
        }

        public void Delete(IOrganizationService service)
        {
            service.Delete(this.LogicalName, this.Id);
        }

        public void Update(IOrganizationService service)
        {
            if (_changedValues.Count > 0)
                service.Update(GetChangedEntity());
            _changedValues.Clear();
        }

        public async System.Threading.Tasks.Task<Guid> CreateAsync(IOrganizationService service)
        {
            return await System.Threading.Tasks.Task.Run(() => { return this.Create(service); });
        }
        public async System.Threading.Tasks.Task UpdateAsync(IOrganizationService service)
        {
            await System.Threading.Tasks.Task.Run(() => { this.Update(service); });
        }
        public async System.Threading.Tasks.Task DeleteAsync(IOrganizationService service)
        {
            await System.Threading.Tasks.Task.Run(() => { this.Delete(service); });
        }
        public Entity GetChangedEntity()
        {
            var entity = new Entity(this.LogicalName);
            entity.Id = this.Id;
            foreach (string attributeName in _changedValues.Keys)
                entity.Attributes[attributeName] = this.Attributes[attributeName];
            return entity;
        }
        public void Save(IOrganizationService service)
        {
            if (this.Id != Guid.Empty) { this.Update(service); }
            else { this.Create(service); }
        }
        public async System.Threading.Tasks.Task SaveAsync(IOrganizationService service)
        {
            await System.Threading.Tasks.Task.Run(() => { this.Save(service); });
        }

        private static Dictionary<Type, string> _proxyTypes = new Dictionary<Type, string>();
        public static void RegisterProxyType(Type ProxyType, string logicalName)
        {
            if (!_proxyTypes.ContainsKey(ProxyType)) { _proxyTypes.Add(ProxyType, logicalName); }
        }
        public static void RegisterProxyTypesInAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                foreach (var type in assembly.GetTypes())
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }
        public static string GetLogicalName<T>() where T : EntityProxy
        {
            return GetLogicalName(typeof(T));
        }

        public static string GetLogicalName(Type t)
        {
            var logicalNameAttribute =
                (EntityLogicalNameAttribute)Attribute.GetCustomAttribute(t, typeof(EntityLogicalNameAttribute));

            return logicalNameAttribute?.LogicalName;
        }

        public static implicit operator EntityReference(EntityProxy proxy)
        {
            if (proxy != null) { return proxy.ToEntityReference(); }
            return null;
        }

        public T GetPropertyValue<T>(string name)
        {
            if (this.Contains(name))
            {
                var value = (T)this.Attributes[name];
                if ((typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?)) && ReturnDatesInLocalTime && value != null)
                    value = (T)(Object)((DateTime)(Object)value).ToLocalTime();
                return value;
            }
            return default(T);
        }


       

        public void SetPropertyValue<T>(string name, T value, string propertyName)
        {
            if (_changedValues.ContainsKey(name))
            {
                var originalValue = _changedValues[name];
                var currentValue = this.Contains(name) ? (object)this.GetPropertyValue<T>(name) : null;
                if (!_equalityComparer.Equals(currentValue, value))
                {
                    OnPropertyChanging(propertyName);
                    if (_equalityComparer.Equals(originalValue, value)) { _changedValues.Remove(name); }
                    this.Attributes[name] = value;
                    OnPropertyChanged(propertyName);
                }
            }
            else
            {
                var currentValue = this.Contains(name) ? (object)this.GetPropertyValue<T>(name) : null;
                if (!_equalityComparer.Equals(currentValue, value))
                {
                    OnPropertyChanging(propertyName);
                    _changedValues.Add(name, currentValue);
                    this.Attributes[name] = value;
                    OnPropertyChanged(propertyName);
                }
            }
        }

        private void OnPropertyChanging(string propertyName)
        {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetPropertyValue(string name, string value, int maxLength, string propertyName)
        {
            var textOptions = GetTextOptions(name);
            if (textOptions != eTextOptions.Ignore && !string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                if (textOptions == eTextOptions.Truncate) { value = value.Substring(0, maxLength); }
                else { throw new Exception(string.Format(GetErrorString(name, eErrorType.Text), name, value, value.Length, maxLength)); }
            }
            SetPropertyValue<string>(name, value, propertyName);
        }

        public void SetPropertyValue(string name, int? value, int minValue, int maxValue, string propertyName)
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
                if (throwError) { throw new Exception(string.Format(GetErrorString(name, eErrorType.Number), name, value, minValue, maxValue)); }
            }
            SetPropertyValue(name, value, propertyName);
        }
        public void SetPropertyValue(string name, decimal? value, decimal minValue, decimal maxValue, string propertyName)
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
                if (throwError) { throw new Exception(string.Format(GetErrorString(name, eErrorType.Number), name, value, minValue, maxValue)); }
            }
            SetPropertyValue(name, value, propertyName);
        }
        public void SetPropertyValue(string name, double? value, double minValue, double maxValue, string propertyName)
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
                if (throwError) { throw new Exception(string.Format(GetErrorString(name, eErrorType.Number), name, value, minValue, maxValue)); }
            }
            SetPropertyValue(name, value, propertyName);
        }
        public void SetPropertyValue(string name, Money value, decimal minValue, decimal maxValue, string propertyName)
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
                if (throwError) { throw new Exception(string.Format(GetErrorString(name, eErrorType.Number), name, value.Value, minValue, maxValue)); }
            }
            SetPropertyValue(name, value, propertyName);
        }
        protected abstract eTextOptions GetTextOptions(string logicalName);
        protected abstract string GetErrorString(string attributeName, eErrorType defaultErrorType);
        protected abstract eNumberOptions GetNumberOptions(string logicalName);

        public bool IsDirty
        {
            get { return this._changedValues.Count > 0; }
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
                            //Compare only down to the second since CRM only tracks down to the second
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
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