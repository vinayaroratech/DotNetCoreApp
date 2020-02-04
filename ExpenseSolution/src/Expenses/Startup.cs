using Expenses.Data.Access.Constants;
using Expenses.Data.Access.DAL;
using Expenses.Data.Model.EFClasses;
using Expenses.Filters;
using Expenses.IoC;
using Expenses.Security.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Expenses
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateLogger("");
            logger.LogInformation("Got Log Configuraiton");

            services.AddSingleton(logger);
            services.AddApplicationInsightsTelemetry(Configuration);

            ContainerSetup.Setup(services, Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (o) =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = TokenAuthOption.Key,
                    ValidAudience = TokenAuthOption.Audience,
                    ValidIssuer = TokenAuthOption.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            // Add framework services.
            services.AddMvc(options => { options.Filters.Add(new ApiExceptionFilter()); });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Expenses", Version = "v1" });
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            InitDatabase(app);

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }
            else
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    //endpoints.MapControllers();
                });
                app.UseExceptionHandler("/Home/Error");
            }
        }

        private void InitDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MainDbContext>();
                context.Database.EnsureCreated();

                context.Add(new User()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    IsDeleted = false,
                    Password = "admin",
                    Username = "admin",
                    Roles = new List<UserRole>(){
                        new UserRole() {
                        Role = new Role() { Id = (int)Roles.Administrator, Name = Roles.Administrator.ToString() }
                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
