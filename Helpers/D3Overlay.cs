using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Interop;
using System.Windows.Threading;
using FindersKeepers.Templates;
using FindersKeepers.Controller;
using FindersKeepers.Templates.Rifts;
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3.Helpers;
using FindersKeepers.Templates.MinimapNotifyObjects;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Helpers
{
    public class D3Overlay : Window
    {
        public Panel Window;
        public Process Process;
        public IntPtr ParentHandle;
        public IntPtr WindowHandle;
        public DispatcherTimer Timer;
        public WindowInteropHelper Helper;
        public Int32Rect ParentSize;
        public bool Exit = false;
        private bool OverlayVisibility = false;

        public static D3Overlay Create(Process p)
        {
            return new D3Overlay((Panel)new Canvas{}, p);
        }

        public void Delete()
        {
            Extensions.Execute.UIThread(() =>
            {
                this.Close();
            });
        }
        
        public D3Overlay(Panel Container, Process _Process )
        {
            Process = _Process;
            SetParentHandle(Process.MainWindowHandle);

            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.None;
            ShowInTaskbar = false;
            AllowsTransparency = true;
            Background = Brushes.Transparent;

            if (Config._.FKConfig.General.FKSettings.UseAlternativeOverlay)
                Topmost = true;

            SnapsToDevicePixels = true;
            SizeToContent = SizeToContent.WidthAndHeight;

            Window = Container;
            Content = Container;

            Timer = new DispatcherTimer();
            Timer.Tick += ((s, e) => { if(!Timer.IsEnabled) return; OnSizeChanged();});
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Start();

            if(Config.Get<FKConfig>().General.Macros.UseHotkey)
            {
                Hotkey.Register(this.WindowHandle);
                ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
            }
        }

        private void SetParentHandle(IntPtr _parentHandle)
        {
            Helper = new WindowInteropHelper(this);
            ParentHandle = _parentHandle;

            if (!Config._.FKConfig.General.FKSettings.UseAlternativeOverlay)
                Helper.Owner = _parentHandle;
        }

        protected override void OnSourceInitialized(EventArgs e)
		{
            WindowHandle = Helper.Handle;
            SetWindowStyleTransparent(WindowHandle);
            base.OnSourceInitialized(e);
        }

        private void SetWindowStyleTransparent(IntPtr windowHandle)
        {
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int GWL_EXSTYLE = -20;
            //const int LWA_ALPHA = 0x2;
            // const int LWA_COLORKEY = 0x1;

        var exStyle = PInvokers.GetWindowLong(windowHandle, GWL_EXSTYLE);
            PInvokers.SetWindowLong(windowHandle, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT | 0x00000080);

            //  PInvokers.SetWindowLong(windowHandle, GWL_EXSTYLE,
            //  (IntPtr)(PInvokers.GetWindowLong(windowHandle, GWL_EXSTYLE) ^ WS_EX_LAYERED ^ WS_EX_TRANSPARENT));

            // PInvokers.SetLayeredWindowAttributes(windowHandle, 0, 255, LWA_ALPHA);
        }

        void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message == Hotkey.WM_HOTKEY)
            {
                try
                {
                    GameManagerAccounts Main = GameManagerAccountHelper.Current;

                    foreach (GameManagerAccounts Acc in GameManager.Instance.Accounts.NotMainAccount())
                    {
                        Point ClientRect = DiabloIII.FromD3toScreenCoords(DiabloIII.Player(Acc.DiabloIII).Point(), Main.Player.Point());

                        //PInvokers.ClientToScreen(Acc.DiabloIII.Process.MainWindowHandle, ref f);

                        if(Config.Get<FKConfig>().General.Macros.UseForceMove)
                            VirtualInput.KeyClick(Acc.DiabloIII.Process.MainWindowHandle, ClientRect);
                        else
                            VirtualInput.LeftClick(Acc.DiabloIII.Process.MainWindowHandle, ClientRect);
                    }
                }
                
                catch(Exception e)
                {
                    DebugWriter.Write(e);
                }
            }
        }

        public void IsOverlayVisible()
        {
            if (!PInvokers.DiabloActive())
            {
                if(OverlayVisibility)
                    this.Visibility = Visibility.Hidden;

                OverlayVisibility = false;
            }

            else
            {
                if(!OverlayVisibility)
                    this.Visibility = Visibility.Visible;

                OverlayVisibility = true;
            }

        }

        public void OnSizeChanged()
        {
            if ((Process == null))
                return;

            if(Config._.FKConfig.General.FKSettings.UseAlternativeOverlay)
                IsOverlayVisible();

            Int32Rect clientRect = PInvokers.GetClientRect(ParentHandle);

            if(!clientRect.Equals(ParentSize))
            {
                Window.Width = clientRect.Width;
                Window.Height = clientRect.Height;
                Left = clientRect.X;
                Top = clientRect.Y;
                ParentSize = clientRect;
            }

            if (ParentSize == default(Int32Rect))
            {
                ParentSize = clientRect;
                Process Process = PInvokers.CurrentWindow();

                if (Process == null)
                    return;

                ParentHandle = Process.MainWindowHandle;
            }
        }
    }

    public class Attach
    {   
        public Canvas Window;
        public Canvas Root = new Canvas { Height = 1200d};
        public double UIScale;
        public D3Overlay D3Window;

        public Dictionary<string, Tuple<UIElement, object>> UIElements = new Dictionary<string, Tuple<UIElement, object>>();
        public UserControl MainBar;// = new MainBar(); 
        public UserControl ItemOverlay;// = new ItemOverlaySimple();
        public UserControl Information;// = new Information();

        public List<object> Control = new List<object>();
        public HashSet<string> ActorNames = new HashSet<string>();
        public bool Hidden = false;
        public List<IGameStatus> Notify = new List<IGameStatus>();
        public Size Size = new Size();

        public bool Init = false;

        public Attach(D3Overlay D3Window)
        {
            Window = (Canvas)D3Window.Window;
            this.D3Window = D3Window;
            Window.Children.Add(Root);
            Window.SizeChanged += (s, ev) => { AddTask(); Set();  };
            Size = new Size(Enigma.D3.Engine.Current.VideoPreferences.x0C_DisplayMode.x20_Width, Enigma.D3.Engine.Current.VideoPreferences.x0C_DisplayMode.x24_Height);
        }

        public void CloseInformation()
        {
            Information = null;
            Root.Children.Remove(Information);
        }

        public void AddTask()
        {
            if (MainBar != null)
                return;

            MainBar = Add<MainBar>("MainBar");
            ItemOverlay = Add<ItemOverlaySimple>("ItemOverlaySimple");
            Information = Add<Information>("Information");

            try
            {
                //if (Config.Get<FKConfig>().General.MiniMapSettings.Enabled)
                    //AddWPF<MinimapNotify>("Minimap");
               // if (Config.Get<FKConfig>().General.MiniMapSettings.LargeMap)
                //    AddWPF<LargeMapNotify>("LargeMap");
                if (Config.Get<FKConfig>().General.Misc.Inventory)
                    Add<Templates.Inventory.Inventory>("Inventory");

                // AddWPF<Templates.ActorHelper.ActorHelperObserver>("ActorHelper");
            }

            catch (Exception e)
            {
                DebugWriter.Write(e);
            }

            //Add<FindersKeepers.Templates.ActorHelperDebug>("Debug");

            Add<FindersKeepers.Templates.Debug>("Debug");
            //Add<Templates.Rifts.GemUpgrade>("GemUpgrade");
        }

        public UserControl CreateControl<T>(string Name, object Params = null)
        {
            UserControl Control = default(UserControl);

            Extensions.Execute.UIThread(() =>
            {
                if (Params == null)
                    Control = (UserControl)Extensions.CreatePage(typeof(T));
                else
                    Control = (UserControl)Extensions.CreatePage(typeof(T), Params);

                Root.Children.Add(Control);
                this.UIElements.Add(Name, new Tuple<UIElement, object>(Control, Control));
            });

            return Control;
        }

        public T AddWPF<T>(string Name) where T : IWPF
        {
            if (this.UIElements.ContainsKey(Name))
                return default(T);

            IWPF ClassControl = default(T);

            Extensions.Execute.UIThread(() =>
            {
                ClassControl = (IWPF)Extensions.CreatePage(typeof(T));
                Root.Children.Add(ClassControl.Control);
                this.UIElements.Add(Name, new Tuple<UIElement, object>(ClassControl.Control, ClassControl));
            });

            UpdateList();
            return (T)ClassControl;
        }

        public T Add<T>(string Name, object Params = null) where T : UserControl
        {
            if (this.UIElements.ContainsKey(Name))
                return default(T);

            UserControl Control = CreateControl<T>(Name, Params);
            UpdateList();
            return (T)Control;
        }

        public T Search<T>(string Name) where T : UserControl
        {
            foreach (KeyValuePair<string, Tuple<UIElement, object>> Control in UIElements)
                if (Control.Key == Name)
                    return (T)Control.Value.Item1;

            return null;
        }

        public T Search<T>(string Name, bool WPF) where T : class
        {
            foreach (KeyValuePair<string, Tuple<UIElement, object>> Control in UIElements)
                if (Control.Key == Name)
                    return (T)Control.Value.Item2;

            return null;
        }

        public void Call<T>(string Name, string Method) where T : class
        {
            T Control = Search<T>(Name, true);

            if (Control != null)
            {
                System.Reflection.MethodInfo Info = Control.GetType().GetMethod(Method);
                Info.Invoke(Control, null);
            } 
        }

        public void Remove(string Name, UIElement Element = null)
        {
            if (!this.UIElements.ContainsKey(Name))
                return;

            Extensions.Execute.UIThread(() =>
            {
                var Key = this.UIElements.Single(x => x.Key == Name).Value.Item2;

                if (Key is IWPF)
                    ((IWPF)Key).Empty();

                if (Element == null)
                        Root.Children.Remove(this.UIElements.Single(x => x.Key == Name).Value.Item1);
                else
                    Root.Children.Remove(Element);

                this.UIElements.Remove(Name);
            });

            UpdateList();
        }



        public void UpdateList()
        {
            Notify = this.UIElements.Select(x => x.Value.Item2).Where(x => x is IGameStatus).Cast<IGameStatus>().ToList();
            Control = this.UIElements.Select(x => x.Value.Item2).Where(x => x is IPerform).ToList();
        }

        public void TryUpdate()
        {
            try
            {
                foreach(UserControl c in Control.ToList().Where(x => x is IUpdate))
                    ((IUpdate)c).Update();
            }

            catch(Exception e)
            {
             
            }
        }

        public void SetUpdate()
        {
            if (!GameManager.Instance.GManager.GList.MainAccount.Gamestate.PlayerLoaded)
            {
                if (!Hidden)
                {
                    Extensions.Execute.UIThread(() =>
                        Root.Visibility = Visibility.Hidden
                    );

                    Hidden = true;
                }
                return;
            }

            if (GameManager.Instance.GManager.GList.MainAccount.Gamestate.LoadingArea)
            {
                if(!Hidden)
                {
                    Extensions.Execute.UIThread(() =>
                        Root.Visibility = Visibility.Hidden
                    );

                    Hidden = true;
                }
                return;
            }

            else
            {
                if (Hidden)
                {
                    Hidden = false;
                    Extensions.Execute.UIThread(() =>
                        Root.Visibility = Visibility.Visible
                    );
                }
            }
       
           foreach (object c in Control)
            {

                try
                {
                    ((IPerform)c).Set();
                }

                catch (Exception e)
                {
                    DebugWriter.Write(e);
                }
            }

            Extensions.Execute.Render();
        }

        public void Delete()
        {
            Extensions.Execute.UIThread(() =>
            {
                foreach (UserControl c in Control)
                    Extensions.Execute.UIThread(() =>
                        Root.Children.Remove(c)
                    );
            });
        }

        public void Set()
        {
            if (GameManager.Instance.GManager.GRef.Attacher == null)
                return;

             UIScale = D3Window.ActualHeight / 1200d;
             Root.Width = D3Window.ActualWidth / UIScale;
             Root.RenderTransform = new ScaleTransform(UIScale, UIScale, 0 ,0 );

            if (MainBar != null)
            {
                Int32Rect P = PInvokers.GetClientRect(D3Window.Process.MainWindowHandle);

                Size = new Size(Enigma.D3.Engine.Current.VideoPreferences.x0C_DisplayMode.x20_Width, Enigma.D3.Engine.Current.VideoPreferences.x0C_DisplayMode.x24_Height);
                MainBar.Width = (P.Width / UIScale);
                ItemOverlay.Width = (P.Width / UIScale);

                if (Search<LargeMap>("LargeMap") != null)
                    Search<LargeMap>("LargeMap").Width = (P.Width / UIScale);
            }
        }
    }
}
