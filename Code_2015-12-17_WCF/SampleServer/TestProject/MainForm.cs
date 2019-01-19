using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using APILogicLib;
using DB = APILogicLib.DB;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void WriteUILog(string msg)
        {
            listBoxLog.Items.Add(msg);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        
        // 암호화 패킷 보내기
        private async void button1_Click_1(object sender, EventArgs e)
        {
            var api = textBoxGameServerAddress + "TestEncryto";

            var userID = "jacking";
            var reqPacket = new REQ_DEV_ECHO()
            {
                WaitSec = 11,
                ReqData = "test",
            };

            var sendData = EncryotRequestData<REQ_DEV_ECHO>(userID, reqPacket);
            var requestJson = Jil.JSON.Serialize(sendData);

            
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(requestJson));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var Network = new System.Net.Http.HttpClient();
            var response = await Network.PostAsync(api, content);

            if (response.IsSuccessStatusCode == false)
            {
                WriteUILog("[Error] RequestHttpAESEncry Network");
                return;
            }

            
            var responeString = await response.Content.ReadAsStringAsync();
            var responseJson = Jil.JSON.Deserialize<RES_ENCRYTO_DATA>(responeString);

            if (responseJson.Result != (short)ERROR_ID.NONE)
            {
                WriteUILog(string.Format("[Error] RequestHttpAESEncry Response Error: {0}", responseJson.Result));
                return;
            }

            var decryptData = DecryptResponseData<RES_DEV_ECHO>(responseJson.Data);
            WriteUILog(string.Format("암호화 패킷 주고 받기 성공. {0}, {1}", decryptData.Result, decryptData.ResData));
            
        }

        REQ_ENCRYTO_DATA EncryotRequestData<REQUSET_T>(string userID, REQUSET_T result)
        {
            var dynamicKey = "f&dsf";
            
            var jsonResObject = Jil.JSON.Serialize<REQUSET_T>(result);
            var encryData = AESEncrypt.Encrypt(dynamicKey, jsonResObject);

            var request = new REQ_ENCRYTO_DATA { ID = userID, Data = encryData };
            return request;
        }

        RESULT_T DecryptResponseData<RESULT_T>(string request)
        {
            try
            {                
                var dynamicKey = "f&d23";
                var decryptData = AESEncrypt.Decrypt(dynamicKey, request);
                var jsonObject = Jil.JSON.Deserialize<RESULT_T>(decryptData);
                return jsonObject;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.ToString());
                return default(RESULT_T);
            }
        }

        
    }


    
}
