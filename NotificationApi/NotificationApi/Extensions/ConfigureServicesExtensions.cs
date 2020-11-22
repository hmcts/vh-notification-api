using System;
using System.IO;
using System.Reflection;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NotificationApi.Common;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Helpers;
using NotificationApi.Common.Security;
using NotificationApi.Contract;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Services;
using Notify.Client;
using Notify.Interfaces;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace NotificationApi.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            var contractsXmlFile = $"{typeof(IAssemblyReference).Assembly.GetName().Name}.xml";
            var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXmlFile);


            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo {Title = "Notifications API", Version = "v1"});
            //     c.AddFluentValidationRules();
            //     c.IncludeXmlComments(xmlPath);
            //     c.IncludeXmlComments(contractsXmlPath);
            //     c.EnableAnnotations();
            //
            //     c.AddSecurityDefinition("Bearer", //Name the security scheme
            //         new OpenApiSecurityScheme
            //         {
            //             Description = "JWT Authorization header using the Bearer scheme.",
            //             Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
            //             Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
            //         });
            //
            //     c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //     {
            //         {
            //             new OpenApiSecurityScheme
            //             {
            //                 Reference = new OpenApiReference
            //                 {
            //                     Id = "Bearer", //The name of the previously defined security scheme.
            //                     Type = ReferenceType.SecurityScheme
            //                 }
            //             },
            //             new List<string>()
            //         }
            //     });
            //     c.OperationFilter<AuthResponsesOperationFilter>();
            // });
            // services.AddSwaggerGenNewtonsoftSupport();
            services.AddOpenApiDocument(document =>
            {
                document.Title = "Notification API";
                document.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("JWT",
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            In = OpenApiSecurityApiKeyLocation.Header,
                            Description = "Type into the textbox: Bearer {your JWT token}.",
                            Scheme = "bearer"
                        }));
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            return services;
        }

        public static IServiceCollection AddCustomTypes(this IServiceCollection services)
        {
            services.AddScoped<IAsyncNotificationClient, NotificationClient>(sp =>
            {
                var notifyConfiguration = sp.GetService<IOptions<NotifyConfiguration>>().Value;
                return new NotificationClient(notifyConfiguration.ApiKey);
            });

            services.AddScoped<INotificationService, NotificationService>();
            
            services.AddMemoryCache();
            services.AddScoped<ILoggingDataExtractor, LoggingDataExtractor>();
            services.AddScoped<ITokenProvider, AzureTokenProvider>();
            services.AddSingleton<ITelemetryInitializer, BadRequestTelemetry>();

            services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddScoped<IQueryHandler, QueryHandler>();

            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<ICommandHandler, CommandHandler>();
            RegisterCommandHandlers(services);
            RegisterQueryHandlers(services);
                        
            return services;
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<ICommand>()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>))
                    .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            serviceCollection.Decorate(typeof(ICommandHandler<>), typeof(CommandHandlerLoggingDecorator<>));
        }

        private static void RegisterQueryHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<IQuery>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>))
                    .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            serviceCollection.Decorate(typeof(IQueryHandler<,>), typeof(QueryHandlerLoggingDecorator<,>));
        }

        public static IServiceCollection AddJsonOptions(this IServiceCollection serviceCollection)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            serviceCollection.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = contractResolver;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            return serviceCollection;
        }
    }
}
