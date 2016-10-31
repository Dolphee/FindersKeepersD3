using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Enigma.D3.UI.Controls;
using Enigma.D3.UI;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FindersKeepers.Helpers;
using System.Collections.ObjectModel;
using FindersKeepers.Controller;
using System.Windows;
using FindersKeepers.Controller.GameManagerData;

namespace FindersKeepers.Templates.Overlay
{
    public class Attacher<T> : OverlayHelper<T> where T : UserControl
    {
        public Canvas Window;
        private Grid Root;
        public double UIScale;
        public Mainbar.MainBarHandler MainBarHandler;
        public ItemOverlayNotifyer ItemOverlay;
        public ObservableCollection<ToolTipHelper> Tooltips { get; set; }
        public Size DiabloIII { get; set; }
        public List<HitTesting> HitTesting;
        public GameManagerAccounts.GameStates Account { get { return GameManagerAccountHelper.Current.Gamestate; } }

        public Attacher(D3Overlay D3Window)
        {
            FKControls = new ObservableCollection<FKControl>();
            Tooltips = new ObservableCollection<ToolTipHelper>();
            HitTesting = new List<Helpers.HitTesting>();

            D3RenderControl = Activator.CreateInstance<T>();
            D3RenderControl.DataContext = this;

            Root = new Grid() { Height = 1200.5d };
            Root.Children.Add(D3RenderControl);
            
            Window = (Canvas)D3Window.Window;
            Window.Children.Add(Root);
            Window.SizeChanged += (s, e) => UpdateSizeAndPosition();

            UpdateSizeAndPosition();
        }

        public Attacher() : this(null) { } // Not the main window e.g MainBarControl

        public void Initialize()
        {
            Extensions.TryInvoke(() =>
            {
                MainBarHandler = CreateWPFControl<Templates.Mainbar.MainBarHandler>("Mainbar", true);
                ItemOverlay = CreateWPFControl<Templates.Overlay.ItemOverlayNotifyer>("ItemOverlay", true);

                if (Config.Get<FKConfig>().General.MiniMapSettings.Enabled)
                    CreateWPFControl<Templates.MinimapNotifyObjects.MinimapNotify>("Minimap", true);
                if (Config.Get<FKConfig>().General.MiniMapSettings.LargeMap)
                    CreateWPFControl<Templates.MinimapNotifyObjects.LargeMapNotify>("LargeMap", true);

                if (Config.Get<FKConfig>().General.Misc.Inventory)
                    CreateControl<Templates.Inventory.Inventory>("Inventory", true);


              //  CreateControl<Debug>("Debug");
            });
        }

        public Rect GetAbsoltutePlacement(FrameworkElement element, bool relativeToScreen = false)
        {
            var absolutePos = element.PointToScreen(new System.Windows.Point(0, 0));
            if (relativeToScreen)
            {
                return new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
            }
            var posMW = Window.PointToScreen(new System.Windows.Point(0, 0));
            absolutePos = new System.Windows.Point(absolutePos.X - posMW.X, absolutePos.Y - posMW.Y);
            return new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
        }

        public ToolTipHelper RegisterTooltip(Point t, FrameworkElement re)
        {

            Extensions.Execute.UIThread(() =>
            {
                MessageBox.Show(GetAbsoltutePlacement(re).ToString());
            });

            ToolTipHelper Helper = new ToolTipHelper
            {
                X = Math.Abs(t.X),
                Y = Math.Abs(t.Y),
                BorderBrush = "FFFFFF",
                Background = "000000",
                Opacity = 0.9,
                Text = "Hej",
                BorderSize = 3,
                CornerRadius = 3,
                Width = 100,
                Height = 100,
            };

            Tooltips.Add(Helper);
            return Helper;
        }

        public override void TryUpdate()
        {

            if (!Controller.GameManagerData.GameManagerAccountHelper.Current.Gamestate.PlayerLoaded)
                return;

            base.TryUpdate();

            // D3RenderControl
            //Extensions.Execute.Render();

            //foreach (HitTesting Hit in HitTesting)
            //  Hit.HitTest();

        }

        public void UpdateSizeAndPosition()
        {
            var uiScale = Window.ActualHeight / 1200.5d;
            UIScale = uiScale;
            Root.Width = Window.ActualWidth / uiScale;
            Root.RenderTransform = new ScaleTransform(uiScale, uiScale, 0, 0);
        }
    }

    public class FKControl : NotifyingObject
    {
        public string Name;
        public bool IsWPF { get { return Controller != null; } }
        public IFKControl _Control; 
        public IFKControl Control { get { return _Control; } }
        public UIRekt _UIRect;
        public UIRekt UIRect { get
            {
                if (_UIRect == null)
                  _UIRect = new UIRekt(Control);
                return _UIRect;
            }
        }
        public IFKControl Controller;

        public FKControl(IFKControl C, string N, object WPFControl = null)
        {
            _Control = C;
            Controller = (IFKControl)WPFControl;
            Name = N;
            Refresh();
        }

        public void UpdateControl()
        {

            //if (!GameManager.Instance.GManager.GList.MainAccount.Gamestate.PlayerLoaded || GameManager.Instance.GManager.GList.MainAccount.Gamestate.LoadingArea)
               //  return;

           if (!IsWPF)
                Control.IUpdate();
            else
                Controller.IUpdate();
        }

        public class UIRekt
        {
            public double Width { get { return Control.Width; } }
            public double Height { get { return Control.Height; } }
            public double X { get { return Canvas.GetLeft(Control); } }
            public double XR { get { return Canvas.GetRight(Control); } }
            public double Y { get { return Canvas.GetTop(Control); } }

            public UserControl Control;
            public UIRekt(IFKControl c) { Control = (UserControl)c; }


        }

    }

}
