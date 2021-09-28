using IdentityServer4.Models;
using Location.Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace Location.Identity
{
    public class Startup
    {
        private readonly IEnumerable<IdentityResource> IdentityResources =
        new IdentityResource[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
        };

        private IEnumerable<ApiScope> ApiScopes { get; set; }
        private IEnumerable<IdentityServer4.Models.Client> Clients;
        private IEnumerable<ApiResource> Apis;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetSection("Identity").Get<IdentityConfig>();
            InitIdentityConfigurations(config);

            var builder = services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryClients(Clients)
                .AddInMemoryIdentityResources(IdentityResources)
                .AddInMemoryApiResources(Apis)
                .AddInMemoryApiScopes(ApiScopes);

            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }

        private void InitIdentityConfigurations(IdentityConfig config)
        {
            var apiScopes = new List<ApiScope>();
            config.Scopes.Values.ToList().ForEach(o =>
            {
                apiScopes.Add(new ApiScope(o));
            });
            ApiScopes = apiScopes.AsEnumerable();

            var clients = new List<IdentityServer4.Models.Client>();
            config.Clients.ForEach(o =>
            {
                clients.Add(new IdentityServer4.Models.Client()
                {
                    ClientId = o.Id,
                    ClientName = o.Name,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(o.Secret.Sha256()) },
                    AllowedScopes = o.AllowedScopes
                });
            });
            Clients = clients.AsEnumerable();

            Apis = new List<ApiResource>
            {
                new ApiResource("Location Api"){
                    Scopes = new List<string>{ },
                    ApiSecrets = new List<Secret>{ new Secret(config.SuperSecret.Sha256()) }
                }
            };
        }
    }
}
