using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib.DB
{
    // <<<<<<<<< MongoDB >>>>>>>>>>>>
    // ORM으로 몽고디비에서 사용할 때는 struct는 안된다.
    public class DBUserAccountInfo
    {
        public string _id; // User ID
        public string PW;
        public Int64 UID;

        public bool IsInvalid()
        {
            return UID == 0 ? true : false;
        }
    }

    public class DBUserBasicGameData
    {
        public string _id; // User ID
        public string Name;
        public Int64 Money;
        public List<bool> UsablePosList = new List<bool>(); 
    }





    // <<<<<<< Redis >>>>>>>>>>>
    public struct DBUserSession
    {
        public string AuthToken;
        public short CV;            // ClientVersion
        public short CDV;           // ClientDataVersion
    }
}
