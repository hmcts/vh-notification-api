using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NotificationApi.Common.Configuration;
using NotificationApi.DAL;
using NotificationApi.Extensions;
using NotificationApi.Middleware.Logging;
using NotificationApi.Middleware.Validation;

namespace NotificationApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddSwagger();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(host => true)
                        .AllowCredentials();
                }));

            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);

            services.AddJsonOptions();
            RegisterSettings(services);
            services.AddCustomTypes();
            RegisterAuth(services);
            services.AddTransient<IRequestModelValidatorService, RequestModelValidatorService>();

            services.AddMvc(opt => opt.Filters.Add(typeof(LoggingMiddleware))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(opt => opt.Filters.Add(typeof(RequestModelValidatorFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IRequestModelValidatorService>());
            services.AddTransient<IValidatorFactory, RequestModelValidatorFactory>();

            services.AddDbContextPool<NotificationsApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VhNotificationsApi")));
        }

        private void RegisterSettings(IServiceCollection services)
        {
            services.Configure<AzureAdConfiguration>(options => Configuration.Bind("AzureAd", options));
            services.Configure<ServicesConfiguration>(options => Configuration.Bind("Services", options));
            services.Configure<NotifyConfiguration>(options => Configuration.Bind("NotifyConfiguration", options));
        }

        private void RegisterAuth(IServiceCollection serviceCollection)
        {
            var securitySettings = Configuration.GetSection("AzureAd").Get<AzureAdConfiguration>();
            var serviceSettings = Configuration.GetSection("Services").Get<ServicesConfiguration>();

            serviceCollection.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{securitySettings.Authority}{securitySettings.TenantId}";
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                        ValidAudience = serviceSettings.VhNotificationApiResourceId
                    };
                });

            serviceCollection.AddAuthorization(AddPolicies);
            serviceCollection.AddMvc(AddMvcPolicies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.RunLatestMigrations();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                const string url = "/swagger/v1/swagger.json";
                c.SwaggerEndpoint(url, "Notifications API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            
            app.UseAuthorization();
            
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
            
            app.UseMiddleware<LogResponseBodyMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }

        private static void AddPolicies(AuthorizationOptions options)
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        }

        private static void AddMvcPolicies(MvcOptions options)
        {
            options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build()));
        }
    }
}
