using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InternetShopDBContext.Lib.RepositoryRegistration
{
    public static class ServiceRegistrationHelper
    {
        public static Type SearchRepositoryForInterface(Type interfaceType, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && type.GetInterfaces().Contains(interfaceType)) return type;
            }
            return null;
        }
    }
}
