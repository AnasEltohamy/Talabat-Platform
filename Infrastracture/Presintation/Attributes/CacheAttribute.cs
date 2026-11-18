using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presintation.Attributes
{
    public class CacheAttribute: ActionFilterAttribute
    {
        private readonly int _durationInSec;

        public CacheAttribute(int durationInSec = 90)
        {
            _durationInSec = durationInSec;
        }
         
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices
                                      .GetRequiredService<ICacheService>();

            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            // 1) الـ Key موجود؟ رجّعه ومتنفّذش الـ Action
            var cachedValue = await cacheService.GetAsync(cacheKey);
            if (cachedValue is not null)
            {
                context.Result = new ContentResult
                {
                    Content = cachedValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // 2) مش موجود؟ نفّذ الـ Action وخزّن النتيجة
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okResult && okResult.Value is not null)
            {
                await cacheService.SetAsync
                    (
                        cacheKey,
                        okResult.Value, 
                        TimeSpan.FromSeconds(_durationInSec)
                    );
            }
        }




        // المسار + الـ Query Parameters مرتبين، عشان كل Request مختلف ياخد Key مختلف
        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q => q.Key))
                key.Append($"|{item.Key}-{item.Value}");

            return key.ToString();
        }
    }
}
