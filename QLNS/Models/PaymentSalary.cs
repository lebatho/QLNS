namespace QLNS.Models
{
    public class PaymentSalary
    {
        public int Id { get; set; }
        public decimal AdvancePayment { get; set; }
        public string ChangedBy { get; set; }
        public string Creator { get; set; }
        public DateTime DateChange { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal InsuranceDeductible { get; set; }
        public int Month { get; set; }
        public decimal NetWage { get; set; }
        public string Status { get; set; }
        public int Year { get; set; }
        public string TimeKeepingCode { get; set; }
        public decimal FullTimeSalary { get; set; }
        public decimal HealthInsurancePremium { get; set; }
        public decimal PersonalIncomeTax { get; set; }
        public decimal SocialInsuranceCosts { get; set; }
        public decimal TransportationAndLunchAllowance { get; set; }
        public decimal ValueAddedWithEachSalary { get; set; }
    }
}
