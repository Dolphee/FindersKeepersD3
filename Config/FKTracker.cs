using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FindersKeepers.Controller.Enums;
using FindersKeepers.Helpers;
using FindersKeepers.Controller;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Controller.Actor;
using FK.UI;

namespace FindersKeepers
{
    public class FKTracker : SetDefault, ICache
    {
        [XmlArray("Legendary")]
        [XmlArrayItem("Item", typeof(LegendaryItems))]
        public List<LegendaryItems> Item { get; set; }
        public static int ID { get; set; }

        public enum GameType
        {
            OpenWorld = 0,
            Rift = 1,
            GreaterRift = 2,
            KadalaGamble = 3,
            KanaiCube = 4
        }

        [Serializable]
        public class LegendaryItems
        {
            [XmlAttribute]
            public long ID;
            [XmlAttribute]
            public string Name;
            [XmlAttribute]
            public bool SetItem;
            [XmlAttribute]
            public bool AncientItem;
            [XmlAttribute]
            public GameType GameType;
            [XmlAttribute]
            public int Difficulty = 0;
            [XmlAttribute]
            public DateTime Found;
            [XmlAttribute]
            public string LevelArea;
            [XmlAttribute]
            public uint AccountID;

            [XmlIgnore]
            public string _LegendaryItemName = "";

            [XmlIgnore]
            public string LegendaryItemName
            {
                get
                {
                    if(_LegendaryItemName == "")
                        _LegendaryItemName = UniqueItemsController.TryGet(Name).ItemName ?? "Unknown";

                    return _LegendaryItemName;
                }
            }
            //[XmlAttribute]
           // public uint Season;
          //  [XmlAttribute]
          //  public uint Hardcore;
        }

        public void OnStartup()
        {
            ID = (Item != null) ? Item.Count : 0;
        }

        public object _DEFAULT()
        {
            return new FKTracker
            {
                Item = new List<LegendaryItems>()
            };
        }
    }

    public static class FKTrackerList
    {
        [XmlIgnore]
        public static List<FKTracker.LegendaryItems> ItemTemp = new List<FKTracker.LegendaryItems>();

        public static List<FKTracker.LegendaryItems> MergeList = new List<FKTracker.LegendaryItems>();
        public static readonly object LockObj = new object();

        public static void Add(FKTracker.LegendaryItems Item)
        {
            if (ItemTemp.Count > 20) // Dump to make sure it doesn't slow the adding
                Push();

            Item.ID = FKTracker.ID;
            ItemTemp.Add(Item);

            FKTracker.ID = FKTracker.ID + 1;
        }

        public static void Push()
        {
            lock(LockObj)
            {
                MergeList.AddRange(ItemTemp);
                ItemTemp = new List<FKTracker.LegendaryItems>();
            }
        }

        public static void PushToSave()
        {
            Push();
            Config.Get<FKTracker>().Item.AddRange(MergeList);

            /* Push Save general pickups*/
            foreach (var x in GameManager.Instance.GameManagerData)
            {
                if (!Config.Get<FKStats>().FKStat.Accounts.Exists(e => e.ID == x.Key))
                    continue;
            }
        }


    }

}
