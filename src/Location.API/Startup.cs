using FluentValidation.AspNetCore;
using GlobalErrorHandling.Extensions;
using Location.Common.Settings;
using Location.Service.Interfaces;
using Location.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace Location.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetSection("Identity").Get<IdentityConfig>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Location.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.Configure<CosmoDBConfig>(Configuration.GetSection("CosmosDB"));

            services.AddAuthentication("Bearer")
                        .AddIdentityServerAuthentication("Bearer", options =>
                        {
                            options.Authority = config.Server;
                        });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(config.Policies["Vehicle"], policy =>
                {
                    policy.RequireClaim("scope", config.Scopes["Vehicle"]);
                });
                options.AddPolicy(config.Policies["Admin"], policy =>
                {
                    policy.RequireClaim("scope", config.Scopes["Admin"]);
                });
            });

            services.AddControllers().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(Assembly.Load("Location.API"));
            });


            services.AddAutoMapper(Assembly.Load("Location.Service"));

            services.AddScoped<ICosmosDBService, CosmosDBService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<ILocationService, LocationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Location.API v1"));
            }

            app.UseSerilogRequestLogging();
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
