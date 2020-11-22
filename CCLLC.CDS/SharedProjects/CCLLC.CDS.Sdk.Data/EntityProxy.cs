using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.Xrm.Sdk;

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

    [System.Runtime.Serialization.DataContractAttribute()]
    public abstract partial class EntityProxy<T> : EntityProxy where T : EntityProxy
    {
        protected EntityProxy(string logicalName) : base(logicalName)
        { }

        protected EntityProxy(Entity original) : base(original)
        { }

        public T WithClearedChangeHistory()
        {
            this.ClearChangeHistory();
            return this.ToEntity<T>();
        }

        new public T GetChangedEntity()
        {
            return base.GetChangedEntity().ToEntity<T>();
        }
    }

    [System.Runtime.Serialization.DataContractAttribute()]
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

            if (original.Id != default(Guid))
            {
                base.Id = original.Id;
            }            
        }

        /// <summary>
        /// Clears history of any previous changes so state of the proxy is as it would be if
        /// it was just retrieved.
        /// </summary>
        protected void ClearChangeHistory()
        {
            this.changedValues.Clear();
        }

        public void Save(IOrganizationService service)
        {
            if (this.Id != default(Guid)) {
                this.Update(service); }
            else {
                this.Create(service); }
        }
        
        public Guid Create(IOrganizationService service)
        {
            this.Id = service.Create(this);
            ClearChangeHistory();
            return this.Id;
        }

        public void Update(IOrganizationService service)
        {
            if (IsDirty)
            {
                service.Update(GetChangedEntity());
                ClearChangeHistory();
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

    }

    public static partial class ExtensionMethods
    {
        [Obsolete("Obsolete. Use ToEntity<T>.")]
        public static T ToProxy<T>(this EntityReference reference) where T : EntityProxy
        {
            if (reference == null) { return null; }
            return reference.ToEntity().ToProxy<T>();
        }        

        [Obsolete("Obsolete. Use ToEntity<T>.")]
        private static T ToProxy<T>(this Entity entity, ConstructorInfo construcor) where T : EntityProxy
        {
            return construcor.Invoke(new object[] { entity }) as T;
        }

        [Obsolete("Obsolete. Use ToEntity<T>.")]
        public static T ToProxy<T>(this Entity entity) where T : EntityProxy
        {
            if (entity != null)
            {
                var constructor = typeof(T).GetConstructor(new Type[] { typeof(Entity) });
                return entity.ToProxy<T>(constructor);
            }
            return null;
        }

        [Obsolete("Obsolete. Use ToList<T>.")]
        public static List<T> ToProxies<T>(this EntityCollection entities) where T : EntityProxy
        {
            return entities.Entities.ToProxies<T>();
        }

        [Obsolete("Obsolete. Use ToList<T>.")]
        public static List<T> ToProxies<T>(this IEnumerable<Entity> entities) where T : EntityProxy
        {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(Entity) });
            return (from entity in entities select entity.ToProxy<T>(constructor)).ToList();
        }       
    }
}