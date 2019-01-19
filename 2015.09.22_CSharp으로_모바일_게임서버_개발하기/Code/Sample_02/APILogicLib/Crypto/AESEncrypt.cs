using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;


namespace APILogicLib
{
    // 출처: http://programmers.high-way.info/cs/aes.html
    // AES 128비트 암호화. 한글 지원
    


    public class AESEncrypt
    {
        // 128bit(16byte)의 IV（초기화 벡터）와 Key(암호키）
        //static private string AesIV = @"!QAZ2WSX#EDC4RFV";
        //static private string AesKey = @"5TGB&YHN7UJM(IK<";
        static private string AesIV = @"!QAZ2WSX#EDC4RFV";  //16
        static private string AesKey = @"5TGB&7U(IK<";      // 11
        static public string AesLoginDynamicKey = @"d_&^t";      // 5

        /// <summary>
        /// 문자열을 AES로 암호화 한다
        /// </summary>
        public static string Encrypt(string dynamincKey, string text)
        {
            var comleteAesKey = dynamincKey.Substring(0, 5) + AesKey;

            System.Security.Cryptography.AesCryptoServiceProvider aes = new System.Security.Cryptography.AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(comleteAesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // 문자열을 바이트형 배열로 변환
            byte[] src = Encoding.Unicode.GetBytes(text);

            // 암호화 한다
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                // 바이트형 배열에서 Base64형식의 문자열로 변환
                return Convert.ToBase64String(dest);
            }
        }

        /// <summary>
        /// 문자열을 AES로 복호화 한다
        /// </summary>
        public static string Decrypt(string dynamincKey, string text)
        {
            var comleteAesKey = dynamincKey.Substring(0, 5) + AesKey;

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(comleteAesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Base64 형식의  문자열에서 바이트형 배열로 변환
            byte[] src = System.Convert.FromBase64String(text);

            // 복호화 한다
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }

        
    }
}
