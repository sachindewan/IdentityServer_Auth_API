using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankOfDotNet.IdentitySvr.Data.Initialize
{
    public class DbInitiazer
    {
        public async  static Task InitizeIdentityserverDatabase(PersistedGrantDbContext persistedGrantDbContext,ConfigurationDbContext configurationDbContext)
        {
            if (!configurationDbContext.Clients.Any())
            {
                foreach(var clients in Config.GetClients())
                await configurationDbContext.Clients.AddAsync(clients.ToEntity());
                configurationDbContext.SaveChangesAsync().GetAwaiter().GetResult();
            }
            if (!configurationDbContext.ApiResources.Any())
            {
                foreach (var apiResource in Config.GetAllApiResources())
                    await configurationDbContext.ApiResources.AddAsync(apiResource.ToEntity());
                configurationDbContext.SaveChangesAsync().GetAwaiter().GetResult();

            }
            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var identityResource in Config.GetAllApiIdentityResources())
                    await configurationDbContext.IdentityResources.AddAsync(identityResource.ToEntity());
                configurationDbContext.SaveChangesAsync().GetAwaiter().GetResult();

            }
        }
    }
}
