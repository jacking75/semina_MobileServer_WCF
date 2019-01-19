using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    public static class Util
    {
        static int GenerateSecureNumber()
        {
            var SecureNumberRandom = new Random((int)DateTime.Now.Ticks);
            var number = SecureNumberRandom.Next(1527, 59841);
            return number;
        }

        public static string GenerateSecureNumber2(int startNumber, int endNumber, int count)
        {
            var SecureNumberRandom = new Random((int)DateTime.Now.Ticks);
            StringBuilder secureString = new StringBuilder();

            for (int i = 0; i < count; ++i)
            {
                secureString.Append(SecureNumberRandom.Next(startNumber, endNumber).ToString());
            }

            return secureString.ToString();
        }

        // 출처: http://stackoverflow.com/questions/54991/generating-random-passwords
        public static string GenerateSecureString(int lowercase, int uppercase, int numerics)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }



        public static string DateTimeToyyyyMMddHHmmss(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmss");
        }

        public static DateTime yyyyMMddHHmmssToDateTime(string timeString)
        {
            DateTime date = DateTime.ParseExact(timeString, "yyyyMMddHHmmss", null);
            return date;
        }       
        
        public static Int64 TimeTickToSec(Int64 curTimeTick)
        {
            Int64 sec = (Int64)(curTimeTick / TimeSpan.TicksPerSecond);
            return sec;
        }

        public static string 내아이피주소
        {
            get
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                string clientIP = string.Empty;
                for (int i = 0; i < host.AddressList.Length; i++)
                {
                    // AddressFamily.InterNetworkV6 - IPv6
                    if (host.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        clientIP = host.AddressList[i].ToString();
                    }
                }
                return clientIP;
            }
        }
       
    }
}
