using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankOfDotNet.IdentitySvr
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetAllApiIdentityResources()
        {
            return new List<IdentityResource>() { new IdentityResources.OpenId(), new IdentityResources.Profile() };
        }
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>() { new ApiResource {
                Name="bankOfDotNet",
                DisplayName ="Web API for Bank of dotnet"
            } };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "alice",
            Password = "password",

            Claims = new []
            {
                new Claim("name", "Alice"),
                new Claim("website", "https://alice.com")
            }
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "bob",
            Password = "password",

            Claims = new []
            {
                new Claim("name", "Bob"),
                new Claim("website", "https://bob.com")
            }
        }
    };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret>()
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes={ "bankOfDotNet" }
            },
            new Client()
            {
                ClientId = "mvc",
                ClientName = "Mvc application",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = {"http://localhost:64400/signin-oidc"},
                PostLogoutRedirectUris = { "http://localhost:64400/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,

                }
            }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
                   {
                 new ApiScope("bankOfDotNet")
            };
        }
    }
}
