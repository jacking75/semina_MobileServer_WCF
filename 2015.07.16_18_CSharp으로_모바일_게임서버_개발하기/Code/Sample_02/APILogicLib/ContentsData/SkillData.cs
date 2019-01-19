using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace APILogicLib.ContentsData
{
    class SkillData
    {
        public int ID { get; private set; }  
        public int SkillEffect { get; private set; } 
        

        public int SetData(BsonDocument document)
        {
            ID = document["ID"].AsInt32;

            try
            {
                SkillEffect = document["SkillEffect"].AsInt32;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return -1;
            }
                        
            return ID;
        }
    }
}
