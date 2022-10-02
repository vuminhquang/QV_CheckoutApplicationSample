using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PosApplication.WebHelpers.Rest.Problems
{
    public class ValidationFailedProblem : ProblemDetailsException
    {
        public ValidationFailedProblem(ModelStateDictionary modelState) : base(new ValidationProblemDetails(modelState))
        {
        }
    }
}
