using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Enigma.D3;
using Enigma.Memory;
using System.Threading;
using System.Security.Principal;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for GameInit.xaml
    /// </summary>
    public partial class GameInit : UserControl
    {
        public Dictionary<int, ProcessHelper> PHelper = new Dictionary<int, ProcessHelper>();
        public DispatcherTimer D3Crawler;
        public readonly object lockobj;
        public static int count = 0;
        public RunController RunController {get;set; }

        public class IAcc
        {
            public bool Init;
            public bool Enabled;
        }

        public class ProcessHelper : IDisposable
        {
            public Process Process;
            public FKAccounts.Multibox AccountSettings;
            public Enigma.D3.Engine Engine;
            public EventHandler ExitHandler;
            public int Key;
            public Border Owner;
            public bool Loaded;
            public bool Enabled;
            public ulong HeroId;

            public void Dispose()
            {
                if(Engine != null)
                    Engine.Dispose();
                if(Process != null)
                    Process.Dispose();
            }
        }

        public GameInit(RunController p)
        {
            InitializeComponent();
            RunController = p;

            Process[] Processes = Helpers.PInvokers.GetDiabloIII();

            Loaded += ((s, e) =>
            {
                if (Processes.Count() == 0)
                    RunController.TryGet("ErrorMessage");

                return;
            });


            D3Crawler = new DispatcherTimer();
            D3Crawler.IsEnabled = true;
            D3Crawler.Interval = TimeSpan.FromSeconds(1);
            D3Crawler.Tick += Crawler;
        }

        public void Crawler(object sender, EventArgs e)
        {
            Process[] Processes = Helpers.PInvokers.GetDiabloIII();

            if (!IErrors(Processes))
                return;
          
            if(PHelper.Count > 0)
            {
                PHelper.ToList().ForEach(x =>
                {
                    if (PHelper[x.Key].Process.HasExited || PHelper[x.Key].Process == null)
                        UnRegister(x);

                        //HeroData(x.Value); // Update data
                });
            }

            Processes.ToList().ForEach(x =>
            {
                if (!PHelper.ContainsKey(x.Id))
                    AddProcess(x);
            });
        }

        public bool IErrors(Process[] p)
        {
            bool HasErrors = false;

            if(p.Count() == 0)
            {
                RunController.TryGet("ErrorMessage");
                HasErrors = true;
            }
            
            try
            {
                bool FKAsAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
                if(p.Any(x => x.GetFileVersion() == null)){ }
            }

            catch(Exception e )
            {
                D3Crawler.Stop();
                RunController.TryGet("NotAdmin");
                HasErrors = true;
            }

            if (!HasErrors && p.Any(x => x.GetFileVersion() != Enigma.D3.Engine.SupportedVersion))
            {
                D3Crawler.Stop();
                RunController.ProcessVersion = p.Single(x => x.GetFileVersion() != Enigma.D3.Engine.SupportedVersion).GetFileVersion();
                RunController.TryGet("NotSupportedVersion");
                HasErrors = true;
            }

            if (HasErrors)
            {
                if (D3Crawler != null)
                    D3Crawler.Stop();

                return false;
            }

            return true;
        }

        private void CreateEngine(ProcessHelper p)
        {
            try
            {
                Engine engine = new Engine(p.Process);
                Engine.Current = engine;
                
                p.Owner.CallUI<TextBlock>("Tag", "Account").SetUI<TextBlock>("Text", "Waiting for D3 to start...");
                p.Owner.CallUI<TextBlock>("Tag", "BattleTag").SetUI<TextBlock>("Text", "Gathering information...");
            
                while (engine == null)
                {
                    Thread.Sleep(1000);
                    engine = new Engine(p.Process);
                    Engine.Current = engine;
                }

                while (engine.ApplicationLoopCount == 0)
                {
                    Thread.Sleep(1000);
                }

                Engine.Current = engine;
                p.Engine = engine;

                p.Owner.CallUI<TextBlock>("Tag", "Account").SetUI<TextBlock>("Text", "Trying to resolve BattleTag");
                p.Owner.CallUI<TextBlock>("Tag", "BattleTag").SetUI<TextBlock>("Text", "Waiting for login into Diablo III");

                while (engine.Memory.Reader.ReadChain<int>(OffsetConversion.ScreenManager, OffsetConversion.BattleNetClient, OffsetConversion.SelectedHeroes) == 0) // Haven't loaded a hero or logged-in
                {
                    Thread.Sleep(1000);
                }

                if(Config.Get<FKAccounts>().GetAccount(HeroReader.GetBattleTag.ToString()) == null)
                {
                    p.Owner.CallUI<TextBlock>("Tag", "Account").SetUI<TextBlock>("Text", "This account has not been setup");
                    p.Owner.CallUI<TextBlock>("Tag", "Account").SetUI<TextBlock>("Foreground", Extensions.HexToBrush("#ff4444"));
                    p.Owner.CallUI<TextBlock>("Tag", "BattleTag").SetUI<TextBlock>("Text", "You need to configure it in Settings > Multibox Accounts");
                    return;
                }

                p.Owner.CallUI<TextBlock>("Tag", "Account").SetUI<TextBlock>("Text", Config.Get<FKAccounts>().GetAccount(HeroReader.GetBattleTag.ToString()).Nickname);
                p.AccountSettings = Config.Get<FKAccounts>().GetAccount(HeroReader.GetBattleTag.ToString());

                p.Loaded = true;
                p.Enabled = true;
       
                //Extensions.Execute.UIThread(()=> p.Owner.CallUI<Image>("Tag", "EnableAcc").CheckBox(p.Enabled));
                p.Owner.CallUI<Image>("Tag", "EnableAcc").SetUI<Image>("Tag", p);

                p.Owner.CallUI<TextBlock>("Tag", "BattleTag").SetUI<TextBlock>("Text", HeroReader.GetBattleTag.ToString());


                if(Config.Get<FKAccounts>().GetAccount(HeroReader.GetBattleTag.ToString()).MainAccount)
                {
                    Extensions.Execute.UIThread(() =>
                    {
                        p.Owner.CallUI<Image>("Tag", "MainAccountSel").Source = Extensions.FKImage("./app");
                        p.Owner.CallUI<Image>("Tag", "MainAccountSel").FindParent<StackPanel>().ToolTip = new TextBlock { Text = "This is set as your main account" };
                    });
                }

                HeroData(p);
            }

            catch (Exception e)
            {
                Extensions.Execute.UIThread(() => MessageBox.Show(e.ToString()));
            }
        }

        public void HeroData(ProcessHelper p)
        {
            if (!p.Loaded)
                return;

            Engine.Current = p.Engine;

            ulong id = HeroReader.GetHeroId.Low64 + HeroReader.GetHeroId.High64;

            if (p.HeroId != id)
            {
                if (HeroReader.GetSeasonal != Heroes.Season.Season)
                {
                    p.Owner.CallUI<Image>("Tag", "SeasonContainer").Parent.CastHelper<StackPanel>().SetUI<StackPanel>("Visibility", Visibility.Collapsed);
                }
                else
                {
                    p.Owner.CallUI<Image>("Tag", "SeasonContainer").Parent.CastHelper<StackPanel>().SetUI<StackPanel>("Visibility", Visibility.Visible);
                    Heroes.Arena Arena = HeroReader.GetHeroArena;

                    Extensions.Execute.UIThread(() => p.Owner.CallUI<Image>("Tag", "SeasonContainer").Parent.CastHelper<StackPanel>().ToolTip.CastHelper<TextBlock>().Text =
                        "Seasonal" + ((Arena == Heroes.Arena.Hardcore) ? " Hardcore " : " ") + "Characher");
                }

                ImageSource Image = Extensions.FKImage("./Classes/" + HeroReader.GetHeroClass.ToString().ToLower() + "_" + HeroReader.GetGender.ToString().ToLower());

                Extensions.Execute.UIThread(() => p.Owner.CallUI<Image>("Tag", "ExperienceOverlay").Source = Image);
                p.HeroId = id;
            }
        }

        private void UnRegister(KeyValuePair<int, ProcessHelper> p)
        {
            PHelper.Remove(p.Key);
            p.Value.Owner.SetUI<Border>("Visibility", Visibility.Collapsed);
        }

        private void AddProcess(Process p)
        {
            if (PHelper.Count >= 4)
                return;

            ProcessHelper Helper = new ProcessHelper();
            Helper.Process = p;
            Helper.Key = PHelper.Count +1;
            Helper.Owner = Container.CallUI<Border>("Name", "Container_" + Helper.Key);
            Helper.Owner.SetUI<Border>("Visibility", Visibility.Visible);
            Helper.Enabled = false;

            try
            {
                Thread f = new Thread(() => CreateEngine(Helper));
                f.IsBackground = true;
                f.Start();
            }

            catch(Exception e)
            {
               Extensions.Execute.UIThread(()=> MessageBox.Show(e.ToString()));
            }

            Helper.Owner.SetUI<Border>("Tag", Helper);
            PHelper.Add(p.Id, Helper);
        }

        private void RemoveHandler(ProcessHelper Helper)
        {
            Helper.Process.Exited -= Helper.ExitHandler;
            Helper.Dispose();
            PHelper.Remove(Helper.Key);
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            Extensions.TryInvoke(() => MainWindow.Window.DragMove());
        }

        private void CloseFK(object sender, MouseButtonEventArgs e)
        {
        }
        private void MiniMize(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("#eeeeee");
            ((Border)sender).BorderThickness = new Thickness(1, 0, 0, 1);
        }

        private void MiniMizeOut(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("transparent");
            ((Border)sender).BorderThickness = new Thickness(0, 0, 0, 0);
        }

        private void AttachE(object sender, MouseEventArgs e)
        {
            var Tag = new FKMethods.TagHelper
            {
                Transition =
                       new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                       {
                           Active = Extensions.HexToColor("#274b6c"),
                           Hover = Extensions.HexToColor("#274b6d"),
                           Reset = Extensions.HexToColor("#f4f4f2"),
                           FadeIn = 500,
                           FadeOut = 200
                       }
            };


            AttachText.AnimateForeground(new FKMethods.TagHelper {
                Transition = new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                {
                    Active = Extensions.HexToColor("#6f6f6f"),
                    Hover = Extensions.HexToColor("#fff"),
                    Reset = Extensions.HexToColor("#6f6f6f"),
                    ResetBorder = Extensions.HexToColor("#fff"),
                    FadeIn = 500,
                    FadeOut = 200
                }
            });

            sender.CastVisual<Border>().AnimateBackground(Tag, false, true);
        }

        private void AttachEF(object sender, MouseEventArgs e)
        {
            var Tag = new FKMethods.TagHelper
            {
                Transition =
                       new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                       {
                           Active = Extensions.HexToColor("#274b6c"),
                           Hover = Extensions.HexToColor("#274b6d"),
                           Reset = Extensions.HexToColor("#f4f4f2"),
                           ResetBorder = Extensions.HexToColor("#fff"),
                           FadeIn = 300,
                           FadeOut = 200
                       }
            };

            AttachText.AnimateForeground(new FKMethods.TagHelper
            {
                Transition = new Controller.FKMenuHelper.MenuStruct.MouseEnter.Colors
                {
                    Active = Extensions.HexToColor("#6f6f6f"),
                    Hover = Extensions.HexToColor("#fff"),
                    Reset = Extensions.HexToColor("#6f6f6f"),
                    FadeIn = 300,
                    FadeOut = 200
                }
            }, true, true);

            sender.CastVisual<Border>().AnimateBackground(Tag, true, true);
        }

        private void Enable(object sender, MouseButtonEventArgs e)
        {
            ProcessHelper p = sender.CastHelper<Image>().Tag.CastHelper<ProcessHelper>();

            if(p == null || !p.Loaded)
            {
                MessageBox.Show("This process is not attachable right now!");
                return;
            }

            p.Enabled = p.Enabled.FlipBool();
            //sender.CastHelper<Image>().CheckBox(p.Enabled);
        }

        private void AttachToD3(object sender, MouseButtonEventArgs e)
        {
            if(PHelper.Count(x => x.Value.Enabled) == 0)
            {
                MessageBox.Show("You need to select at least one account to start");
                return;
            }


            try
            {
                short i = 0;

                foreach (ProcessHelper p in PHelper.Values)
                {
                    if (!p.Enabled)
                        continue;

                    Controller.GameManager.Instance.Accounts.Add(new Controller.GameManagerData.GameManagerAccounts
                    {
                        DiabloIII = new Engine(p.Process),
                        LevelAreaContainer = new Controller.GameManagerData.GameManagerAccounts.LevelAreaList
                        {
                            Items = new HashSet<int>(),
                            ExperienceData = new Controller.GameManagerData.GameManagerAccounts.LevelAreaList.Experience()
                        },
                        GameMangerDataID = i,
                        MainAccount = p.AccountSettings.MainAccount,
                        Gamestate = new Controller.GameManagerData.GameManagerAccounts.GameStates(),
                        RiftHelper = new Controller.Enums.Rift()
                    });

                    Controller.GameManager.Instance.GameManagerData.Add(i, new Controller.GameManagerData.GameManagerData
                    {
                        GameData = new Controller.GameManagerData.GameManagerData.InGameData
                        {
                            Experience = new Controller.GameManagerData.GameManagerData.InGameData.ExperienceData { },
                            StashItems = new Controller.GameManagerData.GameManagerData.InGameData.Stash()
                        },
                        Multibox = p.AccountSettings,
                        AreaLevels = new Dictionary<int, Controller.GameManagerData.GameManagerData.AreaList>(),
                        RiftManager = new Dictionary<int, Controller.GameManagerData.GameManagerData.Rifts>(),
                    });
                    i++;
                }
            }

            catch(Exception ex)
            {
                Extensions.Execute.UIThread(() =>
                MessageBox.Show(ex.ToString()));
            }


            D3Crawler.Stop();
            Controller.GameManager.Instance.GameManagerStart();

          //  FindersKeepers.MainWindow.Window.StartPause.Visibility = Visibility.Collapsed;
           // FindersKeepers.MainWindow.Window.Pause.Visibility = Visibility.Visible;

            Container.Children.Clear();
            count = 1;
            Container.Children.Add(new Running());
        }

        private void FlashWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                foreach(var x in PHelper.Values)
                {
                    if (x.Owner == sender.CastHelper<StackPanel>().FindParent<Grid>().FindParent<StackPanel>().FindParent<StackPanel>().FindParent<Border>())
                        Helpers.PInvokers.FlashWindow(x.Process.MainWindowHandle);
                }
            }

            catch { }
        }
    }
}
