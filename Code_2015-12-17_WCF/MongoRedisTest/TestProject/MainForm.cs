using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;



namespace TestProject
{
    public partial class MainForm : Form
    {
        System.Windows.Threading.DispatcherTimer workProcessTimer = new System.Windows.Threading.DispatcherTimer();


        public MainForm()
        {
            InitializeComponent();

            workProcessTimer.Tick += new EventHandler(OnProcessTimedEvent);
            workProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            workProcessTimer.Start();


            //MongoDBLib1.SetDBInfo(textBoxMongoConnectString.Text, textBoxMongoDBName.Text);
            MongoDBLib.Common.SetDBInfo(textBoxMongoConnectString.Text, textBoxMongoDBName.Text);

            UniqueSeqNumberGenerator.Init(11, 11);
        }

        void OnProcessTimedEvent(object sender, EventArgs e)
        {
            try
            {
                ProcessLog();
            }
            catch (Exception ex)
            {
                DevLog.Write(string.Format("[OnProcessTimedEvent] Exception:{0}", ex.ToString()), LOG_LEVEL.ERROR);
            }
        }

        void ProcessLog()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (true)
            {
                string msg;

                if (DevLog.GetLog(out msg))
                {
                    ++logWorkCount;

                    if (listBoxLog.Items.Count > 512)
                    {
                        listBoxLog.Items.Clear();
                    }

                    listBoxLog.Items.Add(msg);
                    listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
                }
                else
                {
                    break;
                }

                if (logWorkCount > 32)
                {
                    break;
                }
            }
        }
        
        
        // 레디스 연결
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = RedisLib.Init(textBoxRedisAddress.Text);
                if (result == ERROR_ID.NONE)
                {
                    DevLog.Write("Redis 접속 성공");

                    button2.Enabled = button3.Enabled = button4.Enabled = true;
                    button7.Enabled = button6.Enabled = button5.Enabled = true;
                    button8.Enabled = button9.Enabled = button10.Enabled = button11.Enabled = button12.Enabled = true;
                }
                else
                {
                    DevLog.Write(string.Format("레디스 접속 실패. {0}", result));
                }
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.ToString());
            }
            
        }

        const string REDIS_INT_KEY = "test_int";
        const string REDIS_DOUBLE_KEY = "test_double";
        const string REDIS_STRING_KEY = "test_string";
                
        // 레디스 테스트(int, float, string) 추가
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    await RedisLib.SetString<int>(REDIS_INT_KEY, textBoxRedisTestInt.Text.ToInt32());
                    DevLog.Write(string.Format("String Set. {0} : {1}", REDIS_INT_KEY, textBoxRedisTestInt.Text));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    await RedisLib.SetString<double>(REDIS_DOUBLE_KEY, textBoxRedisTestDouble.Text.ToDouble());
                    DevLog.Write(string.Format("String Set. {0} : {1}", REDIS_DOUBLE_KEY, textBoxRedisTestDouble.Text));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    await RedisLib.SetString<string>(REDIS_STRING_KEY, textBoxRedisTestString.Text);
                    DevLog.Write(string.Format("String Set. {0} : {1}", REDIS_STRING_KEY, textBoxRedisTestString.Text));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.ToString());
            }
        }

        // 레디스 테스트(int, float, string) 검색
        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    var value = await RedisLib.GetString<int>(REDIS_INT_KEY);
                    DevLog.Write(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_INT_KEY, value.Item2, value.Item1));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    var value = await RedisLib.GetString<double>(REDIS_DOUBLE_KEY);
                    DevLog.Write(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_DOUBLE_KEY, value.Item2, value.Item1));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    var value = await RedisLib.GetString<string>(REDIS_STRING_KEY);
                    DevLog.Write(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_STRING_KEY, value.Item2, value.Item1));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.ToString());
            }
        }

        // 레디스 테스트(int, float, string) 삭제
        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    var value = await RedisLib.DeleteString<int>(REDIS_INT_KEY);
                    DevLog.Write(string.Format("String Delete. {0} : result({1})", REDIS_INT_KEY, value));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    var value = await RedisLib.DeleteString<double>(REDIS_DOUBLE_KEY);
                    DevLog.Write(string.Format("String Delete. {0} : result({1})", REDIS_DOUBLE_KEY, value));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    var value = await RedisLib.DeleteString<string>(REDIS_STRING_KEY);
                    DevLog.Write(string.Format("String Delete. {0} : result({1})", REDIS_STRING_KEY, value));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.ToString());
            }
        }


        const string REDIS_PERSION_KEY = "test_persion";

        // 레디스 테스트(PERSION) 추가
        private async void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestPName.Text) ||
                string.IsNullOrEmpty(textBoxRedisTestPAge.Text))
            {
                DevLog.Write("Error: 이름이나 나이가 빈 값입니다");
                return;
            }

            var persion = new PERSION() { Name = textBoxRedisTestPName.Text, Age = textBoxRedisTestPAge.Text.ToInt32() };

            await RedisLib.SetString<PERSION>(REDIS_PERSION_KEY, persion);
            DevLog.Write(string.Format("PERSION Set. {0} : {1}, {2}", REDIS_PERSION_KEY, persion.Name, persion.Age));
        }
        // 레디스 테스트(PERSION) 검색
        private async void button6_Click(object sender, EventArgs e)
        {
            var value = await RedisLib.GetString<PERSION>(REDIS_PERSION_KEY);
            DevLog.Write(string.Format("PERSION Get. {0} : {1}, {2}. Result:{3}", REDIS_PERSION_KEY, value.Item2.Name, value.Item2.Age, value.Item1));
        }
        // 레디스 테스트(PERSION) 삭제
        private async void button5_Click(object sender, EventArgs e)
        {
            var value = await RedisLib.DeleteString<PERSION>(REDIS_PERSION_KEY);
            DevLog.Write(string.Format("PERSION Delete. {0} : result({1})", REDIS_PERSION_KEY, value));
        }


        const string REDIS_LIST_KEY = "test_list";

        // 레디스 테스트(List) 추가
        private async void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestList.Text))
            {
                DevLog.Write("Error: 빈 값입니다");
                return;
            }

            var value = await RedisLib.AddList<string>(REDIS_LIST_KEY, textBoxRedisTestList.Text);
            DevLog.Write(string.Format("List 추가. {0} : {1}. Count:{2})", REDIS_LIST_KEY, textBoxRedisTestList.Text, value));
        }
        // 레디스 테스트(List) 검색
        private async void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestListR1.Text) ||
                string.IsNullOrEmpty(textBoxRedisTestListR2.Text))
            {
                var value = await RedisLib.GetList<string>(REDIS_LIST_KEY, 0);
                DevLog.Write(string.Format("List 추가. {0} : {1})", REDIS_LIST_KEY, string.Join(",", value)));
            }
            else
            {
                int pos1 = textBoxRedisTestListR1.Text.ToInt32();
                int pos2 = textBoxRedisTestListR2.Text.ToInt32();
                var value = await RedisLib.GetList<string>(REDIS_LIST_KEY, pos1, pos2);
                DevLog.Write(string.Format("List 추가. {0} : {1})", REDIS_LIST_KEY, string.Join(",", value)));
            }
        }
        // 레디스 테스트(List) 삭제
        private async void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestListCount.Text))
            {
                DevLog.Write("Error: 빈 값입니다");
                return;
            }
            else
            {
                var deleteValue = textBoxRedisTestList.Text;
                int count = textBoxRedisTestListCount.Text.ToInt32();
                var value = await RedisLib.DeleteList<string>(REDIS_LIST_KEY, deleteValue, count);
                DevLog.Write(string.Format("List 삭제. {0} : {1})", REDIS_LIST_KEY, value));
            }
        }
        // 레디스 테스트(List) 삭제. 왼쪽에서 Pop
        private async void button11_Click(object sender, EventArgs e)
        {
            var value = await RedisLib.DeleteList<string>(REDIS_LIST_KEY, true);
            DevLog.Write(string.Format("List 왼쪽에서 Pop. {0} : {1})", REDIS_LIST_KEY, value));
        }
        // 레디스 테스트(List) 삭제. 오른쪽에서 Pop
        private async void button12_Click(object sender, EventArgs e)
        {
            var value = await RedisLib.DeleteList<string>(REDIS_LIST_KEY, false);
            DevLog.Write(string.Format("List 오른쪽에서 Pop. {0} : {1})", REDIS_LIST_KEY, value));
        }


        // GameUser1 추가
        private async void button15_Click(object sender, EventArgs e)
        {
            try
            {                
                var newData = new GameUser1()
                {
                    Name = textBox3.Text,
                    Age = textBox2.Text.ToInt32(),
                };

                var collection = MongoDBLib.Common.GetDBCollection<GameUser1>("GameUser1");
                await collection.InsertOneAsync(newData);

                DevLog.Write(string.Format("GameUser1:{0} 추가", newData.Name));
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // GameUser1 검색
        private async void button14_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox3.Text;

                var collection = MongoDBLib.Common.GetDBCollection<GameUser1>("GameUser1");

                var users = await collection.Find(x => x.Name == findUserName).ToListAsync();

                if (users.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        DevLog.Write(string.Format("GameUser1:{0}, Age:{1}", user.Name, user.Age));
                    }
                }
                else
                {
                    DevLog.Write(string.Format("GameUser1:{0}를 찾을 수 없습니다", findUserName));
                }
                
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // GameUser1 삭제
        private async void button13_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox3.Text;

                var collection = MongoDBLib.Common.GetDBCollection<GameUser1>("GameUser1");

                await collection.DeleteOneAsync(x => x.Name == findUserName);

                DevLog.Write(string.Format("GameUser1:{0}를 삭제했습니다", findUserName));
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // GameUser2 추가
        private async void button18_Click(object sender, EventArgs e)
        {
            try
            {
                var collection = MongoDBLib.Common.GetDBCollection<GameUser2>("GameUser2");
                
                var newData = new GameUser2()
                {
                    _id = textBox5.Text,
                    Age = textBox4.Text.ToInt32(),
                    NickNameList = new List<string>() { textBox1.Text, textBox6.Text },
                };

                await collection.InsertOneAsync(newData);

                DevLog.Write(string.Format("GameUser2:{0} 추가", newData._id));
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // GameUser2 삭제
        private async void button16_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;

                var collection = MongoDBLib.Common.GetDBCollection<GameUser2>("GameUser2");

                await collection.DeleteOneAsync(x => x._id == findUserName);

                DevLog.Write(string.Format("GameUser2:{0}를 삭제했습니다", findUserName));
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // GameUser2 검색: 이름
        private async void button17_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;
                                
                var collection = MongoDBLib.Common.GetDBCollection<BsonDocument>("GameUser2");

                var filter = new BsonDocument("_id", findUserName);
                var projection = BsonDocument.Parse("{Age:1, NickNameList:1}");
                var users = await collection.Find(filter).Project(projection).ToListAsync();


                if (users.Count() > 0)
                {
                    foreach (var data in users)
                    {
                        var age = data["Age"].AsInt32;
                        var nickList = data["NickNameList"].AsBsonArray.Select(p => p.AsString).ToList();

                        DevLog.Write(string.Format("GameUser2:{0}, Age:{1}, Nick:{2},{3}", findUserName, age, nickList[0], nickList[1]));
                    }
                }
                else
                {
                    DevLog.Write(string.Format("GameUser2:{0}를 찾을 수 없습니다", findUserName));
                }

            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // GameUser2 검색: 이름 + 나이
        private async void button19_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;
                var findAge = textBox4.Text.ToInt32();

                var collection = MongoDBLib.Common.GetDBCollection<GameUser2>("GameUser2");
                var users = await collection.Find(x => x._id == findUserName && x.Age >= findAge).ToListAsync();

                
                if (users.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        DevLog.Write(string.Format("GameUser2:{0}, Age:{1}, Nick:{2},{3}", user._id, user.Age, user.NickNameList[0], user.NickNameList[1]));
                    }
                }
                else
                {
                    DevLog.Write(string.Format("GameUser2:{0}를 찾을 수 없습니다", findUserName));
                }

            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        // 수정: 닉네임
        private async void button20_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;
                var newNickNameList = new List<string>() { textBox1.Text, textBox6.Text };

                //var collection = MongoDBLib.Common.GetDBCollection<GameUser2>("GameUser2");

                //var result = await collection.FindOneAndUpdateAsync<GameUser2>(
                //                u => u._id == findUserName, 
                //                Builders<GameUser2>.Update.Set("NickNameList", new BsonArray(newNickNameList)), 
                //    new FindOneAndUpdateOptions<GameUser2, GameUser2>
                //    {
                //        ReturnDocument = ReturnDocument.After
                //    });

                //if (result == null)
                //{
                //    DevLog.Write(string.Format("GameUser2:{0} 닉네임 변경 실패", findUserName)); 
                //}
                //else
                //{
                //    DevLog.Write(string.Format("GameUser2:{0} 닉네임 변경 {1}, {2}", findUserName, result.NickNameList[0], result.NickNameList[1]));
                //}

                var collection = MongoDBLib.Common.GetDBCollection<BsonDocument>("GameUser2");

                var filter = new BsonDocument("_id", findUserName);
                var update = Builders<BsonDocument>.Update.Set("NickNameList", new BsonArray(newNickNameList));
                var options = new FindOneAndUpdateOptions<BsonDocument, BsonDocument>
                {
                    ReturnDocument = ReturnDocument.After
                };

                var document = await collection.FindOneAndUpdateAsync<BsonDocument>(filter, update, options);

                if (document == null)
                {
                    DevLog.Write(string.Format("GameUser2:{0} 닉네임 변경 실패", findUserName));
                }
                else
                {
                    var nickList = document["NickNameList"].AsBsonArray.Select(p => p.AsString).ToList();

                    DevLog.Write(string.Format("GameUser2:{0} 닉네임 변경 {1}, {2}", findUserName, nickList[0], nickList[1]));
                }

            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }               
    }


    struct PERSION
    {
        public string Name;
        public int Age;
    }


    // 몽고디비에 맵핑해서 사용하는 오브젝트는 오직 class만 가능
    class GameUser1
    {
        public MongoDB.Bson.ObjectId _id;
        public string Name;
        public int Age;
    }

    class GameUser2
    {
        public string _id;
        public int Age;
        public List<string> NickNameList;
    }

    public class DBUserItem
    {
        public Int64 _id; // Unique ID

        public string UserID;

        public int ItemID;

        [MongoDB.Bson.Serialization.Attributes.BsonElement("AD")]
        public DateTime AcquireDateTime; // 아이템 입수 시간
    }
}
