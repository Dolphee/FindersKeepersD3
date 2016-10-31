using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using PropertyChanged;

namespace FindersKeepers.Controller.GameManagerData
{
    
    public class GameManagerAccountHelper
    {
        [ThreadStatic]
        private static GameManagerAccounts _account;

        [ThreadStatic]
        private static GameManagerData _accountData;

        public static GameManagerAccounts Current
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }
        public static GameManagerData CurrentData
        {
            get
            {
                return _accountData;
            }
            set
            {
                _accountData = value;
            }
        }
    }

    public class GameManagerAccounts
    {
        public short GameMangerDataID { get; set; }
        public Engine DiabloIII { get; set; }
        public ActorCommonData Player { get; set; }
        public System.Windows.Point PlayerPosition { get; set; }
        public LevelAreaList LevelAreaContainer { get; set; } // Stores all actors shown on minimap
        public Helpers.LevelAreaHelper LevelAreaHelper { get; set; } // Stores all actors shown on minimap
        public FindersKeepers.Controller.Enums.Rift RiftHelper { get; set; } // Until rift has closed, then move to RiftManager
        public bool MainAccount { get; set; }
        public int LevelArea { get; set; }
        public GameStates Gamestate { get; set; }
        public short PlayerIndex = -1;
        public Hero HeroHelper { get; set; }

        public ActorCommonDataHelper PlayerHelper;

        public class ActorCommonDataHelper 
        {
            public int ID { get; set; }
            public System.Windows.Point Position { get; set; }
            public int FastAttribGroupId { get; set; }
            //public System.Windows.Media.Media3D.Vector3D Vector3D { get; set; }

            public void Set(ActorCommonData acd)
            {
                Position = new System.Windows.Point(acd.x0D0_WorldPosX, acd.x0D4_WorldPosY);
                FastAttribGroupId = acd.x120_FastAttribGroupId;
            }
        }

        public class Hero
        {
            public string Name;
            public Enigma.D3.Enums.HeroClass HeroClass;
            public bool Male;  // ((Flags >> 1) & 1) != 0
            public bool Hardcore;
            public bool Season;
        }

        [ImplementPropertyChanged]
        public class GameStates
        {
            [DoNotNotify]
            public bool InGame { get; set; }
            [DoNotNotify]
            public bool IsPaused { get; set; }
            [DoNotNotify]
            public bool UIPaused { get; set; }
            [DoNotNotify]
            public bool LoadingArea { get; set; }
            public bool PlayerLoaded { get; set; }
            [DoNotNotify]
            public int ObjectManagerFrame { get; set; }
            [DoNotNotify]
            public bool ObjMngrStoped { get; set; }
            [DoNotNotify]
            public int _firstFreeAcd { get; set; }
            [DoNotNotify]
            public byte[] ACDBuffer = new byte[0];

            public GameStates() { }
            public GameStates(GameStates e){
                InGame = e.InGame;
                UIPaused = e.UIPaused;
                ObjectManagerFrame = e.ObjectManagerFrame;
                ObjMngrStoped = e.ObjMngrStoped;
                PlayerLoaded = e.PlayerLoaded;
            }
        }

        public class LevelAreaList 
        {
            public long Timer { get; set; }
            public bool GoblinFound { get; set; }
            public Experience ExperienceData { get; set; }
            public HashSet<int> Items { get; set; }
            //  Placeholder for scenes
            //public List<Helpers.IMap> Monsters { get; set; }
            public struct Experience
            {
                public ulong XPGained { get; set; }
            }
        }
    }

    public class GameManagerData
    {
        public FKAccounts.Multibox Multibox { get; set; }
        public InGameData GameData {get;set;}
        public Dictionary<int, AreaList> AreaLevels { get; set; }

        public Dictionary<int, Rifts> RiftManager { get; set; } // GR and Rifts

        public FKStats.Items.FKItem.ItemBase ItemHelper = new FKStats.Items.FKItem.ItemBase(); // Stores all drops until flushed when leaving game / quiting FK

        public class InGameData
        {
            public ulong PlayTime { get; set; }
            public ExperienceData Experience { get; set; }
            public Stash StashItems { get; set; }

            public enum InventoryItems : int
            {
                Reusable_Parts = 361984,
                Arcane_Dust = 361985,
                Death_Breath = 361989,
                Veiled_Crystal = 361986,
                Forgotten_Soul = 361988,
                Khanduran_Rune = 365020,
                Caldeum_Nightshade = 364281,
                Westmarch_Holy_Water = 364975,
                Arreat_War_Tapestry = 364290,
                Corrupted_Angel_Flesh = 364305,
                Tired_Loot_Key = 408416
            }

            public class Stash
            {
                public bool Loaded = false;
                public Dictionary<int, StashItem> _Items;
                private void InitDictionary()
                {
                    _Items = new Dictionary<int, StashItem>();
                    foreach (InventoryItems item in Enum.GetValues(typeof(InventoryItems)))
                        _Items.Add((int)item, new StashItem { Name = item.ToString() });
                }

                public Dictionary<int, StashItem> Items { get { if (_Items == null) InitDictionary(); return _Items; } }
                    
                   /* new Dictionary<int, StashItem>(){ () =>
                    foreach(var item in Enum.GetValues(typeof(InventoryItems)))
                        
                    /*
                    
                    Enum.Ge

                    {189860, new StashItem { Name = "Common Debris"}},
                    {361984, new StashItem { Name = "Reusable Parts"}},
                    {361985, new StashItem { Name = "Arcane Dust"}},
                    {-1, new StashItem { Name = "Exquiste Essence"}},
                    {361989, new StashItem { Name = "Death Breath"}},
                    {283101, new StashItem { Name = "Demonic Essence"}},
                    {189862, new StashItem { Name = "Iridescent Tear"}}, 
                    {361986, new StashItem { Name = "Veiled Crystal"}},
                    {189863, new StashItem { Name = "Fiery Brimstone"}},
                    {361988, new StashItem { Name = "Forgotten Soul"}},

                    {365020 , new StashItem { Name = "Khanduran Rune"}},
                    {364281, new StashItem { Name = "Caldeum Nightshade"}},
                    {364975, new StashItem { Name = "Westmarch Holy Water"}},
                    {364290, new StashItem { Name = "Arreat War Tapestry"}},
                    {364305, new StashItem { Name = "Corrupted Angel Flesh"}},
                    {408416, new StashItem { Name = "Tired Loot Key"}},*/

                public void ResetList()
                {
                    foreach (StashItem Item in Items.Values)
                        Item.Quantity = 0;
                }

                public class StashItem
                {
                    public int SnoId;
                    public ushort Quantity;
                    public string Name;
                }
            }

            public class ExperienceData
            {
                public ulong XPGained { get; set; }
                public ulong XPNeeded { get; set; }
            }
        }

        public class Rifts : AreaList
        {
            public int OwnerID { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public Dictionary<int, AreaList> DisposingLevels { get; set; } // Contains all rift levels and its data
        }

        public class AreaList
        {
            public string LevelArea { get; set; }
            public int LevelAreaSNO { get; set; }
            public GameType GameType { get; set; }
            public int GoblinsFound { get; set; }
            public long Timer { get; set; }
            public HashSet<int> Items { get; set; }
            public GameManagerAccounts.LevelAreaList.Experience ExperienceData { get; set; }
        }

        public enum GameType
        {
            OpenWorld,
            NephalemRift,
            GreaterRift
        }
    }

    public static class GameManagerExtender
    {
        public static GameManagerAccounts Match(this GameManagerAccounts acc)
        {
            return GameManager.Instance.Accounts[acc.GameMangerDataID];
        }

        public static GameManagerData GameData(this GameManagerAccounts acc)
        {
            return GameManager.Instance.GameManagerData[acc.GameMangerDataID];
        }

        public static GameManagerAccounts MainAccount(this List<GameManagerAccounts> List)
        {
            foreach (GameManagerAccounts Key in List)
                if (Key.GameData().Multibox.MainAccount)
                    return Key;

            return List.FirstOrDefault();
        }

        public static IEnumerable<GameManagerAccounts> NotMainAccount(this List<GameManagerAccounts> List)
        {
            return List.Where(x => !x.MainAccount);
        }

        public static void UpdateNewArea(this GameManagerAccounts.LevelAreaList list, int LevelArea, GameManagerAccounts Account)
        {
            if(Account.GameData().AreaLevels.ContainsKey(LevelArea)) // Current Area already exists, update its information
            {               
                GameManagerData.AreaList AreaList = Account.GameData().AreaLevels[LevelArea];

                list.Timer = AreaList.Timer;
                list.ExperienceData = AreaList.ExperienceData;
                list.Items = AreaList.Items;
            }

            else
            {
                list.Timer = 0;
                //list.Monsters = new List<Helpers.IMap.IMapActor>{ };
                list.Items = new HashSet<int>();
                list.ExperienceData = new GameManagerAccounts.LevelAreaList.Experience();
            }
        }

        public static void UpdateArea(this GameManagerAccounts.LevelAreaList list, int LevelArea, GameManagerAccounts Account)
        {
            // Send Data from Current Area to Previous Area

            if(Account.GameData().AreaLevels.ContainsKey(LevelArea)) // Current Area already exists, update its information
            {
                GameManagerData.AreaList AreaList = Account.GameData().AreaLevels[LevelArea];

                AreaList.ExperienceData = new GameManagerAccounts.LevelAreaList.Experience
                {
                     XPGained = AreaList.ExperienceData.XPGained + list.ExperienceData.XPGained
                };

                HashSet<int> NewList = AreaList.Items;
                NewList.UnionWith(list.Items);
                
                AreaList.Items = NewList;
                AreaList.Timer = list.Timer;
            }

            else // New area, not been here before
            {
                Account.GameData().AreaLevels.Add(LevelArea, new GameManagerData.AreaList
                {
                    Items = list.Items,
                    Timer = list.Timer,
                   
                    ExperienceData = new GameManagerAccounts.LevelAreaList.Experience
                    {
                        XPGained = list.ExperienceData.XPGained
                    },
                });
            }
        }

       /* public static Dictionary<short, GameManagerAccounts> NotMain(this Dictionary<short, GameManagerAccounts> List)
        {
            //return List.Where(x => !x.Value.GameData().Multibox.MainAccount).ToDictionary<short, GameManagerAccounts>();
        }*/
    }


}
