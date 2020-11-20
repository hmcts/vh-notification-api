using System;
using FluentValidation;

namespace NotificationApi.Middleware.Validation
{
    public class RequestModelValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestModelValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            var validator = (IValidator)_serviceProvider.GetService(validatorType);
            if(validator == null) throw new InvalidOperationException($"No validator found for {validatorType}");
            return validator;
        }
    }
}
