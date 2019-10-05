using BaseModelLibrary.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InternetShopDBContext.Lib.RepositoryRegistration.AttributeBasedRegistration
{
    /// <summary>
    /// All interfaces marked by <see cref="CustomRepositoryRegistrationAttribute"/>
    /// </summary>
    public static class AttributeBasedServiceRegistration
    {
        public static void RegisterService(IServiceCollection services)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var serviceAttributeType = typeof(CustomRepositoryRegistrationAttribute);

            foreach (var serviceAssembly in assemblies)
            {
                var interfacesWithAttribute = serviceAssembly.GetTypes().Where(t => t.IsInterface && t.GetCustomAttribute(serviceAttributeType) != null);
                foreach (Type type in interfacesWithAttribute)
                {
                    var classForInterface = ServiceRegistrationHelper.SearchRepositoryForInterface(type, serviceAssembly);
                    if (classForInterface == null) continue;
                    services.AddScoped(type, classForInterface);
                }
            }
        }
    }
}
