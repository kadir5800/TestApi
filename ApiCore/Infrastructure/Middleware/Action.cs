namespace ApiCore.Infrastructure.Middleware
{
    public class Action
    {
        
        private readonly RequestDelegate _next;

        public Action(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/Api"))
            {
                // header'ın değeri null ya da boş ise hata fırlat
                    if (string.IsNullOrEmpty(context.Request.Headers["testapi-token"]))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("testapi-token Zorunlu");
                    return;
                }
                else
                {
                    var token=context.Request.Headers["testapi-token"];
                    var clientContext = context.RequestServices.GetService<IClientContext>();
                    clientContext.SetCulture("tr:TR");
                    clientContext.SetToken(token);
                    clientContext.SetUserId(10);
                    //var _authenticationManager = context.RequestServices.GetService<IMobileManager>();
                    await _next(context);
                    return;
                }
               
            }
            else
            {
                await _next(context);
            }

            // header'ın değeri geçerli ise diğer middleware'lere devam et
            await _next(context);
        }
    }

    public static class ActionExtensions
    {
        public static IApplicationBuilder UseActionExtensions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Action>();
        }
    }
}
