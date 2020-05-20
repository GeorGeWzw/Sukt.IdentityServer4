using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MOSI.IdentityServer4.EntityFramework.MySql.Extensions;
using MOSI.IdentityServer4.Identity.SeedData;
using MOSI.IdentityServer4.Models;
using MOSI.IdentityServer4.Models.ConsulModel;
using MOSI.IdentityServer4.Shared;
using MOSI.IdentityServer4.Shared.AuthenticationExtensions;
using MOSI.IdentityServer4.Shared.ConsulServiceRegister;
using MOSI.IdentityServer4.Shared.DbContexts;
using MOSI.IdentityServer4.Shared.Help;

namespace MOSI.IdentityServer4.Identity
{
    public class Startup
    {
         ////http://localhost:5009/.well-known/openid-configuration        浏览器输入此路径查看接口


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            environment = hostEnvironment;
        }
        public IWebHostEnvironment environment;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = GetAppsettings.app(new string[] { "ConnectionStrings", "MySqlDbConnectString" });
            //services.AddScoped<IUserClaimsPrincipalFactory<UserIdentity>, MyUserClaimsPrincipal>();
            
            services.RegisterMySqlDbContexts(connection);//注册上下文连接
            services.AddAuthenticationServices();//配置授权使用的用户实体和Role实体
            ///配置客户端授权认证等
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                //options.IssuerUri = "http://localhost:5009/";
            }).AddAspNetIdentity<UserIdentity>()
            //.AddInMemoryClients(Config.GetClients())
            //.AddInMemoryApiResources(Config.GetApiResources())
            //.AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddProfileService<CustomProfileService>()
            .AddConfigurationStore<MosiConfigurationDbContext>()
            .AddOperationalStore<MosiPersistedGrantDbContext>();
            services.AddMvc();//添加MVC
            //services.AddClaimsPrincipalFactory<UserClaimsPrincipal>();
            //if (environment.IsDevelopment())
            //{
            builder.AddDeveloperSigningCredential();
            //}
            //配置授权
            services.AddAuthentication();
            ///配置跨域
            List<string> Corelist = new List<string>()
            {
                "http://localhost:8080",
                "http://127.0.0.1:2365"
            };
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Corelist.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.AspNetCore.Hosting.IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Consul
            ConsulServiceEntity serviceEntity = new ConsulServiceEntity
            {
                IP = NetworkHelper.LocalIPAddress,
                Port = Convert.ToInt32(Configuration["Service:Port"]),
                ServiceName = Configuration["Service:Name"],
                ConsulIP = Configuration["Consul:IP"],
                ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
            };
            app.RegisterConsul(lifetime, serviceEntity);
            #endregion
            app.UseCors("default");
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.InitializeDatabaseConfiguration();//写入初始化数据
        }

        

    }
}
