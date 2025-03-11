using System.Net;

namespace QLNS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
    }
}
