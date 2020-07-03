using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankOfDotNet.IdentitySvr.Data.Initialize;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankOfDotNet.IdentitySvr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var persistedDbContext = serviceProvider.GetRequiredService<PersistedGrantDbContext>();
                var configurationDbContext = serviceProvider.GetRequiredService<ConfigurationDbContext>();
                if (persistedDbContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
                {
                    persistedDbContext.Database.MigrateAsync().GetAwaiter().GetResult();
                }
                if (configurationDbContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
                {
                    configurationDbContext.Database.MigrateAsync().GetAwaiter().GetResult();
                }
                DbInitiazer.InitizeIdentityserverDatabase(persistedDbContext, configurationDbContext).GetAwaiter().GetResult();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
