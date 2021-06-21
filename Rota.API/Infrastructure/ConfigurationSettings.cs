using Microsoft.Extensions.Configuration;
using Rota.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rota.API.Infrastructure
{
    public static class ConfigurationSettings
    {
        public static void Configure(IConfiguration Configuration)
        {            
            AppSettings.JwtKey = Convert.ToString(Configuration["Jwt:Key"]);
            AppSettings.JwtIssuer = Convert.ToString(Configuration["Jwt:Issuer"]);
            AppSettings.TokenExpiry = Convert.ToString(Configuration["Jwt:TokenExpiry"]);
        }
    }
}
