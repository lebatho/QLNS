using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class Salary
    {
        public Guid Id { get; set; }
        [Column("changed_by")] 
        public string? ChangedBy { get; set; }
        public string? Code { get; set; }
        [Column("coefficient_salary")]
        public double? CoefficientSalary { get; set; } 
        public string? Creator { get; set; }
        [Column("date_change")]
        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        [Column("hourly_rate")]
        public long? HourlyRate { get; set; }
        [Column("salary_level")]
        public long? SalaryLevel { get; set; }  
    }
}