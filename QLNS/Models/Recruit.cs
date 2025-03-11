using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class Recruit
    {
        public Guid Id { get; set; }
        [Column("benefits_received")] 
        public string? BenefitsReceived { get; set; }
        [Column("changed_by")]
        public string? ChangedBy { get; set; }
        public string? Code { get; set; }
        public string? Creator { get; set; }
        [Column("date_change")]
        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        public string? Description { get; set; }
        public string? Feedback { get; set; }
        public int? Quantity { get; set; }
        [Column("recruitment_channel")]
        public string? RecruitmentChannel { get; set; }
        [Column("require_recruit")]
        public string? RequireRecruit { get; set; } 
        public int? Status { get; set; }
        [Column("title_recruit")]
        public string? TitleRecruit { get; set; }
    }
}