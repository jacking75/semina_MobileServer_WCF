using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// http://neue.cc/2015/02/06_504.html
using CloudStructures;

namespace TestProject
{
    public static class RedisLib
    {
        static RedisGroup redisGroupBasic = null;
        
        
        public static ERROR_ID Init(string address)
        {
            try
            {                
                var basicRedisConnectString = address.Split(",").ToList();
                var redisSettings = new RedisSettings[basicRedisConnectString.Count];
                
                if (basicRedisConnectString.Count() > 0)
                {
                    for (int i = 0; i < basicRedisConnectString.Count(); ++i)
                    {
                        redisSettings[i] = new RedisSettings(basicRedisConnectString[i], db: 0);
                    }

                    redisGroupBasic = new RedisGroup(groupName: "Basic", settings: redisSettings);
                }

                // 초기에 연결하도록 한다.
                for (int i = 0; i < basicRedisConnectString.Count; ++i)
                {
                    var key = i.ToString() + "_test";
                    var redis = new RedisString<int>(redisGroupBasic, key);
                    var result = redis.Set(11);
                    if (result.Result == false)
                    {
                        return ERROR_ID.REDIS_START_SET_TEST;
                    }
                }


                return ERROR_ID.NONE;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.ToString());
                return ERROR_ID.REDIS_START_EXCEPTION;
            }
        }


        //****** float은 사용불가. 대신 double 사용 가능

        public static async Task<bool> SetString<T>(string key, T dataObject)
        {
            try
            {
                var redis = new RedisString<T>(redisGroupBasic, key);
                await redis.Set(dataObject);
                return true;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return false;
            }
        }

        public static async Task<Tuple<bool,T>> GetString<T>(string key)
        {
            try
            {
                var redis = new RedisString<T>(redisGroupBasic, key);
                var value = await redis.Get();

                if (value.Value == null)
                {
                    return Tuple.Create(false, default(T));
                }

                return Tuple.Create(true, value.Value);
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return Tuple.Create(false, default(T));
            }
        }

        public static async Task<Int64> Increment(string key, Int64 value)
        {
            try
            {
                var redis = new RedisString<Int64>(redisGroupBasic, key);
                var result = await redis.Increment(value);
                return result;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return 0;
            }
        }

        public static async Task<bool> DeleteString<T>(string key)
        {
            try
            {
                var redis = new RedisString<T>(redisGroupBasic, key);
                var result = await redis.Delete();
                return result;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return false;
            }
        }

        public static async Task<long> AddList<T>(string key, T value)
        {
            try
            {
                var redis = new RedisList<T>(redisGroupBasic, key);
                var result = await redis.LeftPush(value);
                return result;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return 0;
            }
        }

        public static async Task<List<T>> GetList<T>(string key, int startPos, int endPos)
        {
            try
            {
                var redis = new RedisList<T>(redisGroupBasic, key);
                var result = await redis.Range(startPos, endPos);
                return result.ToList();
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return null;
            }
        }
        public static async Task<List<T>> GetList<T>(string key, int startPos)
        {
            try
            {
                var redis = new RedisList<T>(redisGroupBasic, key);
                var result = await redis.Range(startPos, -1);
                return result.ToList();
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value">이 값과 같은 것만 지운다</param>
        /// <param name="count">삭제할 개수. count가 0 보다 크면 왼쪽에서, count가 0 보다 작으면 오른쪽에서, 0과 같으면 모두 지운다</param>
        /// <returns></returns>
        public static async Task<long> DeleteList<T>(string key, T value, int count)
        {
            try
            {
                var redis = new RedisList<T>(redisGroupBasic, key);
                var result = await redis.Remove(value, count);
                return result;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return 0;
            }
        }
        public static async Task<T> DeleteList<T>(string key, bool IsLeft)
        {
            try
            {
                var redis = new RedisList<T>(redisGroupBasic, key);

                if (IsLeft)
                {
                    var result = await redis.LeftPop();
                    return result.Value;
                }
                else
                {
                    var result = await redis.RightPop();
                    return result.Value;
                }
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
                return default(T);
            }
        }



        //static void WriteLog(string msg)
        //{
        //    WriteLogFunc(msg);
        //}

        //static Action<string> WriteLogFunc = null;
        //public static void SetWriteLogFunc(Action<string> func)
        //{
        //    WriteLogFunc = func;
        //}

        

       
    }
}
