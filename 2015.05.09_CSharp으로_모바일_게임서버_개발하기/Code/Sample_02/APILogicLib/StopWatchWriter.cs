using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

namespace APILogicLib
{
    class StopWatchWriter
    {
        public static System.Diagnostics.Stopwatch 생성_시작()
        {
            var stopWatchWork = new System.Diagnostics.Stopwatch();
            stopWatchWork.Start();
            return stopWatchWork;
        }

        public static void 시작(System.Diagnostics.Stopwatch stopWatchWork)
        {
            stopWatchWork.Start();
        }

        public static void 재시작(System.Diagnostics.Stopwatch stopWatchWork)
        {
            stopWatchWork.Restart();
        }

        public static void 중단_후_시간_오버이면_로그출력(System.Diagnostics.Stopwatch stopWatchWork, long limitMS, string checkWork, bool isWriteDB = false,
                                    [CallerFilePath] string fileName = "",
                                    [CallerMemberName] string methodName = "",
                                   [CallerLineNumber] int lineNumber = 0)
        {
            stopWatchWork.Stop();                                     // StopWatchDBWork 완료

            if (stopWatchWork.ElapsedMilliseconds >= limitMS)
            {
                var msg = string.Format("[{0} {1} {2}] [{3}]-ProcTime limit over. expected: {4}ms, actuial: {5}ms.", fileName, methodName, lineNumber, checkWork, limitMS, stopWatchWork.ElapsedMilliseconds);
                Logger.Info(msg);
            }
        }

        public static void Test([CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            var msg = string.Format("[{0} {1} {2}] test", fileName, methodName, lineNumber);
            Logger.Info(msg);
        }
    }
}
