
namespace QLNS.Common
{
    public static class ConstantsValue
    {
        public static int GetRandomNumber(int Min, int Max, List<int>? ExcludedNumbers)
        {
            Random randomGenerator = new Random();
            int currentNumber = randomGenerator.Next(Min, Max + 1);

            if (ExcludedNumbers != null && ExcludedNumbers.Count > 0)
            {
                while (ExcludedNumbers.Contains(currentNumber))
                {
                    currentNumber = randomGenerator.Next(Min + 1, Max + 1);
                }
            }

            return currentNumber;
        }

        #region Permissions
        public const string PerSettings = "settings";
        public const string PerSettingGeneral = "settings-general";
        public const string PerSettingPackage = "settings-package";
        public const string PerSettingHotline = "settings-hotline";
        public const string PerMembers = "members";
        public const string PerEmails = "emails";
        public const string PerCustomers = "customers";
        public const string PerCustomerWarning = "customer-warning";
        public const string PerOA = "config-oa";
        public const string PerTemplate = "template-oa";
        public const string PerWarehouse = "warehouse";
        public const string PerTemplateOtp = "template-otp";
        public const string PerWarehouseReuse = "warehouse-reuse";
        public const string PerWarehouseCancel = "warehouse-cancel";
        public const string PerAgentcyCustomers = "agentcy-customer";


        public const string PerAccounts = "accounts";
        public const string PerPermissions = "permissions";
        public const string PerReport = "report";
        public const string PerEndpoints = "endpoints";
        public const string PerExtensions = "extensions";
        public const string PerQueues = "queues";
        public const string PerConnection = "connection";
        public const string PerPBX = "pbx";
        public const string PerRecord = "records";
        public const string PerWorkedTime = "worktime";
        #endregion

        public static List<string> StatusSuccess = new List<string> { "5", "0", "7" };

        public static List<Status> StatusTable = new List<Status>
        {
            new Status { StatusCode = "0", Message = "Gửi thành công" },
            new Status { StatusCode = "5", Message = "Gửi thành công" },
            new Status { StatusCode = "4", Message = "Từ chối gửi tin" },
            new Status { StatusCode = "7", Message = "Đã gửi tin chờ báo cáo" },
            new Status { StatusCode = "2", Message = "Chờ gửi tin" },
            new Status { StatusCode = "1", Message = "Chờ duyệt" },
            new Status { StatusCode = "-100", Message = "Xảy ra lỗi không xác định, vui lòng thử lại sau" },
            new Status { StatusCode = "-101", Message = "Ứng dụng gửi ZNS không hợp lệ" },
            new Status { StatusCode = "-102", Message = "Ứng dụng gửi ZNS không tồn tại" },
            new Status { StatusCode = "-103", Message = "Ứng dụng chưa được kích hoạt" },
            new Status { StatusCode = "-105", Message = "Ứng dụng gửi ZNS chưa đươc liên kết với OA nào" },
            new Status { StatusCode = "-106", Message = "Phương thức không được hỗ trợ" },
            new Status { StatusCode = "-108", Message = "Số điện thoại không hợp lệ" },
            new Status { StatusCode = "-109", Message = "ID mẫu ZNS không hợp lệ" },
            new Status { StatusCode = "-1091", Message = "Template không có trạng thái Reject hoặc Template được tạo từ Admin tool" },
            new Status { StatusCode = "-110", Message = "Phiên bản Zalo app không được hỗ trợ. Người dùng cần cập nhật phiên bản mới nhất" },
            new Status { StatusCode = "-111", Message = "Mẫu ZNS không có dữ liệu" },
            new Status { StatusCode = "-112", Message = "Dữ liệu mẫu ZNS không hợp lệ" },
            new Status { StatusCode = "-1121", Message = "Dữ liệu tham số vượt quá giới hạn ký tự" },
            new Status { StatusCode = "-1122", Message = "Dữ liệu mẫu ZNS thiếu tham số" },
            new Status { StatusCode = "-1124", Message = "Dữ liệu tham số không đúng format" },
            new Status { StatusCode = "-113", Message = "Button không hợp lệ" },
            new Status { StatusCode = "-114", Message = "Người dùng không nhận được ZNS vì các lý do: Trạng thái tài khoản, Tùy chọn nhận ZNS, Sử dụng Zalo phiên bản cũ, hoặc các lỗi nội bộ khác" },
            new Status { StatusCode = "-116", Message = "Nội dung không hợp lệ" },
            new Status { StatusCode = "-117", Message = "OA hoặc ứng dụng gửi ZNS chưa được cấp quyền sử dụng mẫu ZNS này" },
            new Status { StatusCode = "-118", Message = "Tài khoản Zalo không tồn tại hoặc đã bị vô hiệu hoá" },
            new Status { StatusCode = "-119", Message = "Tài khoản không thể nhận ZNS" },
            new Status { StatusCode = "-120", Message = "OA chưa được cấp quyền sử dụng tính năng này" },
            new Status { StatusCode = "-121", Message = "Mẫu ZNS không có nội dung" },
            new Status { StatusCode = "-122", Message = "Body request không đúng định dạng JSON" },
            new Status { StatusCode = "-124", Message = "Mã truy cập không hợp lệ" },
            new Status { StatusCode = "-125", Message = "ID Official Account không hợp lệ" },
            new Status { StatusCode = "-130", Message = "Nội dung mẫu ZNS vượt quá giới hạn kí tự" },
            new Status { StatusCode = "-131", Message = "Mẫu ZNS chưa được phê duyệt" },
            new Status { StatusCode = "-132", Message = "Tham số không hợp lệ" },
            new Status { StatusCode = "-133", Message = "Mẫu ZNS này không được phép gửi vào ban đêm (từ 22h-6h)" },
            new Status { StatusCode = "-134", Message = "Người dùng chưa phản hồi gợi ý nhận ZNS từ OA" },
            new Status { StatusCode = "-135", Message = "OA chưa có quyền gửi ZNS (chưa được xác thực, đang sử dụng gói miễn phí)" },
            new Status { StatusCode = "-1351", Message = "OA không có quyền gửi ZNS (Hệ thống chặn do phát hiện vi phạm)" },
            new Status { StatusCode = "-138", Message = "Ứng dụng gửi ZNS chưa có quyền sử dụng tính năng này" },
            new Status { StatusCode = "-139", Message = "Người dùng từ chối nhận loại ZNS này" },
            new Status { StatusCode = "-141", Message = "Người dùng từ chối nhận ZNS từ Official Account" },
            new Status { StatusCode = "-144", Message = "OA đã vượt giới hạn gửi ZNS trong ngày" },
            new Status { StatusCode = "-1441", Message = "OA request gửi vượt ngưỡng monthly promotion quota" },
            new Status { StatusCode = "-145", Message = "OA không được phép gửi loại nội dung ZNS này" },
            new Status { StatusCode = "-146", Message = "Mẫu ZNS này đã bị vô hiệu hoá do chất lượng gửi thấp" },
            new Status { StatusCode = "-147", Message = "Mẫu ZNS đã vượt giới hạn gửi trong ngày" },
            new Status { StatusCode = "-153", Message = "Dữ liệu truyền vào sai quy định" }
        };
        public static List<TemplateType> TemplateTypeTable = new List<TemplateType>
        {
            new TemplateType { Type = "4", TypeName = "Tin nhắn OTP" },
            new TemplateType { Type = "25", TypeName = "Tin nhắn thường" }
        };

        public static string getTextNumber(string code)
        {
            switch (code)
            {
                case "0":
                    return "khong";
                case "1":
                    return "mot";
                case "2":
                    return "hai";
                case "3":
                    return "ba";
                case "4":
                    return "bon";
                case "5":
                    return "nam";
                case "6":
                    return "sau"; 
                case "7":
                    return "bay";
                case "8":
                    return "tam";
                case "9":
                    return "chin";
                default:
                    return code;
            }
        }
    }

    public class Status
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class TemplateType
    {
        public string Type { get; set; }
        public string TypeName { get; set; }
    }
    
    public class TemplateOTP
    {
        public string templateId { get; set; }
        public string otp { get; set; }
    }

    /// <summary>
    /// trạng thái cuộc gọi
    /// </summary>
    public static class CallStatus
    {
        public const string TraLoi = "ANSWERED";
        public const string KhongTraLoi = "0";
        public const string Ban = "2";
        public const string Pending = "3";
    }
}
