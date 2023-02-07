using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Singleton - objects are the same for every object and every request.

            // Scoped: bjects are the same within a request, but different across different requests.
            services.AddScoped<IProductsService, ProductsService>();

            // Transient - objects are always different; a new instance is provided to every controller and every service.
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppProfile));
        }
    }
}
