using DWShop.Infrastructure.Context;
using DWShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DWShop.Infrastructure.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AuditableContext>(options =>
             options.UseSqlServer("default"));

            services.AddScoped(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>));

        }
    }
}
