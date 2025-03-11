using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace QLNS.Provider
{
    public class OptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public OptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return BeginInvoke(context);
        }

        private Task BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                context.Response.StatusCode = 200;
                return context.Response.WriteAsync("OK");
            }

            return _next.Invoke(context);
        }
    }

    public static class OptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
    }

    public class CheckAcessMiddleware
    {
        private readonly RequestDelegate _next;
        public CheckAcessMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value!.Contains("Manager"))
            {
                string authHeader = httpContext.Request.Headers["Authorization"]!;
                if (string.IsNullOrEmpty(authHeader))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(authHeader.Replace("Bearer ", ""));
                var tokenS = jsonToken as JwtSecurityToken;
                var userToken = JsonConvert.DeserializeObject<object>(tokenS!.Claims.First(claim => claim.Type == "UserData").Value);

                //if (userToken!.Rules!.Contains("FULL"))
                //{
                //    await _next(httpContext);
                //    return;
                //}
                //Console.WriteLine("CheckAcessMiddleware: Cấm truy cập");
                //httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                // Thiết lập Header cho HttpResponse
                httpContext.Response.Headers.Add("throughCheckAcessMiddleware", new[] { DateTime.Now.ToString() });
                Console.WriteLine("CheckAcessMiddleware: Cho truy cập");
                // Chuyển Middleware tiếp theo trong pipeline
                await _next(httpContext);
            }
        }
    }
}
