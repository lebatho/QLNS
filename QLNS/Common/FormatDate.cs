using System.Globalization;

namespace QLNS.Common
{
    public static class FormatDate
    {
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy
        /// </summary>
        public const string DateTime_103 = "dd/MM/yyyy";
        /// <summary>
        /// Định dạng thời gian MM/dd/yyyy
        /// </summary>
        public const string DateTime_101 = "MM/dd/yyyy";
        /// <summary>
        /// Định dạng thời gian dd-MM-yyyy HH:mm:ss
        /// </summary>
        public const string DateTime_120 = "HH:mm:ss";
        /// <summary>
        /// Định dạng thời gian dd-MM-yyyy HH:mm:ss
        /// </summary>
        public const string DateTime_121 = "dd-MM-yyyy HH:mm:ss";
        /// <summary>
        /// Định dạng thời gian yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string DateTime_yyyyMMddHHmmss_MySQL = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy HH:mm
        /// </summary>
        public const string DateTime_ddMMyyyyHHmm = "dd/MM/yyyy HH:mm";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy HH:mm:ss
        /// </summary>
        public const string DateTime_ddMMyyyyHHmmss = "dd/MM/yyyy HH:mm:ss";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy 00:00
        /// </summary>
        public const string DateTime_ddMMyyyyHHmm_FirstTime = "dd/MM/yyyy 00:00";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy 23:59
        /// </summary>
        public const string DateTime_ddMMyyyyHHmm_LastTime = "dd/MM/yyyy 23:59";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy 23:59
        /// </summary>
        public const string DateTime_ddMMyyyyHHmmss_LastTime = "dd MMM yyyy HH:mm:ss";

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static string GetPartitionFromDate(DateTime fromDate)
        {
            string partition = "p_Jan";
            switch (fromDate.Month)
            {
                case 1:
                    partition = "p_Jan";
                    break;
                case 2:
                    partition = "p_Feb";
                    break;
                case 3:
                    partition = "p_Apr";
                    break;
                case 4:
                    partition = "p_May";
                    break;
                case 5:
                    partition = "p_Jun";
                    break;
                case 6:
                    partition = "p_Jul";
                    break;
                case 7:
                    partition = "p_Aug";
                    break;
                case 8:
                    partition = "p_Sep";
                    break;
                case 9:
                    partition = "p_Oct";
                    break;
                case 10:
                    partition = "p_Nov";
                    break;
                case 11:
                    partition = "p_Dec";
                    break;
                case 12:
                    partition = "p_Jan";
                    break;
            }
            return partition;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string s,
                  string format = "dd-MM-yyyy", string cultureString = "tr-TR")
        {
            try
            {
                var r = DateTime.ParseExact(
                    s: s,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));
                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; // Given Culture is not supported culture
            }
        }

        public static DateTime ToDateTime(this string s,
                    string format, CultureInfo culture)
        {
            try
            {
                var r = DateTime.ParseExact(s: s, format: format,
                                        provider: culture);
                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; // Given Culture is not supported culture
            }

        }

    }
}
