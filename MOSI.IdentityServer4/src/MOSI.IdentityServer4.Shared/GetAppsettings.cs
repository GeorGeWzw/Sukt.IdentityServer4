using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Shared
{
    public class GetAppsettings
    {
        static Microsoft.Extensions.Configuration.IConfiguration Configuration { get; set; }
        static GetAppsettings()
        {
            Configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
        }
        public static string app(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }
                return Configuration[val.TrimEnd(':')];//去除最后一个冒号
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}
