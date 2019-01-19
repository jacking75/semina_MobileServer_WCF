using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

// http://qiita.com/y_minowa/items/685db9926dec0d6b711b

// http://www.atmarkit.co.jp/fdotnet/dotnettips/039inifile/inifile.html

namespace APILogicLib
{
    class INIReadWrite
    {
        /// <summary>
        /// ini 파일 패스
        /// </summary>
        private String filePath { get; set; }

        // ==========================================================
        [DllImport("KERNEL32.DLL")]
        public static extern uint
            GetPrivateProfileString(string lpAppName,
            string lpKeyName, string lpDefault,
            StringBuilder lpReturnedString, uint nSize,
            string lpFileName);

        [DllImport("KERNEL32.DLL")]
        public static extern uint
            GetPrivateProfileInt(string lpAppName,
            string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpstring,
            string lpFileName);
        // ==========================================================


        public INIReadWrite(string fileFullPath)
        {
            filePath = fileFullPath;
        }
        //public InifileUtils()
        //{
        //    //this.filePath = AppDomain.CurrentDomain.BaseDirectory + "hogehoge.ini";
        //}

        //public InifileUtils(String filePath)
        //{
        //    this.filePath = filePath;
        //}

        /// <summary>
        /// ini 파일 중의 섹션 키를 지정해서 문자열을 반환
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public String getValueString(String section, String key)
        {
            StringBuilder sb = new StringBuilder(1024);

            GetPrivateProfileString(
                section,
                key,
                "",
                sb,
                Convert.ToUInt32(sb.Capacity),
                filePath);

            return sb.ToString();
        }

        /// <summary>
        /// ini 파일 중의 섹션 키를 지정해서 숫자 값을 반환
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int getValueInt(String section, String key)
        {
            return (int)GetPrivateProfileInt(section, key, 0, filePath);
        }

        /// <summary>
        /// 지정한 섹션, 키에 수치를 쓴다
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void setValue(String section, String key, int val)
        {
            setValue(section, key, val.ToString());
        }

        /// <summary>
        /// 지정한 섹션, 키에 문자열를 쓴다
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void setValue(String section, String key, String val)
        {
            WritePrivateProfileString(section, key, val, filePath);
        }
    }
}
