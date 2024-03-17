using AutoMapper;
using System.Reflection;

namespace Infrastructure.Mapping
{
    public static class AutoMapperExtension
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonSetting<TSource, TDestination>(
                       this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationType)
            {
                if (sourceType.GetProperty(property.Name) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}
