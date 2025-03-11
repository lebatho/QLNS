using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class CandidateProfile
    {

        public Guid Id { get; set; }

        public string? Address { get; set; }

        public int? Age { get; set; }
        [Column("changed_by")]
        public string? ChangedBy { get; set; }

        public string? Code { get; set; }

        public string? Creator { get; set; }
        [Column("date_change")]
        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        public string? Education { get; set; }

        public string? Email { get; set; }
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("interview_date")]
        public DateTime? InterviewDate { get; set; }

        public string? Interviewer { get; set; }

        public string? Major { get; set; }

        public string? Phone { get; set; }
        [Column("refusal_reason")]
        public string? RefusalReason { get; set; }

        public int? status { get; set; }

        public string? Image { get; set; }
        [Column("imageimage_name")]
        public string? ImageName { get; set; }
        [Column("career_goals")]
        public string? CareerGoals { get; set; }

        public string? Hobby { get; set; }

        public string? Skill { get; set; }
        [Column("working_experience")]
        public string? WorkingExperience { get; set; }
        [Column("title_recruit")]
        public string?   TitleRecruit { get; set; }
    }
}
