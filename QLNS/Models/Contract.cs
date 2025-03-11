using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class Contract
    {
        public Guid Id { get; set; }
        [Column("changed_by")]
        public string? ChangedBy { get; set; }

        public string? Code { get; set; }
        [Column("contract_effect")]
        public DateTime? ContractEffect { get; set; }

        public string? Creator { get; set; }
        [Column("date_change")]
        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        [Column("signing_date")]
        public DateTime? SigningDate { get; set; }

        public int? Status { get; set; }
        [Column("basic_salary")]
        public long? BasicSalary { get; set; }
        [Column("name_leader")]
        public string? NameLeader { get; set; }
        [Column("position_leader")]
        public string? PositionLeader { get; set; }
        [Column("coefficient_salary")]
        public double? CoefficientSalary { get; set; }
        [Column("hourly_rate")]
        public long? HourlyRate { get; set; }
    }
}
