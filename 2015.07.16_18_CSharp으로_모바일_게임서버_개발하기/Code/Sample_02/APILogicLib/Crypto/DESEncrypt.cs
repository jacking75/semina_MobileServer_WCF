using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;


namespace APILogicLib
{
    // 출처: http://d.hatena.ne.jp/undiscokidd/20080702/p1

    // 사용예
    //string JSonData = "fdfdfd";

    //byte[] secretKey = Encoding.UTF8.GetBytes("secretky");
    //string EncryptString = DESEncrypt.Encrypt(JSonData, secretKey);

    //string DeEncryptString = DESEncrypt.Decrypt(EncryptString, secretKey);



    class DESEncrypt
    {
        // 암호화
        static public string Encrypt(string str, byte[] Key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            des.Key = Key;
            des.IV = Key;

            byte[] bstr = Encoding.UTF8.GetBytes(str);

            ICryptoTransform transform = des.CreateEncryptor();
            byte[] encrypted = transform.TransformFinalBlock(bstr, 0, bstr.Length);

            transform.Dispose();

            return Convert.ToBase64String(encrypted);
        }

        // 복호화
        static public string Decrypt(string str, byte[] Key)
        {
            byte[] bcrypt = System.Convert.FromBase64String(str);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            des.Key = Key;
            des.IV = Key;

            ICryptoTransform destransform = des.CreateDecryptor();
            byte[] decrypted = destransform.TransformFinalBlock(bcrypt, 0, bcrypt.Length);

            destransform.Dispose();
            return Encoding.UTF8.GetString(decrypted);
        }
    }



}
