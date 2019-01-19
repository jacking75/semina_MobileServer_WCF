using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace APILogicLib.ContentsData
{
    public static class Manager
    {
        public static bool IsLoadSuccessed { get; private set; }

        static Dictionary<int, ItemData> ItemSet = new Dictionary<int, ItemData>();
        static Dictionary<int, SkillData> SkillSet = new Dictionary<int, SkillData>();

        public static ERROR_ID Load()
        {
            Clear();

            try
            {
                LoadItem();
                LoadSkill();

                IsLoadSuccessed = true;
                return ERROR_ID.NONE;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return ERROR_ID.EXCEPTION_GAME_CONTENT_LOAD;
            }
        }

        static void Clear()
        {
            IsLoadSuccessed = false;

            ItemSet.Clear();
            SkillSet.Clear();
        }


        static bool LoadItem()
        {
            try
            {
                var dbCollection = DB.Mongo.GetGameContentsDBCollection<BsonDocument>
                                                        ("Item");

                var buildingList = dbCollection.FindAll();

                foreach (var data in buildingList)
                {
                    var dbdata = new ItemData();
                    var id = dbdata.SetData(data);

                    ItemSet.Add(id, dbdata);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return false;
            }
        }

        static bool LoadSkill()
        {
            try
            {
                var dbCollection = DB.Mongo.GetGameContentsDBCollection<BsonDocument>
                                                        ("Skill");

                var buildingList = dbCollection.FindAll();

                foreach (var data in buildingList)
                {
                    var dbdata = new SkillData();
                    var id = dbdata.SetData(data);

                    SkillSet.Add(id, dbdata);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return false;
            }
        }
    }
}
