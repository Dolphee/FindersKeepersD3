using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3;
using System.Threading;
using System.Windows;
using FindersKeepers.Templates;
using FindersKeepers.Helpers;
using System.Diagnostics;
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3.Helpers;

namespace FindersKeepers.Controller
{
    public class GameManager
    {
        public static GameManager Instance;
        public GManagers GManager = new GManagers();

        public Dictionary<short, GameManagerData.GameManagerData> GameManagerData = new Dictionary<short,GameManagerData.GameManagerData>();
        public List<GameManagerAccounts> Accounts = new List<GameManagerAccounts>();
        public static List<Action> Actions = new List<Action>();
        public int GameTicks { get { return GManager.GList.MainAccount.DiabloIII.ObjectManager.x790.Dereference()._x008; } }

        public void GameManagerStart()
        {
            try
            {
                GManager.GList.MainAccount = Accounts.MainAccount();
                GManager.GList.MainAccountData = Accounts.MainAccount().GameData();
                GManager.GCache.MapReveal = (Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Reveal || Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Reveal);
                GManager.GRef.Actors = new Actors();
                GManager.GRef.Actors.LoadTimer();
                GManager.GCache.Multiboxing = (Accounts.Count > 1);

                if (Config.Get<FKConfig>().General.FKSettings.MinimizeToTray)
                    Templates.Templating.BasicEvents.MinimizeBase();

                if (SceneHelper.SNOScenes.Count == 0)
                {
                    Helpers.SNO.ItemInformation();
                    SceneHelper.Load();
                }

                Accounts = Accounts.OrderBy(x => x.MainAccount).ToList();
                CreateThread();
            
            }

            catch (Exception e)
            {
                Extensions.Execute.UIThread(() =>
                MessageBox.Show(e.ToString()));
            }
        }

        public void CreateThread()
        {
            lock(new object())
            {
                try
                {
                    new Thread(() =>
                    {
                        foreach (GameManagerAccounts Account in Accounts)
                        {
                            List<Action> GUpdate = new List<Action>();
                            if (Account.MainAccount && !Config.Get<FKConfig>().General.FKSettings.UseOverlayInGame)
                                GUpdate.Add(GManager.GRef.Actors.Enumerate);
                            else if(!Account.MainAccount)
                                GUpdate.Add(GManager.GRef.Actors.EnumerateMultibox);

                            AccountWatcherThread Thread = new AccountWatcherThread(Account, GUpdate);
                            GManager.GThread.Threads.Add(Thread);
                        }

                        GManager.GThread.ExitThread.WaitOne();
        
                    }).Start();
                }

                catch(Exception e)
                {
                    Extensions.Execute.UIThread(() =>
                    MessageBox.Show(e.ToString()));
                }
            }
        }

        public void ProcessHasExit(AccountWatcherThread Thread, bool UserCall = false)
        {
            try
            {
                if (Thread.Account.DiabloIII != null)
                    Thread.Account.DiabloIII.Dispose();

                GManager.GThread.Threads.Remove(Thread);
                GManager.GThread.HasThreads = GManager.GThread.Threads.Count > 0;

                if (!GManager.GThread.HasThreads)// All has exit
                {
                    GManager.GThread.ExitThread.Set();

                        if (!UserCall) // All Diablo exited, not called by user
                        Extensions.Execute.UIThread(() => {
                            // MainWindow.Window.LoadPageIntoMain(typeof(Stop));
                            Templates.Templating.BasicEvents.TryBringBack();
                        });
                }
            }

            catch (Exception e)
            {
                Extensions.Execute.UIThread(() =>
                {
                   // MessageBox.Show(e.ToString());
                });
            }
        }
    }
}
