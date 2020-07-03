using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankOfDotNet.IdentitySvr
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options=>
            {
                options.EnableEndpointRouting = false;
            });
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer().AddDeveloperSigningCredential()
                //.AddInMemoryApiResources(Config.GetAllApiResources())
                //.AddInMemoryClients(Config.GetClients())
                //.AddInMemoryApiScopes(Config.GetApiScopes())
                //.AddInMemoryApiIdentityResources(Config.GetAllIdentityApiResources())

                //configuration store -: clients and resources
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(migrationAssembly));
                }).
                //configuration operation : consents , codes , tokens, etc
                AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddTestUsers(Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseIdentityServer();           
            app.UseMvcWithDefaultRoute();
        }
    }
}
