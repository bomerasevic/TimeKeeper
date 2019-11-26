using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using System.IdentityModel;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using TimeKeeper.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace TimeKeeper.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() });
            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented);

            services.AddAuthorization(o =>
            {
                o.AddPolicy("IsMember", builder =>  // pravimo policy na osnovu role i attribute
                {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new IsMemberRequirement());
                });
                o.AddPolicy("IsAdmin", builder =>  // pravimo policy na osnovu role i attribute
                {
                    builder.RequireRole("admin");
                });
                o.AddPolicy("IsLead", builder =>  // pravimo policy na osnovu role i attribute
                {
                    builder.RequireRole("lead");
                });
                o.AddPolicy("IsMemberOnProject", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new HasAccessToProjects());
                });
                o.AddPolicy("IsEmployee", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new HasAccessToEmployee());
                });
                o.AddPolicy("IsMemberInTeam", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new HasAccessToMembers());
                });
                o.AddPolicy("IsCustomer", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new HasAccessToCustomers());
                });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:44300";
                o.Audience = "timekeeper";
                o.RequireHttpsMetadata = false;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddAuthentication(o =>
            //{
            //    o.DefaultScheme = "Cookies";
            //    o.DefaultChallengeScheme = "oidc";  // trazi i id_token i token
            //}).AddCookie("Cookies", o =>
            //{
            //    o.AccessDeniedPath = "/AccessDenied";
            //});
              //.AddOpenIdConnect("oidc", o =>
              //{
              //    o.SignInScheme = "Cookies";
              //    o.Authority = "https://localhost:44300";
              //    o.ClientId = "tk2019";  // klijent kojeg smo prijavili
              //    o.ClientSecret = "mistral_talents";
              //    o.ResponseType = "code id_token";
              //    o.Scope.Add("openid");  // identitet usera koji je prijavljen
              //    o.Scope.Add("profile");
              //    //o.Scope.Add("address");
              //    o.Scope.Add("roles");
              //    o.Scope.Add("timekeeper");
              //    o.Scope.Add("teams");
              //    o.SaveTokens = true;
              //    o.GetClaimsFromUserInfoEndpoint = true;
              //    o.ClaimActions.MapUniqueJsonKey("address", "address");
              //    o.ClaimActions.MapUniqueJsonKey("role", "role");
              //    o.ClaimActions.MapUniqueJsonKey("team", "team");
              //    //o.TokenValidationParameters = new TokenValidationParameters
              //    //{
              //    //    NameClaimType = JwtClaimTypes.Name,
              //    //    RoleClaimType = JwtClaimTypes.Role
              //    //};
              //})
              

            //services.AddAuthentication("BasicAuthentication")
            //        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            string connectionString = Configuration["ConnectionString"];
            services.AddDbContext<TimeKeeperContext>(o => { o.UseNpgsql(connectionString); });

            services.AddScoped<IAuthorizationHandler, IsMemberHandler>();
            services.AddScoped<IAuthorizationHandler, IsAdminHandler>();
            services.AddScoped<IAuthorizationHandler, IsMemberOnProjectHandler>();
            services.AddScoped<IAuthorizationHandler, IsMemberInTeamHandler>();
            services.AddScoped<IAuthorizationHandler, IsEmployeeHandler>();
            services.AddScoped<IAuthorizationHandler, IsCustomerHandler>();
            
            services.Configure<IISOptions>(o =>
              {
                  o.AutomaticAuthentication = false;  // vezana za windows; ne koristi se windows autentikacija/niti od internet information servera
              });

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCors(c => c.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
