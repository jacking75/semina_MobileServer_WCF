using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

// 클라이언트의 요청을 순차적으로 처리하도록 한다.
namespace APILogicLib
{
    public class RequestLock : IDisposable
    {
        ERROR_ID 요청을_할수있다 = ERROR_ID.PREV_REQUEST_NOT_COMPLETE;
        string key;

        public RequestLock(string id)
        {
            key = string.Format("{0}:ReqTime", id);
        }

        public void SetInit()
        {
            DB.Redis.SetStringNoReturn<double>(key, 0);
        }

        public async Task<ERROR_ID> 요청_처리중인가([System.Runtime.CompilerServices.CallerFilePath] string fileName = "",
                                [System.Runtime.CompilerServices.CallerMemberName] string methodName = "",
                                [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            double prevAllTimeSec = 0;
            var curAllTimeSec = Util.TimeTickToSec(DateTime.Now.Ticks);

            try
            {
                var result = await DB.Redis.GetString<double>(key);
                
                if (result.Item1)
                {
                    prevAllTimeSec = result.Item2;

                    if (prevAllTimeSec > 0)
                    {
                        return 요청을_할수있다;
                    }
                }

                var changeData = await DB.Redis.Increment(key, curAllTimeSec);
                if (changeData != curAllTimeSec)
                {
                    return 요청을_할수있다;
                }
                
                요청을_할수있다 = ERROR_ID.NONE;
                return 요청을_할수있다;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return ERROR_ID.PREV_REQUEST_FAIL_REDIS;
            }
        }

        public void Dispose()
        {
            if (요청을_할수있다 == ERROR_ID.NONE)
            {
                SetInit();
            }
        }

    }
}
