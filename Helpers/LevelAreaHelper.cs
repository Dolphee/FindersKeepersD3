using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using Enigma.D3;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Controller.Enums;
using Enigma.D3.Helpers;

namespace FindersKeepers.Helpers
{
    public class LevelAreaHelper
    {
        public GameManagerAccounts Account { get { return GameManagerAccountHelper.Current; }}
        public bool EliteAffixesEnabled = false;
        public Difficulty GameDifficulty = Difficulty.Normal;

        public bool State()
        {
            /*
             Localdata.Local is created AFTER
             Localdata.Ingame !!!
             */

            GameManagerAccounts.GameStates State = new GameManagerAccounts.GameStates(Account.Gamestate);

            if (!PlayerLoaded(State) || LoadingArea() || !IsObjectManagerOnNewFrame())
                return false;

            if (Account.MainAccount)
            {
                if (!State.PlayerLoaded && Account.Gamestate.PlayerLoaded) // New Game
                    NewGame();
            }

            return true;
        }

        public void NewGame()
        {
            //Extensions.Execute.UIThread(() => System.Windows.MessageBox.Show("MM"));

            ResetSkillbar();

            GameManagerAccountHelper.Current.Player = ActorCommonData.Container.GetPlayer();
            GameDifficulty = (Difficulty)Account.DiabloIII.ObjectManager.Storage.x004;

            Account.HeroHelper = new GameManagerAccounts.Hero
            {
                HeroClass = HeroReader.GetHeroClass,
                Name = HeroReader.GetHeroName.ToString()
            };

          
          
            // x.OnJoin();

           /* System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() => {
                if (IGameStatuses != null)
                    foreach (var x in IGameStatuses)
                        x.OnJoin();
            }));*/
        }

        public void WasIngame()
        {
            if (Account.MainAccount)
            {
                GameManager.Instance.GManager.GList.MainAccount.Player = null;

                if (GameManager.Instance.GManager.GRef.D3OverlayControl.MainBarHandler.ListView.Search<IGameStatus>(typeof(IGameStatus)) != null)
                    GameManager.Instance.GManager.GRef.D3OverlayControl.MainBarHandler.ListView.Search<IGameStatus>(typeof(IGameStatus)).ToList().ForEach(x => x.OnExit());
            }

            Account.GameData().AreaLevels = new Dictionary<int, GameManagerData.AreaList>();
            Account.LevelAreaContainer.Items = new HashSet<int>();
        }

        public void ResetSkillbar()
        {
            System.Threading.Thread f = new System.Threading.Thread(() =>
            {
                Enigma.D3.Engine.Current = GameManager.Instance.GManager.GList.MainAccount.DiabloIII;
                System.Threading.Thread.Sleep(1000);

                UIObjects.ResetPointer();
            });

            f.Start();
        }

        public bool PlayerLoaded(GameManagerAccounts.GameStates State)
        {
            Account.Gamestate.PlayerLoaded = Account.DiabloIII.LocalData.x00_IsActorCreated == 1;

            if (State.PlayerLoaded && !Account.Gamestate.PlayerLoaded)
                WasIngame();

            return Account.Gamestate.PlayerLoaded;
        }

        public bool LoadingArea()
        {
            LoadArea();

            Account.Gamestate.LoadingArea = Account.DiabloIII.ObjectManager.x790.Dereference().x040_Flag != 0;
            return Account.Gamestate.LoadingArea;
        }

        public void LoadArea()
        {
            if ((Account.DiabloIII.LevelArea != null)) // Leaving to menu
            {
                if (Account.DiabloIII.LevelArea.x044_SnoId != Account.LevelArea) // Throws exception!
                {
                    Account.LevelArea = Account.DiabloIII.LevelArea.x044_SnoId;
                    //Account.ActorList.Items.Clear();

                    if (Account.MainAccount)
                    {
                        if (GameManager.Instance.GManager.GCache.MapReveal)
                        {
                            SceneHelper.LoadedScenes.Clear();
                            SceneHelper.NavHelpers.Clear();
                            SceneHelper.NewScenes.Clear();
                            SceneHelper.ForceRefresh = true;

                           if (Config.Get<FKConfig>().General.MiniMapSettings.Enabled)
                                GameManager.Instance.GManager.GRef.D3OverlayControl.Search<Templates.MinimapNotifyObjects.MinimapNotify>().Empty();

                            if (Config.Get<FKConfig>().General.MiniMapSettings.LargeMap)
                               GameManager.Instance.GManager.GRef.D3OverlayControl.Search<Templates.MinimapNotifyObjects.LargeMapNotify>().Empty();
                        }

                        //GameManager.Instance.GManager.GRef.Attacher.SearchAndDestroy("ElitePack");
                        TownHelper.InTown = TownHelper.Towns.Contains(Account.LevelArea);
                    }

                    GetRifts(); // In Rift?

                    if(!Account.RiftHelper.InRiftNow && Config.Get<FKAffixes>().Settings.Enabled)
                            EliteAffixesEnabled = true;

                    int Area = Account.DiabloIII.LevelArea.x044_SnoId;
                    Account.LevelAreaContainer.UpdateArea(Account.LevelArea, Account); // Old area (OLD SNO ID)
                    Account.LevelAreaContainer.UpdateNewArea(Area, Account); // New area (SNO ID)
                    Account.LevelArea = Account.DiabloIII.LevelArea.x044_SnoId;
                }
            }
        }

        public void GetRifts()
        {
            if (Rift.RiftLevels.Contains((uint)Account.DiabloIII.LevelArea.x044_SnoId)) // Is in rift!
                Account.RiftHelper.InRiftNow = true;

            else
                Account.RiftHelper.InRiftNow = false;

            if (Account.MainAccount)
            {
                //GameManager.Instance.GManager.GRef.Attacher.Add<Templates.Rifts.GreaterRift>("GreaterRift");

                Enigma.D3.Collections.LinkedList<Quest> L = Account.DiabloIII.ObjectManager.Storage.Quests.Dereference().x001C_Quests;
                Quest Quest = L.Where(x => (x.x000_QuestSnoId == (int)RiftType.NephalemRift) && x.x01C_QuestStep != -1).Select(x => x).FirstOrDefault();

                if (Quest != null) // At least one is active
                {
                    if (Account.RiftHelper.CurrentRift == null)
                        Account.RiftHelper.StartRift(Quest);

                    if (Account.RiftHelper.InRiftNow)
                    {
                        if (Account.RiftHelper.CurrentRift.RiftLevel != -1)
                        {
                            if (Config.Get<FKAffixes>().Settings.Enabled)
                            {
                                if (Config.Get<FKAffixes>().Settings.InGreaterRifts &&  Account.RiftHelper.CurrentRift.RiftLevel >= Config.Get<FKAffixes>().Settings.GreaterRiftCondition)
                                    EliteAffixesEnabled = true;
                                else
                                    EliteAffixesEnabled = false;
                            }

                            else
                            {
                                EliteAffixesEnabled = false;
                            }

                            GameManager.Instance.GManager.GRef.D3OverlayControl.CreateControl<Templates.Rifts.GreaterRift>("GreaterRift");
                        }
                        else
                        {
                            if (Config.Get<FKAffixes>().Settings.Enabled)
                            {
                                if (Config.Get<FKAffixes>().Settings.InRift && (int)GameManagerAccountHelper.Current.LevelAreaHelper.GameDifficulty >= (int)Config.Get<FKAffixes>().Settings.RiftCondition)
                                    EliteAffixesEnabled = true;
                                else
                                    EliteAffixesEnabled = false;
                            }
                            else
                            {
                                EliteAffixesEnabled = false;
                            }

                            GameManager.Instance.GManager.GRef.D3OverlayControl.CreateControl<Templates.Rifts.Rift>("Rift");
                        }
                    }
                }
            }
        }

        public bool IsObjectManagerOnNewFrame()
        {
            int CurrentFrame = Account.DiabloIII.ObjectManager.x038_Counter_CurrentFrame;

            if (CurrentFrame == Account.Gamestate.ObjectManagerFrame)
            {
                Account.Gamestate.ObjMngrStoped = true;
                return false;
            }

            if (CurrentFrame < Account.Gamestate.ObjectManagerFrame)
            {
                Account.Gamestate.ObjectManagerFrame = CurrentFrame;
                Account.Gamestate.ObjMngrStoped = true;
                Account.Gamestate.InGame = false;
                return false;
            }

            Account.Gamestate.ObjectManagerFrame = CurrentFrame;
            Account.Gamestate.ObjMngrStoped = false;
            Account.Gamestate.InGame = true;
            return true;
        }

        public void InGame()
        {
            bool InGame = Account.DiabloIII.LocalData.x04_IsNotInGame == 0;

             if (Account.Gamestate.InGame != InGame)
                WasIngame();

            Account.Gamestate.InGame = InGame;
        }
    }
}
