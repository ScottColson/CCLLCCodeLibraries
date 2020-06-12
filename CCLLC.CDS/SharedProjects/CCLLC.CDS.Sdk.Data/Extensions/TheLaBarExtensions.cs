using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;


namespace CCLLC.CDS.Sdk
{
    /// <summary>
    /// Extensions classes that support creating an array of column names from a Lambda expression.
    /// <credit>
    ///   Daryl LaBar @ https://github.com/daryllabar/DLaB.Xrm/blob/master/DLaB.Xrm.Core/Extensions/Extensions.cs 
    /// </credit>
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates an array of attribute names array from an Anonymous Type Initializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymousTypeInitializer">The anonymous type initializer.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">lambda must return an object initializer</exception>
        public static string[] GetAttributeNamesArray<T>(this Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            var initializer = anonymousTypeInitializer.Body as NewExpression;
            if (initializer?.Members == null)
            {
                throw new ArgumentException("lambda must return an object initializer");
            }

            // Search for and replace any occurrence of Id with the actual Entity's Id
            return initializer.Members.Select(GetLogicalAttributeName<T>).ToArray();
        }

        /// <summary>
        /// Retrieve the logical name for a property from its attribute tag.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetLogicalAttributeName<T>(MemberInfo property) where T : Entity
        {
            var name = property.Name;
            var attribute = typeof(T).GetProperty(name)?.GetCustomAttributes<AttributeLogicalNameAttribute>().FirstOrDefault();
            
            return attribute?.LogicalName 
                ?? throw new Exception(string.Format("Unable to retrieve AttributeLogicalName for property {0}", name));
        }

        /// <summary>
        /// Gets the class level attribute based on type.
        /// </summary>
        /// <remarks>Taken from https://stackoverflow.com/questions/2656189/how-do-i-read-an-attribute-on-a-class-at-runtime </remarks>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TAttribute GetClassAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
        }

    }
}
