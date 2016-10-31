using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using Enigma.D3.Enums;
using Enigma.D3.Helpers;
using FindersKeepers.Helpers;
using FindersKeepers.Controller;
using FindersKeepers.Controller.Enums;
using System.Diagnostics;
using FindersKeepers.Controller.GameManagerData;
using System.Media;
using Enigma.D3.Collections;
using Enigma.D3.UI.Controls;
using FindersKeepers.Helpers.Overlay.ActorTypes;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Helpers
{
    public class Actors
    {
        public bool OnHault = false;
        public bool ActorsCreated = false;
        public bool Paused = false;
        public bool PrevPaused = false;
        public bool ChangeingArea = false;
        public bool PrevChangedArea = false;
        public System.Windows.Threading.DispatcherTimer Timer;
        public bool EnumerateStash = false;
        public bool StashOpen = false;
        public bool UseMiniMap = true;
        public bool IsinGame = false;
        public bool Break = false;
        private readonly object TimerLock = new object();
        public DamageNumbers DamangeNumbers = new DamageNumbers();
        public List<Action> TownActions = new List<Action>();
        public List<Action> Features = new List<Action>();


        public bool x = false;
        public void Enumerate() // Main Account
        {
            try
            {
                if (!GameManagerAccountHelper.Current.LevelAreaHelper.State())
                    return;

                GameManagerAccountHelper.Current.Player.TakeSnapshot();

                foreach (Action e in Features)
                    e.Invoke();

                var ACDContainer = ActorCommonData.Container;
                ACDContainer.TakeSnapshot();

                GameManagerAccountHelper.Current.Player.TakeSnapshot();

                foreach (var item in ACDContainer.GetBufferDump(ref GameManagerAccountHelper.Current.Gamestate.ACDBuffer))
                {
                    var Actor = item.Create();

                    if (Actor.x000_Id == -1)
                        continue;

                    Actor.DetermineActor();

                    if ((Actor.x184_ActorType == ActorType.Item) && (int)Actor.x114_ItemLocation == -1) // -1 == Ground items //  && Actor.x090_ActorSnoId != -1 && Actor.x27C_WorldId != 0 && Actor.x0D0_WorldPosX != 0 && (Actor.x0B0_GameBalanceType == GameBalanceType.Items && 
                        ItemFilter.Enumerate(Actor);
                }

                ACDContainer.FreeSnapshot();
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
        }


        public void EnumerateMultibox()
        {
            try
                {
                    if (!GameManagerAccountHelper.Current.LevelAreaHelper.State())
                        return;

                    var ACDContainer = ActorCommonData.Container;
                    ACDContainer.TakeSnapshot();

                    foreach (var item in ACDContainer.GetBufferDump(ref GameManagerAccountHelper.Current.Gamestate.ACDBuffer))
                    {
                        var Actor = item.Create();

                        if (Actor.x000_Id == -1)
                            continue;

                        if ((Actor.x184_ActorType == ActorType.Item) && (int)Actor.x114_ItemLocation == -1) // -1 == Ground items //  && Actor.x090_ActorSnoId != -1 && Actor.x27C_WorldId != 0 && Actor.x0D0_WorldPosX != 0 && (Actor.x0B0_GameBalanceType == GameBalanceType.Items && 
                            ItemFilter.Enumerate(Actor);
                    }

                    ACDContainer.FreeSnapshot();
                }

                catch (Exception e)
                {
                    DebugWriter.Write(e);
                }
        }

        public void LoadTimer()
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += (object state, EventArgs e) =>
            {
                GameManager.Instance.GManager.GList.MainAccountData.GameData.PlayTime += 1;
               // GameManager.MainAccount.ActorList.Timer += 1;
            };
            Timer.IsEnabled = true;
            Timer.Start();
        }
    }

    public static class ActorHelpers
    {
        public static ExpandableContainer<Enigma.D3.ActorCommonData> GetContainer(this Engine List)
        {
            return List.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData;
        }

        public static ActorCommonData GetACDData(this Actor Actor)
        {
            return (ActorCommonData)Engine.Current.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData[(short)Actor.x088_AcdId];
        }

        public static bool IsMonster(this ActorCommonData Actor)
        {
            return (Actor.x188_Hitpoints > 0.00001 &&
                (Actor.x198_Flags_Is_Trail_Proxy_Etc & 1) == 0 &&
                Actor.x190_TeamId == 10);
        }

        public static bool IsItem(this ActorCommonData Actor)
        {
            return Actor.x184_ActorType == Enigma.D3.Enums.ActorType.Item;
        }
    }
}
