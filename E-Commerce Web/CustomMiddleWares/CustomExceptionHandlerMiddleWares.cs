using DomainLayer.Exceptions;
using Service.Identity;
using Shared.ErrorModels;

namespace E_Commerce_Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWares
    {
        private readonly RequestDelegate _nextRequest;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleWares(RequestDelegate NextRequest,ILogger<CustomExceptionHandlerMiddleWares> logger)
        {
            _nextRequest = NextRequest;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try {
                await _nextRequest.Invoke(httpContext);
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMassage = $"End Point {httpContext.Request.Path} Not Found"
                    };

                    await httpContext.Response.WriteAsJsonAsync(Response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Custom Exception Handler MiddleWares Wrong");
                
                 await HandelExceptionAsync(httpContext, ex);

            }
        }

        private async static Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // 1- Response Object

            var Response = new ErrorToReturn()
            {
                //StatusCode = httpContext.Response.StatusCode,
                ErrorMassage = ex.Message,
            };


            // 2- Set status Code For Response
            Response.StatusCode = ex switch
            {
                NotFoundExcption => StatusCodes.Status404NotFound,
                UnauthorizedUserException => StatusCodes.Status401Unauthorized,
                BadrequestExcption Badrequest => GetBadrequestErrors(Badrequest, Response),
                _ => StatusCodes.Status500InternalServerError
            };
           
            // 3- Return Object As JSON

            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadrequestErrors(BadrequestExcption badrequest, ErrorToReturn? response)
        {
            response.Errors = badrequest.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
