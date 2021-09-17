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
                ClientSecrets = { new Secret("3fbd0681-2163-4135-bb62-0c048f3700de".Sha256()) },
                AllowedScopes = { "vehicle.scope" }
            },
            new Client
            {
                ClientId = "admin.client",
                ClientName = "Client Credentials admin",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("4f7e8675-0e22-422a-844d-9dc85a274237".Sha256()) },
                AllowedScopes = { "admin.scope" }
            }
        };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("Location Api"){
                    Scopes = new List<string>{ },
                    ApiSecrets = new List<Secret>{ new Secret("9f418a99-f083-484c-9340-e637c3eff7eb".Sha256()) }
                }
            };
    }
}
