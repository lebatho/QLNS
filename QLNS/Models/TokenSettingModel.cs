namespace QLNS.Models
{
    public class TokenSettingModel
    {
        public int Expires { get; set; }
        public string? Secret { get; set; }
        public string? DefaultPassword { get; set; }
        public int OTPRecoverPassword { get; set; }
    }
}
