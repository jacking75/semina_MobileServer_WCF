using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace APILogicLib.ContentsData
{
    class ItemData
    {
        public int ID { get; private set; }
        public int Level { get; private set; }


        public int SetData(BsonDocument document)
        {
            ID = document["ID"].AsInt32;

            try
            {
                Level = document["Level"].AsInt32;
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
