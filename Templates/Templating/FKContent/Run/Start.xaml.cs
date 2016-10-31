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
using System.Diagnostics;
using Enigma.Memory;
using System.Security.Principal;
using FindersKeepers.Helpers;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : UserControl
    {
        public class ProcessHelper
        {
           public Process Process;
           public FKAccounts.Multibox Multibox;
        }

        Process[] DiabloIII = FindersKeepers.Helpers.PInvokers.GetDiabloIII();
        List<ProcessHelper> StartList = new List<ProcessHelper>();
        
        public Start()
        {
            InitializeComponent();


            Controller.GameManager.Instance = new Controller.GameManager();
            Controller.GameManager.Instance.Accounts = new List<Controller.GameManagerData.GameManagerAccounts>();

            //if(Enigma.D3.Engine.SupportedVersion == (Version)DiabloIII.FirstOrDefault().MainModule.FileVersionInfo.FileVersion)

            /* if (DiabloIII.Count() > 1)
             {
                 test();
                 return;
             }*/

            if (DiabloIII.Count() < 1)
            {
                Container.Children.Clear();
                //Container.Children.Add(new ErrorMessage(null, this.GetType(), Container));
                return;
            }

            if(DiabloIII.Count() == 1)
            {
                ProcessInList(DiabloIII.FirstOrDefault(), (FindNickname(DiabloIII.FirstOrDefault().MainWindowTitle) == null) ?  
                    Config.Get<FKAccounts>().GetIfNotPresent() : 
                    FindNickname(DiabloIII.FirstOrDefault().MainWindowTitle));

                StartFK(null, null);
                return;
            }

            Find();
        }

        protected void Find()
        {
            int i = 1;
            foreach (Process p in DiabloIII.Where(x => x.MainWindowTitle != null))
            {
                /*if (Administrator.IsProcessOwnerAdmin(p) && !RunningAsAdmin()) // Is running as admin
                {
                    Container.Children.Clear();
                    Container.Children.Add(new NeedAdmin());
                    break;
                }*/

                Init(FindNickname(p.MainWindowTitle) , p, i);
                i++;
            }
        }
        public static bool RunningAsAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                    .IsInRole(WindowsBuiltInRole.Administrator);
        }       

        protected FKAccounts.Multibox FindNickname(string name)
        {
            if(name.Substring(0,2) == "is")
                name = name.Substring(0, 3);

            return Config.Get<FKAccounts>().GetAccount(name);
               // Config.Get<FKConfig>().Multibox.Exists(x => x.ProcessName == name) ? Config.Get<FKConfig>().Multibox.Find(x => x.ProcessName == name) : null;
        }
        
        protected void Init(FKAccounts.Multibox Multibox, Process p, int id)
        {
            StackPanel Panel = (StackPanel)Processes.Child;
            if (Multibox == null)
                Multibox = new FKAccounts.Multibox { Nickname = "Not configured #" + (id + 1), MultiboxID = id, TextColor = "939393", Foreground = Extensions.HexToBrush("#"+ Foreground), Enabled = true,BattleTag = "Not set", MainAccount = (id == 1) ? true : false };

            Border Border = new Border { Cursor = Cursors.Hand, Height = 35, Background = (id == 0) ? Extensions.HexToColor("#cfffc0") : Extensions.HexToColor("Transparent")};
            StackPanel Stack = new StackPanel { HorizontalAlignment = HorizontalAlignment.Left, Orientation = Orientation.Horizontal };
            Image Image = new Image { Source = Extensions.FKImage("checked"), Width = 13, Height = 10, Margin = new Thickness(10, 0, 0, 0),  Tag = true };
            TextBlock Text = new TextBlock { Width = 110, FontFamily = new FontFamily("Gautami"), Text = Multibox.Nickname, FontSize = 12, Foreground = Extensions.HexToColor("#666666"), Margin = new Thickness(10, 10, 0, 0) };
                
            Border.MouseDown += ChangeProcess;
            Border.Tag = (object)Multibox;
            Stack.Children.Add(Image);
            Stack.Children.Add(Text);
            Border.Child = Stack;
            Panel.Children.Add(Border);
            Image.MouseDown += ((s,e) => {
                bool State = ((bool)Image.Tag) ? false : true;
                Image.Tag = State;
                Image.Source = (State) ? Extensions.FKImage("checked") : Extensions.FKImage("_checked");
                ProcessInList(p, Multibox);
            });

           /* try
            {
                Enigma.D3.Engine Enginer = new Enigma.D3.Engine(p);
                Multibox.BattleTag = Enginer.Memory.Reader.ReadChain<Enigma.D3.RefString>(0x01B5D54C, 0x10, 0x9C, 0x30).x04_PtrText;
               
            }

            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }*/
            
            ProcessInList(p, Multibox);

            if (id == 1)
                ChangeProcess(Border, null);
            
        }

        private void ProcessInList(Process p, FKAccounts.Multibox m)
        {
            if (StartList.Exists(x => x.Process == p))
                StartList.Remove(StartList.Find(x => x.Process == p));
            else
                StartList.Add(new ProcessHelper { Process = p, Multibox = m });
        }

        private void StartFK(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Controller.GameManager.Instance.Accounts.Count > 0)
                {
                    MessageBox.Show("Finderskeepers is already running");
                    return;
                }

                if (StartList.Count < 1)
                {
                    MessageBox.Show("You need to select at least one Diablo III account");
                    return;
                }
                
                short i = 0;

                foreach (ProcessHelper p in StartList)
                {
                    FKAccounts.Multibox MultiboxID = p.Multibox;

                    Controller.GameManager.Instance.Accounts.Add(new Controller.GameManagerData.GameManagerAccounts
                    {
                        DiabloIII = new Enigma.D3.Engine(p.Process),
                        LevelAreaContainer = new Controller.GameManagerData.GameManagerAccounts.LevelAreaList
                        {
                            Items = new HashSet<int>(),
                            ExperienceData = new Controller.GameManagerData.GameManagerAccounts.LevelAreaList.Experience()
                        },
                        GameMangerDataID = i,
                        MainAccount = MultiboxID.MainAccount, Gamestate = new Controller.GameManagerData.GameManagerAccounts.GameStates(),
                        RiftHelper = new Controller.Enums.Rift()
                    });

                    Controller.GameManager.Instance.GameManagerData.Add(i, new Controller.GameManagerData.GameManagerData
                    {
                        GameData = new Controller.GameManagerData.GameManagerData.InGameData
                        {
                            Experience = new Controller.GameManagerData.GameManagerData.InGameData.ExperienceData { },
                            StashItems = new Controller.GameManagerData.GameManagerData.InGameData.Stash()
                        },
                        Multibox = MultiboxID,
                        AreaLevels = new Dictionary<int, Controller.GameManagerData.GameManagerData.AreaList>(),
                        RiftManager = new Dictionary<int, Controller.GameManagerData.GameManagerData.Rifts>(),
                    });

                    i++;
                }

               // FindersKeepers.MainWindow.Window.StartPause.Visibility = Visibility.Collapsed;
                //FindersKeepers.MainWindow.Window.Pause.Visibility = Visibility.Visible;

                Controller.GameManager.Instance.GameManagerStart();
                Container.Children.Clear();
                Container.Children.Add(new Running());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        protected void UpdateInformation(FKAccounts.Multibox m)
        {
            Nickname.Text = m.Nickname;
            BattleTag.Text = ((m.BattleTag == null)) ? "Battletag not present" : m.BattleTag;
            ProcessTitle.Text = m.BattleTag;
            Tracking.Text = (m.ItemTracking) ? "This account will track items and experience" : "This account will not track items and experience";
            MainAccount.Text = (m.MainAccount) ? "This account is your main account" : "This account is not your main account";
            MainAccountImage.Source = m.MainAccount ? Extensions.FKImage("Icons/enabled") : Extensions.FKImage("Icons/disabled");
            TrackingImage.Source = m.ItemTracking ? Extensions.FKImage("Icons/enabled") : Extensions.FKImage("Icons/disabled");
        }

        private void ChangeProcess(object sender, MouseButtonEventArgs e)
        {
            StackPanel Panel = (StackPanel)Processes.Child;
           
            foreach(var Border in Panel.Children.OfType<Border>())
            {
                Border.Background = Brushes.Transparent;
            }

            ((Border)sender).Background = Extensions.HexToColor("#cfffc0");
            UpdateInformation((FKAccounts.Multibox)((Border)sender).Tag);
        }

   
    }
}
