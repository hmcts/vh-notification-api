using System;
using AdminWebsite.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NotificationApi.Common;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Helpers;
using NotificationApi.Common.Security;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Middleware.Logging;
using NotificationApi.Services;
using NotificationApi.Swagger;
using Notify.Client;
using Notify.Interfaces;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using ZymLabs.NSwag.FluentValidation;

namespace NotificationApi.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSingleton<FluentValidationSchemaProcessor>();
            var apiVersionDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
            foreach (var versionDescription in apiVersionDescription.ApiVersionDescriptions)
            {
                services.AddOpenApiDocument((configure, serviceProvider) =>
                {
                    ConfigureSwaggerForVersion(configure,serviceProvider, versionDescription.GroupName);
                });
            }
            return services;
        }


        private static void ConfigureSwaggerForVersion(AspNetCoreOpenApiDocumentGeneratorSettings configure, IServiceProvider serviceProvider, string version)
        {
            configure.DocumentName = version;
            configure.ApiGroupNames = new[] { version };
            configure.Title = "Notification API";
            configure.Version = version;
            configure.DocumentProcessors.Add(
                new SecurityDefinitionAppender("JWT",
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Type into the textbox: Bearer {your JWT token}.",
                        Scheme = "bearer"
                    }));
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            configure.OperationProcessors.Add(new AuthResponseOperationProcessor());
            var fluentValidationSchemaProcessor = serviceProvider.GetService<FluentValidationSchemaProcessor>();
            
            // Add the fluent validations schema processor
            configure.SchemaProcessors.Add(fluentValidationSchemaProcessor);
        }

        public static IServiceCollection AddCustomTypes(this IServiceCollection services)
        {
            services.AddScoped<IAsyncNotificationClient, NotificationClient>(sp =>
            {
                var notifyConfiguration = sp.GetService<IOptions<NotifyConfiguration>>().Value;
                return new NotificationClient(notifyConfiguration.ApiKey);
            });
            services.Decorate<IAsyncNotificationClient, AsyncNotificationClientLoggingDecorator>();

            services.AddMemoryCache();
            services.AddScoped<ILoggingDataExtractor, LoggingDataExtractor>();
            services.AddScoped<ITokenProvider, AzureTokenProvider>();
            services.AddSingleton<ITelemetryInitializer, AppInsightsTelemetry>();
            services.AddScoped<ICreateNotificationService, CreateNotificationService>();
            services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddScoped<IQueryHandler, QueryHandler>();

            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<ICommandHandler, CommandHandler>();
            RegisterCommandHandlers(services);
            RegisterQueryHandlers(services);

            services.AddSingleton<IPollyRetryService, PollyRetryService>();

            return services;
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            //Scruptor package : https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container/
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
