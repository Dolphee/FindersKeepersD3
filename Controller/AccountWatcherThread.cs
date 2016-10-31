using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FindersKeepers.Templates;
using FindersKeepers.Helpers;
using System.Diagnostics;
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3.Helpers;
using FindersKeepers.Templates.Overlay;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Controller
{
    public class AccountWatcherThread : IDisposable
    {
        public AutoResetEvent WaitHandle = new AutoResetEvent(false);
        public GameManagerData.GameManagerAccounts Account;
        public bool ExitFlag;
        public Thread MainThread;
        public object TimerLock = new object();
        public object ActionLock = new object();
        private Timer Timer;
        private List<Action> Action;

        public AccountWatcherThread(GameManagerData.GameManagerAccounts Acc, List<Action> T)
        {
            Account = Acc;
            Action = T;
            CreateThread();
            MainThread.Start();
   
            Timer = new Timer(Update, null, Timeout.Infinite, GameManager.Instance.GManager.GCache.PollRate);
            StartTimer();
        }

        public void Update(object State)
        {
            if (Monitor.TryEnter(TimerLock))
            {
                try
                {
                    WaitHandle.Set();
                }
                finally
                {
                    Monitor.Exit(TimerLock);
                }
            }
        }

        public void StartTimer() {
            Timer.Change(0, GameManager.Instance.GManager.GCache.PollRate);
        }

        public void StopTimer()
        {
            if (Timer == null)
                return;

            Timer.Change(Timeout.Infinite, GameManager.Instance.GManager.GCache.PollRate);
            Timer.Dispose();
            Timer = null;
        }

        public void Suspend(bool UserCall = false)
        {
            if (Account.MainAccount)
            {
                GameManager.Instance.GManager.GRef.D3Overlay.Delete();
                GameManager.Instance.GManager.GRef.D3Overlay = null;
                //GameManager.Instance.GManager.GRef.Attacher.Delete();
                GameManager.Instance.GManager.GRef.Attacher = null;
            }

            StopTimer();

            if (UserCall)
            {
                WaitHandle.Close();

                if (!MainThread.IsAlive)
                    return;

                MainThread.Abort();
            }

            GameManager.Instance.ProcessHasExit(this, UserCall);
            this.Dispose();
        }

        public void CreateThread()
        {
            try
            {
                MainThread = new Thread(() =>
                {
                    try
                    {
                        Enigma.D3.Engine.Current = Account.DiabloIII;
                        GameManagerAccountHelper.Current = Account;
                        GameManagerAccountHelper.CurrentData = Account.GameData();
                        GameManagerAccountHelper.Current.LevelAreaHelper = new LevelAreaHelper();

                        if (Account.MainAccount)
                            CreateUI();

                        do
                        {
                            lock(ActionLock)
                            {
                                foreach (Action i in Action)
                                    i.Invoke();
                            }
                            WaitHandle.WaitOne();
                        }

                        while (!Account.DiabloIII.Process.HasExited && !ExitFlag);
                        Suspend();
                    }
                    catch (Exception f)
                    {
                        DebugWriter.Write(f);
                    }
                });

                MainThread.IsBackground = true;
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }
        }

        public void CreateUI()
        {
            Extensions.Execute.UIThread(() =>
            {
                if (Config.Get<FKConfig>().General.FKSettings.UseOverlayInGame)
                {
                    if (Account.DiabloIII.VideoPreferences.x0C_DisplayMode.x04_WindowMode == Enigma.D3.Graphics.WindowMode.Fullscreen)
                    {
                        System.Windows.MessageBox.Show("FindersKeepersD3 is not supported in Fullscreen.\nPlease run Diablo III in Window mode or Window Fullscreen.");
                        return;
                    }

                    Extensions.TryInvoke(() =>
                    {
                        LoadDefaultFeatures();
                        GameManager.Instance.GManager.GRef.D3Overlay = D3Overlay.Create(Account.DiabloIII.Process);
                        GameManager.Instance.GManager.GRef.D3Overlay.Show();

                        /*GameManager.Instance.GManager.GRef.Attacher = new Attach(GameManager.Instance.GManager.GRef.D3Overlay);
                         GameManager.Instance.GManager.GRef.Attacher = new Attach(GameManager.Instance.GManager.GRef.D3Overlay);
                        Action.Add(GameManager.Instance.GManager.GRef.Attacher.SetUpdate);*/

                        GameManager.Instance.GManager.GRef.D3OverlayControl = new Templates.Overlay.Attacher<D3OverlayControl>(GameManager.Instance.GManager.GRef.D3Overlay);
                        GameManager.Instance.GManager.GRef.D3OverlayControl.Initialize();

                        Action.Add(GameManager.Instance.GManager.GRef.Actors.Enumerate);
                        Action.Add(GameManager.Instance.GManager.GRef.D3OverlayControl.TryUpdate);

                        PInvokers.SetForegroundWindow(Account.DiabloIII.Process.MainWindowHandle);

                        Enigma.D3.Engine.Current = Account.DiabloIII;
                        GameManagerAccountHelper.Current = Account;


                   
                        //Debug.GetDDump.AttributesList();
                    });

                    Action.Add(() =>
                    {
                        if (GameManagerAccountHelper.Current.Player != null)
                            GameManagerAccountHelper.Current.Player.FreeSnapshot();
                    });
                }
            });
        }

        public void LoadDefaultFeatures()
        {
            if (Config.Get<FKConfig>().General.DamageNumber.Enabled && Config.Get<FKConfig>().General.Experience.Enabled)
                GameManager.Instance.GManager.GRef.Actors.Features.Add(() => GameManager.Instance.GManager.GRef.Actors.DamangeNumbers.Update());

            if (Config.Get<FKAffixes>().Settings.Enabled)
                GameManager.Instance.GManager.GRef.Actors.Features.Add(() => Affix.NewLoop());

            if (Config.Get<FKConfig>().General.Skills.Skill)
            {
                GameManager.Instance.GManager.GRef.Actors.Features.Add(() =>
                {
                    if (UIObjects.SkillsBar.Cache & !UIObjects.SkillsBar.TryGetValue<Enigma.D3.UI.Controls.UXControl>()) // Skills updated
                        GameManagerAccountHelper.Current.LevelAreaHelper.ResetSkillbar();
                });
            }

            if ((Config.Get<FKConfig>().General.MiniMapSettings.RevealMapLargemap.Reveal || Config.Get<FKConfig>().General.MiniMapSettings.RevealMapMinimap.Reveal))
            {
                GameManager.Instance.GManager.GRef.Actors.Features.Add(() =>
                {
                    SceneHelper.LoadScenes();
                    MapItems.Scene();
                });
            }

            GameManager.Instance.GManager.GRef.Actors.Features.Add(() =>
            {
                if (TownHelper.InTown)
                {
                    TownHelper.GetStash();
                    TownHelper.Gambling();
                }
            });
        }

        public void Dispose()
        {
            StopTimer();
        }
    }
}
