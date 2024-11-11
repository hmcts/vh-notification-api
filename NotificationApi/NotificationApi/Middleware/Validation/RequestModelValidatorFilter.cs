using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace NotificationApi.Middleware.Validation
{
    public class RequestModelValidatorFilter(
        IRequestModelValidatorService requestModelValidatorService,
        ILogger<RequestModelValidatorFilter> logger)
        : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            logger.LogDebug("Processing request");
            foreach (var property in context.ActionDescriptor.Parameters)
            {
                var valuePair = context.ActionArguments.SingleOrDefault(x => x.Key == property.Name);
                if (property.BindingInfo?.BindingSource == BindingSource.Body)
                {
                    var validationFailures = requestModelValidatorService.Validate(property.ParameterType, valuePair.Value);
                    context.ModelState.AddFluentValidationErrors(validationFailures);
                }
                
                if (valuePair.Value.Equals(GetDefaultValue(property.ParameterType)))
                {
                    context.ModelState.AddModelError(valuePair.Key, $"Please provide a valid {valuePair.Key}");

                }
            }

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();
                logger.LogWarning("Request Validation Failed: {Join}", string.Join("; ", errors));
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState));
            }
            else
            {
                await next();
            }
        }

        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
