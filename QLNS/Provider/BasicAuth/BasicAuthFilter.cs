using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using QLNS.Services;

namespace QLNS.Provider.BasicAuth
{
    public class BasicAuthFilter : IAuthorizationFilter
    {
        private readonly string _realm;
        public BasicAuthFilter(string realm)
        {
            _realm = realm;
            if (string.IsNullOrWhiteSpace(_realm))
            {
                throw new ArgumentNullException(nameof(realm), @"Please provide a non-empty realm value.");
            }
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string authHeader = context.HttpContext.Request.Headers["Authorization"]!;
                string domainId = context.HttpContext.Request.Headers["X-Domain-Id"].ToString();
                if (authHeader != null)
                {
                    var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
                    if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        var credentials = Encoding.UTF8
                                            .GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
                                            .Split(':', 2);
                        if (credentials.Length == 2)
                        {
                            if (await IsAuthorized(context, credentials[0], credentials[1], domainId))
                            {
                                return;
                            }
                        }
                    }
                }

                ReturnUnauthorizedResult(context);
            }
            catch (FormatException)
            {
                ReturnUnauthorizedResult(context);
            }
        }

        public async Task<bool> IsAuthorized(AuthorizationFilterContext context, string username, string password, string domainId)
        {
            //var _UserServices = context.HttpContext.RequestServices.GetRequiredService<ILoginServices>();
            //var check = await _UserServices.ValidateLoginAsync(username, password, Guid.Parse(domainId));
            //return string.IsNullOrEmpty(check.Item3);
            return false;
        }

        private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
        {
            // Return 401 and a basic authentication challenge (causes browser to show login dialog)
            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{_realm}\"";
            context.Result = new UnauthorizedResult();
        }
    }
}
