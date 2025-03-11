using Microsoft.EntityFrameworkCore;
using QLNS.Common;
using QLNS.Context;
using QLNS.Models.Auth;
using QLNS.Services;

namespace QLNS.Services
{
    public interface ILoginServices
    {
        Task<UserToken> GetLogoutUserAsync(string token);

        /// <summary>
        /// Kiểm tra authorization
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Task<(bool, int, string)> ValidateAuthJwtAsync(string userName, string passWord, Guid domainId);

        /// <summary>
        /// Kiểm tra trước khi lấy thông tin login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Task<(bool, int, string)> ValidateLoginAsync(string userName, string passWord, Guid domainId);

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserToken> GetLogonUserAsync(LoginModel model, Guid domainId, string userAgent);
        Task<(bool, UserLogonModel?)> GetUserLogout(string token);
    }
    public class LoginServices : ILoginServices
    {
        private readonly ILogger<LoginServices> _logger;
        private readonly DataContext _Context;
        private readonly ITokenServices _TokenServices;
        public LoginServices(ILogger<LoginServices> logger, DataContext context, ITokenServices tokenServices)
        {
            _logger = logger;
            _Context = context;
            _TokenServices = tokenServices;
        }

        /// <summary>
        /// Kiểm tra Auth JWT
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<(bool, int, string)> ValidateAuthJwtAsync(string userName, string passWord, Guid domainId)
        {
            var item = await _Context.Users.Where(x => x.UserName == userName ).FirstOrDefaultAsync();
            if (item == null) return (false, 587, "Có lỗi xảy ra, Kiểm tra lại thông tin!");

            if (item.PassWord != passWord) return (false, 587, "Sai mật khẩu tài khoản");

            //if (item.IsDeleted) return (false, 587, "Tài khoản không tồn tại");

            //if (!item.IsActived) return (false, 587, "Tài khoản đã bị khóa hoặc chưa được kích hoạt");

            return (true, 0, string.Empty);
        }

        /// <summary>
        /// Kiểm tra trước khi lấy thông tin login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<(bool, int, string)> ValidateLoginAsync(string userName, string passWord, Guid domainId)
        {
            var item = await _Context.Users.Where(x => x.UserName == userName ).FirstOrDefaultAsync();
            if (item == null) return (false, 587, "Có lỗi xảy ra, Kiểm tra lại thông tin!");

            //var pass = GeneratorPassword.EncodePassword(passWord, item.PasswordSalt!);
            //if (item.Password != pass) return (false, 587, "Sai mật khẩu tài khoản");

            //if (item.IsDeleted) return (false, 587, "Tài khoản không tồn tại");

            //if (!item.IsActived) return (false, 587, "Tài khoản đã bị khóa hoặc chưa được kích hoạt");

            return (true, 0, string.Empty);
        }

        public async Task<UserToken> GetLogonUserAsync(LoginModel model, Guid domainId, string userAgent)
        {
            var item = await _Context.Users.Where(u => u.UserName == model.UserName ).
                    Select(s => new UserLogonModel()
                    {
                        Id = s.Id,
                        CustomerId = Guid.Empty,
                        Email = s.Email,
                        FullName = s.FullName,
                        Password = s.PassWord,
                        PhoneNumber = s.Phone,
                        UserName = s.UserName!,
                        Avatar = s.Avatar,
                        Permissions = new List<Guid>(),
                        Rules = new List<string>(),
                        IpAddress = model.IpAddress!,
                    }).FirstOrDefaultAsync();
            if (item != null)
            {
                //item.Permissions = await _Context.UserPermissions.Where(x => x.UserId.Equals(item.Id)).Select(s => s.PermissionId).ToListAsync();
                //item.Rules = await _Context.UserPermissions.Where(x => x.UserId.Equals(item.Id)).Select(s => s.Permission!.PermissionCode!).ToListAsync();
                //if (!item.IsMember) {
                //    var customer = await _Context.Customers.Where(x => x.UsersId.Equals(item.Id)).FirstOrDefaultAsync();
                //    if (customer != null)
                //    {
                //        item.CustomerId = customer.Id;
                //    }
                //}
            }
            var token = _TokenServices.Logon(item!);

            return token;
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserToken> GetLogoutUserAsync(string token)
        {
            return await _TokenServices.UserTokenLogout(token);
        }

        public Task<(bool, UserLogonModel?)> GetUserLogout(string token)
        {
            try
            {
                var item = _TokenServices.GetUserDataAsync(token);
                if (item != null)
                {
                    return Task.FromResult((true, item))!;
                }
                return Task.FromResult((false, item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Task.FromResult((false, new UserLogonModel()))!;
            }
        }

    }
}
