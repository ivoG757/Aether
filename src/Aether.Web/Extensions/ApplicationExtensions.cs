using System.Reflection;

namespace Aether.Web.Extensions
{

    public static class ApplicationExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection,
            Type repositoryType)
        {
            Assembly repositoriesAssembly = repositoryType.Assembly;

            IEnumerable<Type> repositoryInterfaces = repositoriesAssembly
                .GetTypes()
                .Where(t => t.IsInterface &&
                            t.Name.StartsWith("I") && t.Name.EndsWith("Repository"))
                .ToArray();

            foreach (Type serviceType in repositoryInterfaces)
            {
                Type implementationType = repositoriesAssembly
                    .GetTypes()
                    .First(t => t is { IsClass: true, IsAbstract: false } &&
                                 serviceType.IsAssignableFrom(t));

                serviceCollection.AddScoped(serviceType, implementationType);
            }

            return serviceCollection;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection,
            Type serviceType)
        {
            Assembly servicesAssembly = serviceType.Assembly;

            IEnumerable<Type> serviceInterfaces = servicesAssembly
                .GetTypes()
                .Where(t => t.IsInterface &&
                            t.Name.StartsWith("I") && t.Name.EndsWith("Service"))
                .ToArray();

            foreach (Type currentServiceType in serviceInterfaces)
            {
                Type implementationType = servicesAssembly
                    .GetTypes()
                    .First(t => t is { IsClass: true, IsAbstract: false } && 
                                 currentServiceType.IsAssignableFrom(t));

                // this will not behave correctly if there are multiple implementations of the same interface,
                // but for now we have only one implementation per interface

                serviceCollection.AddScoped(currentServiceType, implementationType);
            }

            return serviceCollection;
        }
    }
}