namespace QLNS.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string EmployeeCode { get; set; }
    }
}
