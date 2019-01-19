using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    // 출처: http://w.livedoor.jp/jun_c/d/xor%A4%C7%B0%C5%B9%E6%B2%BD

    // 사용예
    //string JSonData = "fdfdfd";

    //string Key = "jacking751975";
    //string EncryptString = XorEncrypt.EncryptionASCII(JSonData, Key);

    //string DeEncryptString = XorEncrypt.DecryptionToASCII(EncryptString, Key);



    // 일단 영문자만 사용 가능 ㅠㅠ.. 왠지 바이트를 문자열로 바꿀 때 Base64를 사용하면 한글도 사용할 수 있을 듯...
    class XorEncrypt
    {
        /// <summary>
        /// 암호화 한다
        /// </summary>
        /// <param name="byt">암호화 할 문자열</param>
        /// <param name="kye">key</param>
        /// <returns>암호화된 문자열</returns>
        static public string EncryptionASCII(string byt, string kye)
        {
            // 문자열을 byte로 변환 
            byte[] _byt = Encoding.ASCII.GetBytes(byt);
            byte[] _key = Encoding.ASCII.GetBytes(kye);

            int j = 0; 
            string str = "";
            
            for (int i = 0; i < _byt.Length; i++)
            {
                //_key가 _byt 보다 요소 수가 적을 때를 위해서より要素数が少ないときのため
                if (j < _key.Length)
                {
                    j++;
                }
                else
                {
                    j = 1;
                }

                //xor로 암호화 
                _byt[i] = (byte)(_byt[i] ^ _key[j - 1]);
                
                str += string.Format("{0:000}", _byt[i]);
            }
            return str;
        }

        
        /// <summary>
        /// 복호화
        /// </summary>
        /// <param name="byt">복호화 할 문자열</param>
        /// <param name="key">Key</param>
        /// <returns>복호화된 문자열</returns>
        static public string DecryptionToASCII(string byt, string key)
        {
            // 문자열을 byte로 변환
            byte[] _key = Encoding.ASCII.GetBytes(key);

            //3개로 구분되어진 문자열의 문자 수의 1/3 byte 배열을 준비한다
            byte[] _byt = new byte[byt.Length / 3];
            //문자 수의 1/3 반복한다
            for (int a = 0; a < byt.Length / 3; a++)
            {
                //3개씩 byte 변환해서 저장한다
                _byt[a] = byte.Parse(byt.Substring(a * 3, 3));
            }

            int j = 0;
            //_byt 요소를 순환한다
            for (int i = 0; i < _byt.Length; i++)
            {
                //_key가 _byt 보다 요소 수가 적을 때를 위해서
                if (j < _key.Length)
                {
                    j++;
                }
                else
                {
                    j = 1;
                }
                //xor로 복호화
                _byt[i] = (byte)(_byt[i] ^ _key[j - 1]);
            }
            
            //byte를 문자열로 변환한다 
            return Encoding.ASCII.GetString(_byt);
        }
    }

    
}
