using IdentityServer4.Models;
using System.Collections.Generic;

namespace Location.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("vehicle.scope"),
                new ApiScope("admin.scope"),
            };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "vehicle.client",
                ClientName = "Client Credentials vehicle",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "vehicle.scope" }
            },
            new Client
            {
                ClientId = "admin.client",
                ClientName = "Client Credentials admin",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "admin.scope" }
            }
        };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("Location Api"){
                    Scopes = new List<string>{ },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
            };
    }
}
