using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QLNS.Context;
using QLNS.Models;
using QLNS.Models.Auth;

namespace QLNS.Services
{
    public interface ITokenServices
    {
        bool VerifyAccessToken(string token);
        UserToken GetUserToken(User user);
        Task<UserToken> RefreshUserToken(string token);
        ClaimsPrincipal GetClaimsPrincipalByToken(string token);
        UserToken Logon(UserLogonModel user);
        Task<UserToken> UserTokenLogout(string token);
        UserLogonModel GetUserDataAsync(string token);
    }
    public class TokenServices : ITokenServices
    {
        private readonly DataContext _Context;
        private readonly TokenSettingModel _UserTokenSetting;
        private readonly ILogger<TokenServices> _logger;
        public TokenServices(DataContext Context, IOptions<TokenSettingModel> UserTokenSetting, ILogger<TokenServices> logger)
        {
            _logger = logger;
            _Context = Context;
            _UserTokenSetting = UserTokenSetting.Value;
        }

        public UserToken GetUserToken(User user)
        {
            return this.GenUserToken(user);
        }

        public UserToken Logon(UserLogonModel user)
        {
            return this.GenUserToken(user);
        }

        public UserToken Logout(UserLogonModel user)
        {
            return this.GenUserTokenLogout(user);
        }

        public async Task<UserToken> RefreshUserToken(string token)
        {
            try
            {
                var principal = this.GetClaimsPrincipalByToken(token);
                var userData = JsonConvert.DeserializeObject<UserLogonModel>(principal.FindFirst("UserData")?.Value!);
                if (principal != null && userData != null)
                {
                    var item = await _Context.Users.Where(x => x.Id == userData.Id)
                        .Select(s => new UserLogonModel()
                        {
                            Id = s.Id,
                            Email = s.Email,
                            FullName = s.FullName,
                            PhoneNumber = s.Phone,
                            UserName = s.UserName,
                            Password = s.PassWord,
                        }).FirstOrDefaultAsync();

                    return this.Logon(item!);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new UserToken();

        }

        public UserLogonModel GetUserDataAsync(string token)
        {
            try
            {
                var principal = this.GetClaimsPrincipalByToken(token);
                if (principal.FindFirst("UserData") != null)
                {
                    var userData = JsonConvert.DeserializeObject<UserLogonModel>(principal.FindFirst("UserData")?.Value!);
                    if (principal != null && userData != null)
                    {
                        return userData;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new UserLogonModel();

        }

        public async Task<UserToken> UserTokenLogout(string token)
        {
            try
            {
                var principal = this.GetClaimsPrincipalByToken(token);
                var userData = JsonConvert.DeserializeObject<UserLogonModel>(principal.FindFirst("UserData")?.Value!);
                if (principal != null && userData != null)
                {
                    var item = await _Context.Users.Where(x => x.Id == userData.Id)
                        .Select(s => new UserLogonModel()
                        {
                            Id = s.Id,
                            Email = s.Email,
                            FullName = s.FullName,
                            PhoneNumber = s.Phone,
                            UserName = s.UserName,
                            Password = s.PassWord,
                        }).FirstOrDefaultAsync();

                    return this.Logout(item!);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new UserToken();

        }

        public ClaimsPrincipal GetClaimsPrincipalByToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_UserTokenSetting.Secret!)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var claim = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                var outToken = securityToken;
                return claim;
            }
            catch (Exception)
            {
                return new ClaimsPrincipal();
            }
        }

        private UserToken GenUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_UserTokenSetting.Secret!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),

                Expires = DateTime.Now.AddHours(Convert.ToDouble(_UserTokenSetting.Expires)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserToken userToken = new()
            {
                Id = user.Id,
                Accesstoken = tokenHandler.WriteToken(token),
                ExpiredAt = tokenDescriptor.Expires,
                Username = user.UserName
            };
            return userToken;
        }

        private UserToken GenUserToken(UserLogonModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var userData = new UserDataModel()
            {
                Id = user.Id,
                ParentId = user.ParentId,
                DomainId = user.DomainId,
                CustomerId = user.CustomerId,
                Password = user.Password,
                Email = user.Email,
                FullName = user.FullName,
                Permissions = user.Permissions,
                Rules = user.Rules,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                ExpiredTime = user.ExpiredTime,
                Avatar = user.Avatar,
                DateLogin = DateTime.Now,
                IpAddress = user.IpAddress,
            };
            var myClaims = new List<Claim>()
                {
                    new Claim("UserData", JsonConvert.SerializeObject(userData)),
                };

            var token = GetJwtToken($"{user.Id!}_{user.UserName}", _UserTokenSetting.Secret!, "", "",
                TimeSpan.FromHours(_UserTokenSetting.Expires), myClaims.ToArray());


            UserToken userToken = new()
            {
                Id = user.Id,
                ParentId = user.ParentId,
                CustomerId = user.CustomerId,
                DomainId = user.DomainId,
                Password = user.Password,
                Accesstoken = tokenHandler.WriteToken(token),
                ExpiredAt = token.ValidTo,
                Username = user.UserName,
                Permissions = user.Permissions,
                Rules = user.Rules,
                ExpiredTime = user.ExpiredTime,
                Avatar = user.Avatar,
                FullName = user.FullName,
                IpAddress = user.IpAddress!
            };
            return userToken;
        }

        private UserToken GenUserTokenLogout(UserLogonModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var myClaims = new List<Claim>()
                {
                    new Claim("UserData", JsonConvert.SerializeObject(user)),
                };

            var token = GetJwtToken(user.UserName!, _UserTokenSetting.Secret!, "", "",
                TimeSpan.FromHours(-30), myClaims.ToArray());


            UserToken userToken = new()
            {
                Id = user.Id,
                Accesstoken = tokenHandler.WriteToken(token),
                ExpiredAt = token.ValidTo,
                Username = user.UserName,
                FullName = user.FullName
            };
            return userToken;
        }



        JwtSecurityToken GetJwtToken(string username, string signingKey, string issuer, string audience,
            TimeSpan expiration, Claim[] additionalClaims = null)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (additionalClaims is object)
            {
                var claimList = new List<Claim>(claims);
                claimList.AddRange(additionalClaims);
                claims = claimList.ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.Add(expiration),
                claims: claims,
                signingCredentials: creds
            );
        }

        public bool VerifyAccessToken(string accesstoken)
        {
            var principal = this.GetClaimsPrincipalByToken(accesstoken);
            return principal != null;
        }

    }
}
