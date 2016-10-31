using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FindersKeepers.Controller;
using System.Collections.ObjectModel;
using FindersKeepers.Assets;
using FK.UI;

namespace FindersKeepers
{
    public partial class MainWindow : Window
    {
        public static MainWindow Window;
        public static string Version { get { return "FindersKeepersD3 - " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); } }
        private ObservableCollection<MenuContainer> _Menu { get; set; }
        public ObservableCollection<MenuContainer> Menu { get {if (_Menu == null) _Menu = MenuHelper.Create(); return _Menu;} set { } }
        public static Config Config {get;set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Window = this;

            Task.Factory.StartNew(() =>
            {
                // if (App.Mutex)
                //  return;

                Extensions.TryInvoke(() =>
                {
                    Config = new Config();
                    //WebHelper Web = new WebHelper();
                    GameManager.Instance = new GameManager();

                    Extensions.Execute.UIThread(() =>{
                        LoadPage<Templates.RunController>("GameInit"); 
                    });

                    UniqueItems.Load();
                    
                    // Controller.Actor.SNOActors.Add(); 
                });

            },TaskCreationOptions.LongRunning);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
 
        private void ProcessPage(object sender, MouseButtonEventArgs e)
        {
            MenuContainer Element = (MenuContainer)(sender as Border).Tag;
            //(sender as Border).BorderBrush = Extensions.HexToBrush("1e3c53", false);

            foreach (MenuContainer elem in Menu)
                elem.IsActive = (Element.MName == elem.MName);

            if (Element.Target == null)
                return;

            LoadPageBase(Element.Template);
           // Extensions.FindVisualChildren<Border>(sender as Border).FirstOrDefault().AnimateBorder(null, 200, Extensions.FadeDirection.FadeOut);

        }

        public static void LoadPageBase(object Element) {
            MainWindow.Window.MainPage.Children.Clear();
            MainWindow.Window.MainPage.Children.Add((UIElement)Element);
        }
  
        private void AnimateFadeIn(object sender, MouseEventArgs e)
        {
            Extensions.EffectEntity Entity = (sender as FrameworkElement).Create();

            if ((Entity.Data as MenuContainer).IsActive)
                return;

            if (Entity.Get<Brush>("Background").Opacity != 0.6)
                Entity.Opacity(0.6);

            Entity.Animate(SolidColorBrush.ColorProperty, 700, Extensions.FadeDirection.FadeIn);
            Extensions.FindVisualChildren<Border>(Entity.Element).FirstOrDefault().AnimateBorder(Entity, 700, Extensions.FadeDirection.FadeIn);
        }

        private void AnimateFadeOut(object sender, MouseEventArgs e)
        {
            Extensions.EffectEntity Entity = (sender as FrameworkElement).Create();

            if ((Entity.Data as MenuContainer).IsActive)
                return;

            Entity.Animate(SolidColorBrush.ColorProperty, 200, Extensions.FadeDirection.FadeOut);
            Extensions.FindVisualChildren<Border>(Entity.Element).FirstOrDefault().AnimateBorder(Entity, 200, Extensions.FadeDirection.FadeOut);

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) { Templates.Templating.BasicEvents.CloseFindersKeepers(); }
        public void Move(object sender, MouseButtonEventArgs e) { MainWindow.Window.DragMove(); }
        public static void LoadPage<T>(object param) { LoadPageBase(Activator.CreateInstance(typeof(T), param)); }
        public static void LoadPage<T>() { LoadPageBase(Activator.CreateInstance<T>()); }
        public static void LoadPage(Type type) { LoadPageBase(Activator.CreateInstance(type)); }
    }
}
