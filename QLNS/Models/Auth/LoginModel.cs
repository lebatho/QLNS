namespace QLNS.Models.Auth
{
    public class LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? IpAddress { get; set; }
        public string? ConnectedId { get; set; }
    }
}
