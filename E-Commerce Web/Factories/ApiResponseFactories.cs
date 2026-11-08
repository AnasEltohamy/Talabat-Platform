using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.ErrorModels;

namespace E_Commerce_Web.Factories
{
    public static class ApiResponseFactories
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
        {
            var Errors = context.ModelState
                    .Where(M => M.Value.Errors.Any())
                    .Select(M => new ValidationError
                    {
                        Field = M.Key,
                        ErrorList = M.Value.Errors.Select(E => E.ErrorMessage)
                    });

            var Response = new ValidationErrorToReturn()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMassage = "Validation Error",
                ValidationErrors = Errors
            };

            return new BadRequestObjectResult(Response);

        }
    }
}
