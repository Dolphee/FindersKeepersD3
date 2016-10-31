using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3.Helpers;
using FindersKeepers.Helpers;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Controller.Enums
{
    public class Rift : IDisposable
    {
        public static HashSet<uint> RiftLevels = new HashSet<uint>(((uint[])Enum.GetValues(typeof(Rifts))).ToList());
        public Enigma.D3.Quest RiftQuest;
        public RiftStats CurrentRift;
        public System.Threading.Timer Timer;
        public GameManagerAccounts Account; 
        public bool RiftIsActive = false;
        public bool InRiftNow = false;
        public bool InRift(int Area) { return RiftLevels.Contains((uint)Area); }
        public bool IsVisible = false;
        public RiftType RiftType;

        //  public static List<RiftStats> DisposedRifts = new List<RiftStats>();
        // public static RiftType RiftType;

        public void StartRift(Enigma.D3.Quest Quest)
        {
            RiftType = (RiftType)Quest.x000_QuestSnoId;
            RiftIsActive = true;

            CurrentRift = new Rift.RiftStats
            {
                StartTick = Quest.x004_CreationTick,
                Started = DateTime.Now.AddSeconds(-((GameManager.Instance.GameTicks - Quest.x004_CreationTick) / 60)),
                RiftType = this.RiftType,
                RiftLevel = -1,
                Legendary = new List<int>(),
                Levels = 1,
                Boss = -1,
                Difficulty = (Difficulty)GameManagerAccountHelper.Current.DiabloIII.ObjectManager.Storage.x004,
            };

            if (GameManagerAccountHelper.Current.DiabloIII.ObjectManager.Storage._x05C != -1) // GR
            {
                CurrentRift.RiftLevel = (GameManagerAccountHelper.Current.DiabloIII.ObjectManager.Storage._x05C + 1);
                CurrentRift.Difficulty = Difficulty.GreaterRift;

                TownHelper.UpdateStash();

                //RiftType = RiftType.GreaterRift;
            }

            Account = GameManagerAccountHelper.Current;
            SetTimer();
        }

        public void CloseRift()
        {
            CurrentRift = null;
            RiftIsActive = false;
            StopTimer();

            try
            {
                foreach (var x in GameManager.Instance.Accounts)
                {
                    foreach (int Key in RiftLevels)
                    {
                        if (x.GameData().AreaLevels.ContainsKey(Key))
                            x.GameData().AreaLevels.Remove(Key);
                    }

                   //  x.LevelAreaContainer. Check if Area == Rift.Level
                }
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
        }

        public Enigma.D3.Quest GetQuest()
        {
            if (Account.DiabloIII.ObjectManager.Storage.Quests.Dereference() == null)
                return null;

            return Account.DiabloIII.ObjectManager.Storage.Quests.Dereference().x001C_Quests.Where(x => x.x000_QuestSnoId == (int)RiftType).FirstOrDefault();
        }

        private int PreviousStep = 0;
        public void Update(object state)
        {
            Enigma.D3.Quest Quest = GetQuest();

            if (Quest != null)
            {
                if (CurrentRift.Difficulty == Difficulty.GreaterRift)
                {
                    CurrentRift.State = (GreaterRiftState)Quest.x01C_QuestStep;
                    CurrentRift.Diff = (GameManager.Instance.GameTicks - CurrentRift.StartTick) / 60d;

                    /*if(true)
                    {
                        if (!UIObjects.GemUpgrade.Cache & UIObjects.GemUpgrade.TryGetValue<Enigma.D3.UI.Controls.UXItemsControl>())
                            GameManager.Instance.GManager.GRef.Attacher.Add<Templates.Rifts.GemUpgrade>("GemUpgrade");
                    }*/

                    if (Quest.x01C_QuestStep == (int)GreaterRiftState.NotStarted) // Finished
                        CloseRift();

                    else if (Quest.x01C_QuestStep == (int)GreaterRiftState.KillBoss && PreviousStep != Quest.x01C_QuestStep)
                        CurrentRift.BossSpawned = (GameManager.Instance.GameTicks - CurrentRift.StartTick) / 60d;

                 //   else if (Quest.x01C_QuestStep == (int)GreaterRiftState.KillBoss && CurrentRift.Boss == -1)
                   //     ActorCommonDataHelper.Enumerate().Where(x => x.x0B8_MonsterQuality == Enigma.D3.Enums.MonsterQuality.Boss).
                
                    else if (Quest.x01C_QuestStep == (int)GreaterRiftState.KilledBoss && PreviousStep != Quest.x01C_QuestStep)
                        CurrentRift.BossKilled = (GameManager.Instance.GameTicks - CurrentRift.StartTick) / 60d;
                }

                else
                {
                   //CurrentRift.State = (RiftState)Quest.x01C_QuestStep;
                    CurrentRift.Diff = (GameManager.Instance.GameTicks - CurrentRift.StartTick) / 60d;

                    if (Quest.x01C_QuestStep == (int)RiftState.NotStarted) // Finnished
                        CloseRift();

                    else if (Quest.x01C_QuestStep == (int)RiftState.KillBoss && PreviousStep != Quest.x01C_QuestStep)
                        CurrentRift.BossSpawned = (GameManager.Instance.GameTicks - CurrentRift.StartTick) / 60d;

                    else if (Quest.x01C_QuestStep == (int)RiftState.KilledBoss && PreviousStep != Quest.x01C_QuestStep)
                        CurrentRift.BossKilled = (GameManager.Instance.GameTicks - CurrentRift.StartTick) / 60d;
                }

                PreviousStep = Quest.x01C_QuestStep;
            }

            else
            {
                PreviousStep = 0;
                CloseRift();
            }
        }

        public void SetTimer()
        {
            if(Timer == null)
               Timer = new System.Threading.Timer(Update, null, System.Threading.Timeout.Infinite, 1000);

            Timer.Change(0, 1000);
        }

        public void StopTimer()
        {
            try
            {
                if (Timer == null)
                    return;

                Timer.Change(System.Threading.Timeout.Infinite, 1000);
            }

            catch (Exception e)
            {
                Extensions.Execute.UIThread(() =>
                {
                    DebugWriter.Write(e);
                });
            }
        }

        public void Dispose()
        {
            StopTimer();
            if (Timer != null)
                Timer.Dispose();
        }


        [Serializable]
        public class RiftStats
        {
            [XmlAttribute]
            public int StartTick { get; set; }

            [XmlAttribute]
            public GreaterRiftState State { get; set; }

            [XmlAttribute]
            public double Diff { get; set; }
            [XmlAttribute]
            public RiftType RiftType {get;set;}
            [XmlAttribute]
            public DateTime Started { get; set; }
            [XmlAttribute]
            public DateTime Ended { get; set; }
            [XmlAttribute]
            public DateTime RealTime { get; set; }
            [XmlAttribute]
            public Difficulty Difficulty { get; set; }
            [XmlAttribute]
            public double BossSpawned { get; set; }
            [XmlAttribute]
            public double BossKilled { get; set; }
            [XmlAttribute]
            public int Boss { get; set; }
            [XmlAttribute]
            public List<int> Legendary { get; set; }
            [XmlAttribute]
            public uint Levels { get; set; }// number of levels
            [XmlAttribute]
            public int RiftLevel { get; set; } 
            [XmlAttribute]
            public bool GoblinRift { get; set; }
            [XmlAttribute]
            public ulong XPGained { get; set; }
        }
    }

    [Serializable]
    public enum RiftType : int
    {
        NephalemRift = 337492,
        GreaterRift = 382695
    }

    public enum RiftState : int // Not checked
    {
        NotStarted = -1, // QuestStep = -1, rest QS = 1
        Started = 1,
        KillBoss = 3,
        KilledBoss = 10,
        ClosingRift = 5 // QuestStep  = 2
    }
    public enum GreaterRiftState : int // Not checked
    {
        NotStarted = -1,
        Started = 13, // QuestStep = -1, rest QS = 1
        KillBoss = 16,
        KilledBoss = 34, // Has Not gamled
        HasGambled = 10, // Not closed
        Closing = 5
    }

    [Serializable]
    public enum Difficulty : uint
    {
        Normal = 0,
        Hard,
        Master,
        Expert,
        Torment_I,
        Torment_II,
        Torment_III,
        Torment_IV,
        Torment_V,
        Torment_VI,
        Torment_VII,
        Torment_VIII,
        Torment_IX,
        Torment_X,
        Torment_XI,
        Torment_XII,
        Torment_XIII,
        GreaterRift = 35
    }

    public enum Rifts : uint
    {
        X1_LR_Level_01 = 288482,
        X1_LR_Level_02 = 288684,
	    X1_LR_Level_03 = 288686,
	    X1_LR_Level_04 = 288797,
        X1_LR_Level_05 = 288799,
        X1_LR_Level_06 = 288801,
        X1_LR_Level_07 = 288803,
        X1_LR_Level_08 = 288809,
        X1_LR_Level_09 = 288812,
        X1_LR_Level_10 = 288813
    }
}
