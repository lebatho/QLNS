namespace QLNS.Models.Auth
{
    public class SSOToken
    {
        public Guid Id { get; set; }
        public Guid DomainId { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public string ConnectedId { get; set; }
        public bool IsActived { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
