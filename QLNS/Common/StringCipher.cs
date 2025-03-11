using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;

namespace QLNS.Common
{
    public static class StringCipher
    {
        static string salt = "cloud@ZNSCRM##2024";
        internal const string Inputkey = "560A18CD-6346-4CF0-A2E8-671F9B6B9EA9";
        public static string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");

            var aesAlg = NewRijndaelManaged(salt);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static string DecryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");

            if (!IsBase64String(plainText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var aesAlg = NewRijndaelManaged(salt);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(plainText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }
            return text;
        }

        public static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        private static RijndaelManaged NewRijndaelManaged(string salt)
        {
            if (salt == null) throw new ArgumentNullException("salt");
            var saltBytes = Encoding.ASCII.GetBytes(string.Concat(salt, Inputkey));
            var key = new Rfc2898DeriveBytes(Inputkey, saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }

        public static byte[] GenerateAesKey(int bits)
        {
            using (RandomNumberGenerator rngCryptoServiceProvider = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[bits / 8];
                rngCryptoServiceProvider.GetBytes(key);
                return key;
            }
        }
    }

    public static class StringHelper
    {
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(([\+84|84|0|1]+(2|3|4|5|7|8|9|1[0|1|2|6|8|9]))+([0-9]{6,12}))$").Success;
        }
        public static bool IsNumber(string username)
        {
            return Regex.Match(username, @"^([0-9])$").Success;
        }
        public static bool IsUsername(string username)
        {
            return Regex.Match(username, @"^([a-zA-Z0-9]{5,30})$").Success;
        }
        public static bool IsDomain(string domain)
        {
            return Regex.Match(domain, @"^([a-zA-Z0-9]{4,15})$").Success;
        }

        public static bool IsEndpointId(string username)
        {
            return Regex.Match(username, @"^([0-9]{3,6})$").Success;
        }

        public static bool IsPortNumber(string username)
        {
            return Regex.Match(username, @"^([0-9]{4,5})$").Success;
        }
        public static bool IsIpAddress(string username)
        {
            return Regex.Match(username, @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$").Success;
        }

        public static bool IsGatewayId(string username)
        {
            return Regex.Match(username, @"^([0-9]{9,12})$").Success;
        }

        public static bool IsPassword(string password)
        {
            return Regex.Match(password, @"^((?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,30})$").Success;
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static string GetEmptySpaceString(this string str)
        {
            var hadSpace = convertToUnSign3(RemoveSign4VietnameseString(str));
            var splitSpace = hadSpace.Split(' ').ToList();

            return $"{string.Join("", splitSpace)}";
        }

        public static string GetReplaceQueueString(this string str)
        {
            var hadSpace = convertToUnSign3(RemoveSign4VietnameseString(str));
            var splitSpace = hadSpace.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));

            return $"{string.Join("_", splitSpace)}".ToLower();
        }

        public static string Json2String(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        public static string GetPrefixConnectNetworkProvider(int prefix)
        {
            string content = string.Empty;
            switch (prefix)
            {
                case 1:
                    content = "Định dạng From không có 0";
                    break;
                case 2:
                    content = "Định dạng From 84, VD: 84x";
                    break;
                default:
                    content = "Định dạng cơ bản (From có 0, 0x)";
                    break;
            }
            return content;
        }

        public static string GetRamdomCode(int number)
        {
            var code = string.Empty;
            Random random = new();
            for (int i = 0; i < number; i++)
            {
                //code += Convert.ToString(random.Next(1, 20), "000000");
                code += random.Next(1, 99).ToString("00");
            }
            return code;
        }
    }
}
