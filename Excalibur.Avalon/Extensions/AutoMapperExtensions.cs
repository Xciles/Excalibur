using System.ComponentModel;
using System.Reflection;
using AutoMapper;
using Excalibur.Avalon.Utils;

namespace Excalibur.Avalon.Extensions
{
    /// <summary>
    /// Class that is used to automatically ignore properties when mapping with AutoMapper.
    /// It ignores based on an attribute
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Adds an ignore for marked properties
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="expression">An AutoMapper mapping expression</param>
        /// <returns>The same mapping expression for fluent usage</returns>
        public static IMappingExpression<TSource, TDestination> IgnoreDoNotMap<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            foreach (var propertyInfo in sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var descriptor = TypeDescriptor.GetProperties(sourceType)[propertyInfo.Name];
                var attribute = (DoNotMapAttribute)descriptor.Attributes[typeof(DoNotMapAttribute)];

                if (attribute != null)
                {
                    expression.ForMember(propertyInfo.Name, source => source.Ignore());
                }
            }

            return expression;
        }
    }
}