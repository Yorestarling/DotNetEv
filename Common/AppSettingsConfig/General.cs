using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AppSettingsConfig
{
    public class General
    {
        public JWTConfigurations? JWTConfigurations { get; set; }
    }

    public class JWTConfigurations
    {
        public int ExpirationInMinutes { get; set; }
        public string? SecretKey { get; set; }
        public bool ValidateAudience { get; set; }
    }

}
