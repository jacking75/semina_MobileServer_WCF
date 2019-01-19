using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    public static class Main
    {
        public static bool EnableRequestHeathCheck { get; private set; }


        public static ERROR_ID Init()
        {
            ERROR_ID error;
            
            error = InitDB();
            if (error != ERROR_ID.NONE)
            {
                Logger.Error(string.Format("Starting. Fail DB:{0}", error));
                return error;
            }

            int workerId = 1;
            int dataCenterId = 1;
            UniqueSeqNumberGenerator.Init(workerId, dataCenterId);
            Logger.Info(string.Format("채번 workerId:{0}, dataCenterId:{1}", workerId, dataCenterId));

            //error = ContentsData.Manager.Load();
            //if (error != ERROR_ID.NONE)
            //{
            //    Logger.Error(string.Format("Starting. Fail Contents Loading:{0}", error));
            //    return error;
            //}

            EnableRequestHeathCheck = true;
            Logger.Info(string.Format("Starting. Success"));
            return ERROR_ID.NONE;
        }

        static ERROR_ID InitDB()
        {
            ERROR_ID error;
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServerConfig.ini");

            error = LoadMongoDBSetting(filePath);
            if (error != ERROR_ID.NONE)
            {
                Logger.Error(string.Format("Starting. Fail:{0}", error));
                return error;
            }

            //
            var redisList = LoadRedisDBSetting(filePath);
            if (string.IsNullOrEmpty(redisList))
            {
                Logger.Error(string.Format("Starting. Fail:{0}", ERROR_ID.LOAD_CONFIG_REDIS));
                return ERROR_ID.LOAD_CONFIG_REDIS;
            }


            error = DB.Redis.Init(redisList);
            if (error != ERROR_ID.NONE)
            {
                Logger.Error(string.Format("Starting. Fail:{0}", error));
                return error;
            }

            return error;
        }

        static ERROR_ID LoadMongoDBSetting(string filePath)
        {        
            try
            {
                INIReadWrite iniReader = new INIReadWrite(filePath);

                var dbConnect = iniReader.getValueString("DB", "AccountConnectString");
                var dbName = iniReader.getValueString("DB", "AccountDBName");
                DB.Mongo.SetAccountDBInfo(dbConnect, dbName);
                Logger.Info(string.Format("Starting. AccountConnectString:{0}, AccountDBName:{1}", dbConnect, dbName));

                dbConnect = iniReader.getValueString("DB", "GameConnectString");
                dbName = iniReader.getValueString("DB", "GameDBName");
                DB.Mongo.SetGameDBInfo(dbConnect, dbName);
                Logger.Info(string.Format("Starting. GameConnectString:{0}, GameDBName:{1}", dbConnect, dbName));
                
                return ERROR_ID.NONE;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.ToString());
                return ERROR_ID.LOAD_CONFIG_MONGODB;
            }
        }

        static string LoadRedisDBSetting(string filePath)
        {
            try
            {
                INIReadWrite iniReader = new INIReadWrite(filePath);

                var list = iniReader.getValueString("DB", "RedisList");
                Logger.Info(string.Format("Starting. Redis:{0}", list));

                return list;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.ToString());
                return "";
            }
        }
    }
    

}
