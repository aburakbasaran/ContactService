using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ContactService.Application.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void Register<TContext>(this ModelBuilder builder, TContext context, IEnumerable<Assembly> fromAssemblies) where TContext : BaseDbContext
        {
            Type type = typeof(IModelBuilder<>).MakeGenericType(context.GetType());
            IEnumerable<TypeInfo> types = fromAssemblies.SelectMany(x => x.DefinedTypes).Where(c => c.IsAssignableTo(type));

            if (types?.Any() == true)
            {
                builder.RegisterEntities(types);
                builder.RegisterCustomMappings(types);
            }
        }

        private static void RegisterEntities(this ModelBuilder builder, IEnumerable<Type> fromTypes)
        {
            IEnumerable<Type> concreteTypes = fromTypes
                .Where(x => !x.GetTypeInfo().IsAbstract
                            && !x.GetTypeInfo().IsInterface
                            && x.BaseType != null
                            && x.IsConcreteOfAggregateRoot());

            foreach (Type concreteType in concreteTypes)
            {
                builder.Entity(concreteType);
            }
        }

        private static void RegisterCustomMappings(this ModelBuilder builder, IEnumerable<Type> fromTypes)
        {
            foreach (Type builderType in fromTypes)
            {
                builderType.GetMethod(nameof(IModelBuilder<BaseDbContext>.Build))
                    .Invoke(Activator.CreateInstance(builderType), new object[] { builder });
            }
        }

        private static bool IsConcreteOfAggregateRoot(this Type type)
        {
            Type baseType = type.GetTypeInfo().BaseType;
            return baseType != null && (typeof(BaseEntity).IsAssignableFrom(type)
                                        || baseType.IsGenericType &&
                                        typeof(BaseEntity).IsAssignableFrom(baseType.GetGenericTypeDefinition()));
        }
    }
}
