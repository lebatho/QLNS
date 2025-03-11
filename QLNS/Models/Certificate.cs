using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class Certificate
    {
        public Guid Id { get; set; }
        [Column("changed_by")]
        public string? ChangedBy { get; set; }
        public string? Code { get; set; }
        public string? Creator { get; set; }
        [Column("date_change")]
        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        [Column("granted_by")]
        public string? GrantedBy { get; set; }
        [Column("issued_date")]
        public DateTime? IssuedDate { get; set; }
        public string? Majors { get; set; }
        public string? Name { get; set; }
    }
}
