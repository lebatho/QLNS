using System.Security.Cryptography;
using System.Text;
using QLNS.Common;

namespace QLNS.Common
{
    public static class GeneratorPassword
    {
        internal const int SALT_SIZE = 16;
        static string s_HashAlgorithm = string.Empty;

        private static Random random = new Random();
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
        public static string GetNameAndCode(string name, int number)
        {
            var arrayName = StringHelper.RemoveSign4VietnameseString(name).Split(' ').ToList();
            var codeName = string.Empty;
            if (arrayName.Count > 0)
            {
                arrayName.ForEach(r =>
                {
                    codeName += r.First();
                });
            }

            var code = string.Empty;
            Random random = new();
            for (int i = 0; i < number; i++)
            {
                //code += Convert.ToString(random.Next(1, 20), "000000");
                code += random.Next(1, 99).ToString("00");
            }
            return string.Format("{0}-{1}", codeName, code);
        }
        public static string GetFullNameAndCode(string name, int number)
        {
            var arrayName = StringHelper.GetEmptySpaceString(name);
            var codeName = arrayName.Substring(0, 3);

            var code = string.Empty;
            Random random = new();
            for (int i = 0; i < number; i++)
            {
                //code += Convert.ToString(random.Next(1, 20), "000000");
                code += random.Next(1, 99).ToString("00");
            }
            return string.Format("{0}-{1}", codeName, code);
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghjklmnopiuyrtewqasfghjzxcvb@#$!&()ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string EncodePassword(string pass, string salt)
        {
            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);

            HashAlgorithm hm = GetHashAlgorithm()!;
            byte[] bRet;
            if (hm is KeyedHashAlgorithm)
            {
                KeyedHashAlgorithm kha = (KeyedHashAlgorithm)hm;
                if (kha.Key.Length == bSalt.Length)
                {
                    kha.Key = bSalt;
                }
                else if (kha.Key.Length < bSalt.Length)
                {
                    byte[] bKey = new byte[kha.Key.Length];
                    Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                    kha.Key = bKey;
                }
                else
                {
                    byte[] bKey = new byte[kha.Key.Length];
                    for (int iter = 0; iter < bKey.Length;)
                    {
                        int len = Math.Min(bSalt.Length, bKey.Length - iter);
                        Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                        iter += len;
                    }
                    kha.Key = bKey;
                }
                bRet = kha.ComputeHash(bIn);
            }
            else
            {
                byte[] bAll = new byte[bSalt.Length + bIn.Length];
                Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
                Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
                bRet = hm!.ComputeHash(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        static HashAlgorithm? GetHashAlgorithm()
        {
            if (!string.IsNullOrEmpty(s_HashAlgorithm))
                return HashAlgorithm.Create(s_HashAlgorithm)!;

            string temp = "SHA1";
            if (temp != "MD5")
            {
                temp = "SHA1";
            }
            HashAlgorithm hashAlgo = HashAlgorithm.Create(temp)!;
            if (hashAlgo == null)
                return null;
            s_HashAlgorithm = temp;
            return hashAlgo;
        }

        public static string GenerateSalt()
        {
            byte[] buf = new byte[SALT_SIZE];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
    }
}
