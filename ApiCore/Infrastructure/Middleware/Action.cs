using Business.DTO.BaseObjects;
using Business.IMeneger;

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
                if (string.IsNullOrEmpty(context.Request.Headers["Zapi-Token"]))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Zapi-Token Zorunlu");
                    return;
                }
                else
                {
                    var token = context.Request.Headers["Zapi-Token"];
                    var clientContext = context.RequestServices.GetService<IClientContext>();
                    var _authenticationManager = context.RequestServices.GetService<ITokenControl>();
                    var user = _authenticationManager.getToken(token);
                    if (!user.Status)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Zapi-Token Zorunlu");
                        return;
                    }

                    clientContext.SetToken(user.token);
                    clientContext.SetUserId(user.Id);

                    await _next(context);
                }

            }
            else
            {
                await _next(context);
            }
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
