using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;

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


        public static T GetAliasedValue<T>(this Entity Target, string EntityAlias, string FieldName, T DefaultValue)
        {
            string key = string.Format("{0}.{1}", EntityAlias, FieldName);
            if (!Target.Contains(key) || Target[key] is null)
            {
                return DefaultValue;
            }

            AliasedValue value = Target.GetAttributeValue<AliasedValue>(key);
            return (T)value.Value;
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

    }
}

