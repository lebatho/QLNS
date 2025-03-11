namespace QLNS.Models.Auth
{
    public class UserDataModel
    {
        public int Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? DomainId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? IpAddress { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public DateTime? DateLogin { get; set; }
        public List<Guid>? Permissions { get; set; }
        public List<string>? Rules { get; set; }
    }
}
