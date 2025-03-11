using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class TimeKeeping
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
        [Column("_month")]
        public int Month { get; set; }
        [Column("number_day_off")]
        public int NumberDayOff { get; set; }
        [Column("number_day_unexcused_leave")]
        public int NumberDayUnexcusedLeave { get; set; }
        [Column("number_overtime_hours")]
        public int NumberOvertimeHours { get; set; }
        [Column("number_work_day")]
        public int NumberWorkDay { get; set; }
        [Column("_year")]
        public int? Year { get; set; }
        [Column("employee_code")]
        public string? EmployeeCode { get; set; }
        public int Status { get; set; }
    }
}
