using DomainLayer.Exceptions;
using Service.Identity;
using Shared.ErrorModels;

namespace E_Commerce_Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWares
    {
        private readonly RequestDelegate _nextRequest;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleWares(RequestDelegate NextRequest, ILogger<CustomExceptionHandlerMiddleWares> logger)
        {
            _nextRequest = NextRequest;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _nextRequest.Invoke(httpContext);

                if (httpContext.Response.HasStarted) return;

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await httpContext.Response.WriteAsJsonAsync(new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"End Point {httpContext.Request.Path} Not Found"
                    });
                }
                else if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    await httpContext.Response.WriteAsJsonAsync(new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Unauthorized"
                    });
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
            var Response = new ErrorToReturn()
            {
                Message = ex.Message,
            };

            Response.StatusCode = ex switch
            {
                NotFoundExcption => StatusCodes.Status404NotFound,
                UnauthorizedUserException => StatusCodes.Status401Unauthorized,
                BadrequestExcption Badrequest => GetBadrequestErrors(Badrequest, Response),
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = Response.StatusCode;   // السطر الناقص

            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadrequestErrors(BadrequestExcption badrequest, ErrorToReturn? response)
        {
            response.Errors = badrequest.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}