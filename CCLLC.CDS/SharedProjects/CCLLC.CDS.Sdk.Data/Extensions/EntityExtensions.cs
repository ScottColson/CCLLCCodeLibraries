using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CCLLC.CDS.Sdk
{    
    
    /// <summary>
    /// Extensions for the Microsoft.Xrm.Sdk Entity class to provide a set of common functions for manipulating Entity records.
    /// </summary>
    public static partial class Extensions
    {        
        /// <summary>
        /// Checks the target for existence of any attribute contained in the provided array of attribute names and returns
        /// true if at least one of the provided attributes exists.
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="AttributeNames"></param>
        /// <returns></returns>
        public static bool ContainsAny(this Entity Target, params string[] AttributeNames)
        {
            if (AttributeNames != null)
            {
                foreach (string a in AttributeNames)
                {
                    if (Target.Contains(a))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks an early bound entity for the existence of one or more fields using projection to
        /// define the field list.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="Target"></param>
        /// <param name="anonymousTypeInitializer"></param>
        /// <returns></returns>
        public static bool ContainsAny<E>(this E Target, Expression<Func<E, object>> anonymousTypeInitializer) where E : Entity
        {
            var columns = anonymousTypeInitializer.GetAttributeNamesArray<E>();
            return Target.ContainsAny(columns);
        }

        /// <summary>
        /// Retrieves an attribute from the entity with of the specified type and returns an 
        /// optionally specified default value if the attribute does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Target"></param>
        /// <param name="Key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Entity Target, string Key, T DefaultValue = default(T))
        {
            if (!Target.Contains(Key) || Target[Key] is null)
            {
                return DefaultValue;
            }
            
            return Target.GetAttributeValue<T>(Key);            
        }


        public static T GetAliasedValue<T>(this Entity target, string alias, string fieldName, T defaultValue = default(T))
        {
            string key = string.Format("{0}.{1}", alias, fieldName);
            return target.GetAliasedValue<T>(key, defaultValue);
            
        }

        public static T GetAliasedValue<T>(this Entity target, string key, T defaultValue = default(T))
        {            
            if (!target.Contains(key) || target[key] is null)
            {
                return defaultValue;
            }

            AliasedValue value = target.GetAttributeValue<AliasedValue>(key);
            return (T)value.Value;
        }

        /// <summary>
        /// Returns the aliased fields associated with a joined entity as a early bound entity. When alias is null 
        /// an alias of the return record type is assumed. Will return null if there are no aliased fields for the
        /// return entity type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static T GetAliasedEntity<T>(this Entity target, string alias = null) where T : Entity, new()
        {
            T record = new T();
            var idattribute = record.LogicalName + "id";

            if(string.IsNullOrEmpty(alias))
            {
                alias = record.LogicalName;
            }

            alias += ".";

            foreach(var key in target.Attributes.Keys.Where(k => k.StartsWith(alias)))
            {
                var value = target.GetAttributeValue<AliasedValue>(key).Value;
                var aliasedKey = key.Substring(alias.Length);
                
                record.Attributes.Add(aliasedKey, value);
                if(aliasedKey == idattribute)
                {
                    record.Id = (Guid)value;                    
                }

            }

            if (record.Attributes.Count == 0)
                return null;

            return record;
        }

        /// <summary>
        /// Merge the entity with another entity to get a more complete list of attributes. If 
        /// the current entity and the source entity both have a value for a given attribute 
        /// then the attribute of the current entity will be preserved.
        /// </summary>
        /// <param name="copyTo"></param>
        /// <param name="copyFrom"></param>
        public static void MergeWith(this Entity copyTo, Entity copyFrom)
        {
            if (copyTo != null && copyFrom != null)
            {
                copyFrom.Attributes.ToList().ForEach(a =>
                {
                    // if it already exists then don't copy
                    if (!copyTo.Attributes.ContainsKey(a.Key))
                    {
                        copyTo.Attributes.Add(a.Key, a.Value);
                    }
                });
            }
        }


        /// <summary>
        /// Removes an attribute from the entity attribute collection if that attribute exists.
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="AttributeName"></param>
        public static void RemoveAttribute(this Entity Target, string AttributeName)
        {
            if (Target.Contains(AttributeName))
            {
                Target.Attributes.Remove(AttributeName);
            }
        }

        public static void SetState(this IOrganizationService service, Entity target, int state, int status)
        {
            service.SetState(target, new OptionSetValue(state), new OptionSetValue(status));
        }

        public static void SetState(this IOrganizationService service, Entity target, OptionSetValue state, OptionSetValue status)
        {
            var request = new SetStateRequest() { EntityMoniker = target.ToEntityReference(), State = state, Status = status };
            service.Execute(request);
        }

        public static Entity ToEntity(this EntityReference reference)
        {
            if (reference == null) { return null; }
            return new Entity() { LogicalName = reference.LogicalName, Id = reference.Id };
        }

        public static T ToEntity<T>(this EntityReference reference) where T : Entity
        {
            return reference.ToEntity().ToEntity<T>();
        }

        public static AttributeMetadata GetAttributeMetadata(this IOrganizationService service, string entityLogicalName, string attributeLogicalName)
        {
            var request = new RetrieveAttributeRequest() { EntityLogicalName = entityLogicalName, LogicalName = attributeLogicalName };
            return (service.Execute(request) as RetrieveAttributeResponse).AttributeMetadata;
        }

        public static OptionMetadata GetOptionMetadata(this OptionSetValue value, IOrganizationService service, Entity entity, string attributeLogicalName)
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

        public static string GetOptionSetText(this OptionSetValue value, IOrganizationService service, Entity entity, string attributeLogicalName)
        {
            var optionMeta = GetOptionMetadata(value, service, entity, attributeLogicalName);
            return optionMeta.GetOptionSetText();
        }

        public static string GetOptionSetText(this OptionMetadata optionMeta)
        {
            if (optionMeta != null && optionMeta.Label != null && optionMeta.Label.UserLocalizedLabel != null) { return optionMeta.Label.UserLocalizedLabel.Label; }
            return string.Empty;
        }

        public static List<T> ToList<T>(this EntityCollection entities) where T : Entity
        {
            return entities.Entities.ToList<T>();
        }

        public static List<T> ToList<T>(this IEnumerable<Entity> entities) where T : Entity
        {
            return (from entity in entities select entity.ToEntity<T>()).ToList();
        }

    }
}

