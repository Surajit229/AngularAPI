using Microsoft.Extensions.DependencyInjection;
using Rota.Business;
using Rota.Business.Interfaces;
using Rota.Repository;
using Rota.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rota.API.Infrastructure
{
    public static class DependencyMappings
    {
        public static void DependencySetting(this IServiceCollection services)
        {
            //  Repositories dependency injections
            services.AddTransient<IRotaRepository, RotaRepository>();

            //  Services dependency injections
            services.AddTransient<IRotaService, RotaService>();
        }
    }
}
