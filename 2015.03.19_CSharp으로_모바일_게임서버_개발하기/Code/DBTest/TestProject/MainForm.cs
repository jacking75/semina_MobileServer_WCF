using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TestProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Redis.SetWriteLogFunc(this.WriteUILog);

            MongoDBLib.SetDBInfo(textBoxMongoConnectString.Text, textBoxMongoDBName.Text);
        }

        void WriteUILog(string msg)
        {
            listBoxLog.Items.Add(msg);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        // 레디스 연결
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Redis.Init(textBoxRedisAddress.Text);
                if (result == ERROR_ID.NONE)
                {
                    WriteUILog("Redis 접속 성공");

                    button2.Enabled = button3.Enabled = button4.Enabled = true;
                    button7.Enabled = button6.Enabled = button5.Enabled = true;
                    button8.Enabled = button9.Enabled = button10.Enabled = button11.Enabled = button12.Enabled = true;
                }
                else
                {
                    WriteUILog(string.Format("레디스 접속 실패. {0}", result));
                }
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
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
                    await Redis.SetString<int>(REDIS_INT_KEY, textBoxRedisTestInt.Text.ToInt32());
                    WriteUILog(string.Format("String Set. {0} : {1}", REDIS_INT_KEY, textBoxRedisTestInt.Text));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    await Redis.SetString<double>(REDIS_DOUBLE_KEY, textBoxRedisTestDouble.Text.ToDouble());
                    WriteUILog(string.Format("String Set. {0} : {1}", REDIS_DOUBLE_KEY, textBoxRedisTestDouble.Text));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    await Redis.SetString<string>(REDIS_STRING_KEY, textBoxRedisTestString.Text);
                    WriteUILog(string.Format("String Set. {0} : {1}", REDIS_STRING_KEY, textBoxRedisTestString.Text));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
        }

        // 레디스 테스트(int, float, string) 검색
        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    var value = await Redis.GetString<int>(REDIS_INT_KEY);
                    WriteUILog(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_INT_KEY, value.Item2, value.Item1));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    var value = await Redis.GetString<double>(REDIS_DOUBLE_KEY);
                    WriteUILog(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_DOUBLE_KEY, value.Item2, value.Item1));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    var value = await Redis.GetString<string>(REDIS_STRING_KEY);
                    WriteUILog(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_STRING_KEY, value.Item2, value.Item1));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
        }

        // 레디스 테스트(int, float, string) 삭제
        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    var value = await Redis.DeleteString<int>(REDIS_INT_KEY);
                    WriteUILog(string.Format("String Delete. {0} : result({1})", REDIS_INT_KEY, value));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    var value = await Redis.DeleteString<double>(REDIS_DOUBLE_KEY);
                    WriteUILog(string.Format("String Delete. {0} : result({1})", REDIS_DOUBLE_KEY, value));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    var value = await Redis.DeleteString<string>(REDIS_STRING_KEY);
                    WriteUILog(string.Format("String Delete. {0} : result({1})", REDIS_STRING_KEY, value));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
        }


        const string REDIS_PERSION_KEY = "test_persion";

        // 레디스 테스트(PERSION) 추가
        private async void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestPName.Text) ||
                string.IsNullOrEmpty(textBoxRedisTestPAge.Text))
            {
                WriteUILog("Error: 이름이나 나이가 빈 값입니다");
                return;
            }

            var persion = new PERSION() { Name = textBoxRedisTestPName.Text, Age = textBoxRedisTestPAge.Text.ToInt32() };

            await Redis.SetString<PERSION>(REDIS_PERSION_KEY, persion);
            WriteUILog(string.Format("PERSION Set. {0} : {1}, {2}", REDIS_PERSION_KEY, persion.Name, persion.Age));
        }
        // 레디스 테스트(PERSION) 검색
        private async void button6_Click(object sender, EventArgs e)
        {
            var value = await Redis.GetString<PERSION>(REDIS_PERSION_KEY);
            WriteUILog(string.Format("PERSION Get. {0} : {1}, {2}. Result:{3}", REDIS_PERSION_KEY, value.Item2.Name, value.Item2.Age, value.Item1));
        }
        // 레디스 테스트(PERSION) 삭제
        private async void button5_Click(object sender, EventArgs e)
        {
            var value = await Redis.DeleteString<PERSION>(REDIS_PERSION_KEY);
            WriteUILog(string.Format("PERSION Delete. {0} : result({1})", REDIS_PERSION_KEY, value));
        }


        const string REDIS_LIST_KEY = "test_list";

        // 레디스 테스트(List) 추가
        private async void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestList.Text))
            {
                WriteUILog("Error: 빈 값입니다");
                return;
            }

            var value = await Redis.AddList<string>(REDIS_LIST_KEY, textBoxRedisTestList.Text);
            WriteUILog(string.Format("List 추가. {0} : {1}. Count:{2})", REDIS_LIST_KEY, textBoxRedisTestList.Text, value));
        }
        // 레디스 테스트(List) 검색
        private async void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestListR1.Text) ||
                string.IsNullOrEmpty(textBoxRedisTestListR2.Text))
            {
                var value = await Redis.GetList<string>(REDIS_LIST_KEY, 0);
                WriteUILog(string.Format("List 추가. {0} : {1})", REDIS_LIST_KEY, string.Join(",", value)));
            }
            else
            {
                int pos1 = textBoxRedisTestListR1.Text.ToInt32();
                int pos2 = textBoxRedisTestListR2.Text.ToInt32();
                var value = await Redis.GetList<string>(REDIS_LIST_KEY, pos1, pos2);
                WriteUILog(string.Format("List 추가. {0} : {1})", REDIS_LIST_KEY, string.Join(",", value)));
            }
        }
        // 레디스 테스트(List) 삭제
        private async void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestListCount.Text))
            {
                WriteUILog("Error: 빈 값입니다");
                return;
            }
            else
            {
                var deleteValue = textBoxRedisTestList.Text;
                int count = textBoxRedisTestListCount.Text.ToInt32();
                var value = await Redis.DeleteList<string>(REDIS_LIST_KEY, deleteValue, count);
                WriteUILog(string.Format("List 삭제. {0} : {1})", REDIS_LIST_KEY, value));
            }
        }
        // 레디스 테스트(List) 삭제. 왼쪽에서 Pop
        private async void button11_Click(object sender, EventArgs e)
        {
            var value = await Redis.DeleteList<string>(REDIS_LIST_KEY, true);
            WriteUILog(string.Format("List 왼쪽에서 Pop. {0} : {1})", REDIS_LIST_KEY, value));
        }
        // 레디스 테스트(List) 삭제. 오른쪽에서 Pop
        private async void button12_Click(object sender, EventArgs e)
        {
            var value = await Redis.DeleteList<string>(REDIS_LIST_KEY, false);
            WriteUILog(string.Format("List 오른쪽에서 Pop. {0} : {1})", REDIS_LIST_KEY, value));
        }


        // GameUser1 추가
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                var collection = MongoDBLib.GetDBCollection<GameUser1>("GameUser1");

                var newData = new GameUser1()
                {
                    Name = textBox3.Text,
                    Age = textBox2.Text.ToInt32(),
                };

                collection.Insert(newData);

                WriteUILog(string.Format("GameUser1:{0} 추가", newData.Name));
            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // GameUser1 검색
        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox3.Text;

                var collection = MongoDBLib.GetDBCollection<GameUser1>("GameUser1");

                var users = collection.Find(MongoDB.Driver.Builders.Query.EQ("Name", findUserName));

                if (users.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        WriteUILog(string.Format("GameUser1:{0}, Age:{1}", user.Name, user.Age));
                    }
                }
                else
                {
                    WriteUILog(string.Format("GameUser1:{0}를 찾을 수 없습니다", findUserName));
                }
                
            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // GameUser1 삭제
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox3.Text;

                var collection = MongoDBLib.GetDBCollection<GameUser1>("GameUser1");

                collection.Remove(MongoDB.Driver.Builders.Query.EQ("Name", findUserName));

                WriteUILog(string.Format("GameUser1:{0}를 삭제했습니다", findUserName));
            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // GameUser2 추가
        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                var collection = MongoDBLib.GetDBCollection<GameUser2>("GameUser2");

                var newData = new GameUser2()
                {
                    _id = textBox5.Text,
                    Age = textBox4.Text.ToInt32(),
                    NicNameList = new List<string>() { textBox1.Text, textBox6.Text },
                };

                collection.Insert(newData);

                WriteUILog(string.Format("GameUser2:{0} 추가", newData._id));
            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // GameUser2 삭제
        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;

                var collection = MongoDBLib.GetDBCollection<GameUser2>("GameUser2");

                collection.Remove(MongoDB.Driver.Builders.Query.EQ("_id", findUserName));

                WriteUILog(string.Format("GameUser2:{0}를 삭제했습니다", findUserName));
            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // GameUser2 검색: 이름
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;

                var collection = MongoDBLib.GetDBCollection<MongoDB.Bson.BsonDocument>("GameUser2");
                var query = MongoDB.Driver.Builders.Query.EQ("_id", findUserName);
                var fields = MongoDB.Driver.Builders.Fields.Include("Age").Include("NicNameList");

                var users = collection.Find(query).SetFields(fields);

                if (users.Count() > 0)
                {
                    foreach (var data in users)
                    {
                        var age = data["Age"].AsInt32;
                        var nickList = data["NicNameList"].AsBsonArray.Select(p => p.AsString).ToList();

                        WriteUILog(string.Format("GameUser2:{0}, Age:{1}, Nick:{2},{3}", findUserName, age, nickList[0], nickList[1]));
                    }
                }
                else
                {
                    WriteUILog(string.Format("GameUser2:{0}를 찾을 수 없습니다", findUserName));
                }

            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // GameUser2 검색: 이름 + 나이
        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;

                var collection = MongoDBLib.GetDBCollection<GameUser2>("GameUser2");

                // 이름이 같고, 지정한 나이보다 같거나 큰
                var query = MongoDB.Driver.Builders.Query.And(MongoDB.Driver.Builders.Query.EQ("_id", findUserName),
                                    MongoDB.Driver.Builders.Query.GTE("Age", textBox4.Text.ToInt32()));
                
                var users = collection.Find(query);

                if (users.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        WriteUILog(string.Format("GameUser2:{0}, Age:{1}, Nick:{2},{3}", user._id, user.Age, user.NicNameList[0], user.NicNameList[1]));
                    }
                }
                else
                {
                    WriteUILog(string.Format("GameUser2:{0}를 찾을 수 없습니다", findUserName));
                }

            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
            }
        }

        // 수정: 닉네임
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                var findUserName = textBox5.Text;
                var newNickNameList = new List<string>() { textBox1.Text, textBox6.Text };

                var collection = MongoDBLib.GetDBCollection<MongoDB.Bson.BsonDocument>("GameUser2");

                var modifyArgs = new MongoDB.Driver.FindAndModifyArgs()
                {
                    Query = MongoDB.Driver.Builders.Query.EQ("_id", findUserName),
                    Update = MongoDB.Driver.Builders.Update.Set("NicNameList", new MongoDB.Bson.BsonArray(newNickNameList)),
                    Fields = MongoDB.Driver.Builders.Fields.Include("NicNameList"),
                    SortBy = MongoDB.Driver.Builders.SortBy.Null,
                    VersionReturned = MongoDB.Driver.FindAndModifyDocumentVersion.Modified,
                    Upsert = false,
                };

                var newResult = collection.FindAndModify(modifyArgs);

                if (newResult.ModifiedDocument == null)
                {
                    WriteUILog(string.Format("GameUser2:{0} 닉네임 변경 실패", findUserName));
                }
                else
                {
                    var nickList = newResult.ModifiedDocument["NicNameList"].AsBsonArray.Select(p => p.AsString).ToList();
                    WriteUILog(string.Format("GameUser2:{0} 닉네임 변경 {1}, {2}", findUserName, nickList[0], nickList[1]));
                }
            }
            catch (Exception ex)
            {
                WriteUILog(ex.Message);
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
        public List<string> NicNameList;
    }
}
