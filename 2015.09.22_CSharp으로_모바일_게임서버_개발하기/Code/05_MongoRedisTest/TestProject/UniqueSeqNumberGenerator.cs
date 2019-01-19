using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 채번 생성 
namespace TestProject
{
    class UniqueSeqNumberGenerator
    {
        static Snowflake.IdWorker worker;

        /// <summary>
        /// snowflake 초기화. 실패가 발생하면 예외를 던져준다.
        /// </summary>
        /// <param name="workerId">workerId 최고값은 31</param>
        /// <param name="dataCenterId">dataCenterId 최고값은 31</param>
        public static void Init(Int64 workerId, Int64 dataCenterId)
        {
            worker = new Snowflake.IdWorker(workerId, dataCenterId);
        }

        public static Int64 채번_받아오기()
        {
            return worker.NextId();
        }
    }
}
