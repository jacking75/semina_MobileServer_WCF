using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

// 클라이언트의 요청을 순차적으로 처리하도록 한다.
namespace RESTfulAPILib.Request
{
    public class RequestLock : IDisposable
    {
        ERROR_CODE 요청을_할수있다 = ERROR_CODE.PREV_REQUEST_NOT_COMPLETE;
        string key;

        public RequestLock(string id)
        {
            key = string.Format("{0}:AT", id);
        }

        public void SetInit()
        {
            DB.Redis.SetStringNoReturn<double>(key, 0);
        }

        public ERROR_CODE 요청_처리중인가([System.Runtime.CompilerServices.CallerFilePath] string fileName = "",
                                [System.Runtime.CompilerServices.CallerMemberName] string methodName = "",
                                [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            double prevAllTimeSec = 0;
            var curAllTimeSec = TimeTickToSec(DateTime.Now.Ticks);

            try
            {
                var result = DB.Redis.GetString<double>(key);
                
                if (result.Item1)
                {
                    prevAllTimeSec = result.Item2;

                    if (prevAllTimeSec > 0)
                    {
                        return 요청을_할수있다;
                    }
                }
                else
                {
                    Console.WriteLine("[RequestLock] 처음 요청");
                    DB.Redis.SetStringAsync<double>(key, 0).Wait();
                }

                var changeData = DB.Redis.Increment(key, curAllTimeSec);
                if (changeData != curAllTimeSec)
                {
                    return 요청을_할수있다;
                }

                요청을_할수있다 = ERROR_CODE.NONE;
                return 요청을_할수있다;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[RequestLock] 예외발생: " + ex.Message);
                return ERROR_CODE.PREV_REQUEST_FAIL_REDIS;
            }
        }

        public void Dispose()
        {
            if (요청을_할수있다 == ERROR_CODE.NONE)
            {
                SetInit();
            }
        }

        Int64 TimeTickToSec(Int64 curTimeTick)
        {
            Int64 sec = (Int64)(curTimeTick / TimeSpan.TicksPerSecond);
            return sec;
        }

    }
}
