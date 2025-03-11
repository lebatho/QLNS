namespace QLNS.Models.Auth
{
    public class UserLogonModel
    {
        public int Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid DomainId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Socket { get; set; }
        public string? Domain { get; set; }
        public string? IpAddress { get; set; }
        public bool IsMember { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public List<Guid>? Permissions { get; set; }
        public List<string>? Rules { get; set; }
    }
}
