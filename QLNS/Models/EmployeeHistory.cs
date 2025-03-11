namespace QLNS.Models
{
    public class EmployeeHistory
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public DateTime EndDate { get; set; }
        public string? Event { get; set; }  
        public string? Status { get; set; }
        public string? EmployeeCode { get; set; }
    }
}
