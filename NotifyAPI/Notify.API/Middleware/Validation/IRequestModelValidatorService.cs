using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Notify.API.Middleware.Validation
{
    public interface IRequestModelValidatorService
    {
        IList<ValidationFailure> Validate(Type requestModel, object modelValue);
    }
}