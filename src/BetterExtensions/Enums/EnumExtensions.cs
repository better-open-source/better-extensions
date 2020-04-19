using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BetterExtensions.Enums
{
    /// <summary>
    /// Enum Extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Select attributes for enum value
        /// </summary>
        /// <param name="enum">Enum value</param>
        /// <typeparam name="TAttr">Selected attribute type</typeparam>
        /// <returns>Attributes sequence</returns>
        public static IEnumerable<TAttr> SelectAttributes<TAttr>(this Enum @enum) 
            where TAttr : Attribute =>
            @enum
                .GetType()
                .GetMember(@enum.ToString())
                .Where(mi => mi.MemberType == MemberTypes.Field)
                .SelectMany(mi => mi.GetCustomAttributes(typeof(TAttr), false).Cast<TAttr>());
        
        /// <summary>
        /// Select sequence of attributes parameter from enum 
        /// </summary>
        /// <param name="enum">Enum value</param>
        /// <param name="selector">Selector</param>
        /// <typeparam name="TAttr">Selected attribute type</typeparam>
        /// <typeparam name="TResult">Excepted result type</typeparam>
        /// <returns>TResult sequence</returns>
        public static IEnumerable<TResult> SelectAttributeValues<TAttr, TResult>(this Enum @enum, Func<TAttr, TResult> selector) 
            where TAttr : Attribute =>
            @enum.SelectAttributes<TAttr>()
                .Select(selector);

        /// <summary>
        /// Select single attribute parameter from enum
        /// </summary>
        /// <param name="enum">Enum value</param>
        /// <param name="selector">Selector</param>
        /// <typeparam name="TAttr">Selected attribute type</typeparam>
        /// <typeparam name="TResult">Excepted result type</typeparam>
        /// <returns>TResult</returns>
        public static TResult SingleAttributeValue<TAttr, TResult>(this Enum @enum, Func<TAttr, TResult> selector) 
            where TAttr : Attribute => 
            selector(@enum.SelectAttributes<TAttr>().FirstOrDefault());
    }
}