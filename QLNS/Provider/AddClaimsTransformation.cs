using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace QLNS.Provider
{
    public class AddClaimsTransformation : IClaimsTransformation
    {
        public AddClaimsTransformation()
        {
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Clone current identity
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity!;

            // Support AD and local accounts
            var userData = principal.Claims.FirstOrDefault(c => c.Type == "UserData");
            if (userData == null)
            {
                return principal;
            }

            // Get user from database
            //var currentUser = JsonConvert.DeserializeObject<UserLogonModel>(userData.Value);
            //var user = await _userService.GetAccountAsync(currentUser.Id);
            //if (user == null)
            //{
            //    return principal;
            //}

            //// Add role claims to cloned identity
            //var claim = new Claim("ConnectData", JsonConvert.SerializeObject(user));
            //newIdentity.AddClaim(claim);
            await Task.CompletedTask;
            return clone;
        }
    }
}
