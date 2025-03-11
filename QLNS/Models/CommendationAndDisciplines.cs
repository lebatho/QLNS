using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class CommendationAndDisciplines
    {
        public Guid Id { get; set; }
        [Column("changed_by")]
        public string? ChangedBy { get; set; }
        public string? Creator { get; set; }
        [Column("date_change")]
        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        [Column("issued_date")]
        public DateTime? IssuedDate { get; set; }
        public string? Reason { get; set; }
        public int Status { get; set; }
        [Column("_day")]
        public int Day { get; set; }
        [Column("decision_day")]
        public DateTime? DecisionDay { get; set; }
        [Column("decision_number")]
        public string? DecisionNumber { get; set; }
        [Column("_month")]
        public int Month { get; set; }
        [Column("reward_discipline_level")]
        public double? RewardDisciplineLevel { get; set; }
        [Column("staff_name")]
        public string? StaffName { get; set; }
        [Column("_year")]
        public int Year { get; set; }
        public int Type { get; set; }
        [Column("employee_code")]
        public string? EmployeeCode { get; set; }
        [Column("basic_salary")]
        public int BasicSalary { get; set; }
        [Column("coefficient_salary")]
        public float CoefficientSalary { get; set; }
        [Column("hourly_rate")]
        public int HourlyRate { get; set; }
    }
}
