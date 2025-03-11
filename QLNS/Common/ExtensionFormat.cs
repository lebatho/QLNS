namespace QLNS.Common
{
    public static class ExtensionFormat
    {
        public static List<string> Vina = new List<string> { "082", "081", "085", "084", "083", "088", "094", "091" };
        public static List<string> Viettel = new List<string> { "032", "033", "034", "035", "036", "037", "038", "039", "098", "097", "096", "086" };
        public static List<string> Mobi = new List<string> { "093", "090", "089", "078", "076", "077", "079", "070" };
        public static List<string> Vietnamobile = new List<string> { "092", "052", "056", "058" };
        public static List<string> Gmobile = new List<string> { "099", "059" };
        public static List<string> Itelecom = new List<string> { "087" };
        public static List<string> CDVNPT = new List<string> { "0243", "0248" };


        public static List<string> CDNUMBER = new List<string> { "024", "028", "020", "022" };
        public static List<string> CDCMC = new List<string> { "02871", "02471", "02037" };
        public static List<string> CDGtel = new List<string> { "02499" };
        public static List<string> CDDigitel = new List<string> { "02488" };
        public static List<string> CDSPT = new List<string> { "02854" };
        public static List<string> CDItel777 = new List<string> { "02477" };

        public static string PhoneNumberLength(string phoneNumber) => !string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length < 10 ? $"0{phoneNumber}" : phoneNumber;
    }
}
