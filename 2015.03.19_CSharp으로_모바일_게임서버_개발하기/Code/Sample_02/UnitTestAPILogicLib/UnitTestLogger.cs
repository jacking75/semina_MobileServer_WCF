using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestAPILogicLib
{
    [TestClass]
    public class UnitTestLogger
    {
        [TestMethod]
        public void TestMethod_EnableFileWrite()
        {
            int write = 0;
            var type = APILogicLib.Logger.LOG_TYPE.NONE;
            var isEnable = false;

            var TestObject = new PrivateType(typeof(APILogicLib.Logger));


            // 파일에 쓰기. 성공
            write = (int)APILogicLib.Logger.LOG_TYPE.FILE;
            type = APILogicLib.Logger.LOG_TYPE.FILE;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(true, isEnable);

            // 파일에 쓰기. 실패
            write = (int)APILogicLib.Logger.LOG_TYPE.DB;
            type = APILogicLib.Logger.LOG_TYPE.FILE;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(false, isEnable);



            // DB에 쓰기. 성공
            write = (int)APILogicLib.Logger.LOG_TYPE.DB;
            type = APILogicLib.Logger.LOG_TYPE.DB;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(true, isEnable);

            // DB에 쓰기. 실패
            write = (int)APILogicLib.Logger.LOG_TYPE.FILE;
            type = APILogicLib.Logger.LOG_TYPE.DB;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(false, isEnable);



            // FILE + DB에 쓰기. 성공
            write = 0;
            write |= (int)APILogicLib.Logger.LOG_TYPE.FILE;
            write |= (int)APILogicLib.Logger.LOG_TYPE.DB;

            type = APILogicLib.Logger.LOG_TYPE.FILE;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(true, isEnable);

            type = APILogicLib.Logger.LOG_TYPE.DB;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(true, isEnable);

            type = APILogicLib.Logger.LOG_TYPE.NETWORK;
            isEnable = (bool)TestObject.InvokeStatic("EnableFileWrite", new object[] { write, type });
            Assert.AreEqual(false, isEnable);
        }
    }
}
