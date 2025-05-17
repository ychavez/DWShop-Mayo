using DWShop.Client.Infrastructure.Managers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Client.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IServiceCollection AddManagers(this IServiceCollection services) 
        {
            var managers = typeof(IManager);

            var types = managers.Assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new
                {

                    Service = x.GetInterface($"I{x.Name}"),
                    Implementarion = x
                })
                .Where(x => x.Service is not null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementarion);
                }
            }

            return services;
        
        }
    }
}
