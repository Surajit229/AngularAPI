using System;
using System.Collections.Generic;
using System.Text;

namespace Rota.Utility
{
    public static class AppSettings
    {
        public static string JwtKey { get; set; }
        public static string JwtIssuer { get; set; }
        public static string TokenExpiry { get; set; }
    }
}
