using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib.Data
{
    // 유저의 게임 아이템 저장소
    public static class ItemRepository
    {
        static Int64 SeqNumber = 0;
        static List<GameItem> GameItems = new List<GameItem>();


        public static void AddItem(string id)
        {
            var rand = new Random();

            ++SeqNumber;

            var item = new GameItem()
            {
                UID = SeqNumber,
                ID = id,
                DummyData = rand.Next(1, 1212121),
            };

            GameItems.Add(item);
        }

        public static List<GameItem> GetItems(string id)
        {
            return GameItems.FindAll(data => data.ID == id);
        }
    }

    public class GameItem
    {
        public Int64 UID; // Unique Index
        public string ID;
        public int DummyData;
    }
}
