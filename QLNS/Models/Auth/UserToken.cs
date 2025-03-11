namespace QLNS.Models.Auth
{
    public class UserToken
    {
        public int Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? DomainId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? Accesstoken { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? Avatar { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? IpAddress { get; set; }
        public List<Guid>? Permissions { get; set; }
        public List<string>? Rules { get; set; }
    }
}
