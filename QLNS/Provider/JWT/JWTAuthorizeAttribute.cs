using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using QLNS.Models.Auth;
using QLNS.Services;

namespace QLNS.Provider.JWT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class JWTAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public Guid domainId { get; set; }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.User == null || filterContext.HttpContext.User.Identity?.IsAuthenticated == false)
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }

                string authHeader = filterContext.HttpContext.Request.Headers["Authorization"].ToString();
                domainId = Guid.Parse(filterContext.HttpContext.Request.Headers["X-Domain-Id"].ToString());

                if (string.IsNullOrEmpty(authHeader))
                {
                    ReturnUnauthorizedResult(filterContext);
                    return;
                }
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(authHeader.Replace("Bearer ", ""));
                var tokenS = jsonToken as JwtSecurityToken;
                var userToken = JsonConvert.DeserializeObject<UserLogonModel>(tokenS!.Claims.First(claim => claim.Type == "UserData").Value);

                var claimsIdentity = filterContext.HttpContext.User.Identity as ClaimsIdentity;
                var currentUser = JsonConvert.DeserializeObject<UserLogonModel>(claimsIdentity!.FindFirst("UserData")!.Value);

                //Kiểm tra mỗi lần restart server cần logout hết mọi người ra ngoài để join lại QUEUE
                if (currentUser == null)
                {
                    filterContext.Result = new ForbidResult();
                    return;
                }
                if (userToken!.Id != currentUser.Id)
                {
                    filterContext.Result = new ForbidResult();
                    return;
                }
                //if (!await IsAuthorized(filterContext, currentUser.UserName, currentUser.Password))
                //{
                //    ReturnUnauthorizedResult(filterContext);
                //    return;
                //}
            }
            catch (Exception)
            {
                ReturnUnauthorizedResult(filterContext);
                return;
            }
        }
        public async Task<bool> IsAuthorized(AuthorizationFilterContext context, string username, string password)
        {
            var _UserServices = context.HttpContext.RequestServices.GetRequiredService<ILoginServices>();
            var check = await _UserServices.ValidateAuthJwtAsync(username, password, domainId);
            return check.Item1;
        }

        private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
        {
            // Return 401 and a basic authentication challenge (causes browser to show login dialog)
            context.HttpContext.Response.StatusCode = 401;
            context.Result = new UnauthorizedResult();
        }
    }
}
