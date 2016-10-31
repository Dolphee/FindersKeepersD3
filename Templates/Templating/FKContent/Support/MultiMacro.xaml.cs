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

namespace FindersKeepers.Templates.Support
{
    /// <summary>
    /// Interaction logic for MultiMacro.xaml
    /// </summary>
    public partial class MultiMacro
    {
        public Support _support;
        public FKConfig.GeneralSettings.MultiBoxMacro Macro {get;set; }

        public MultiMacro(object s) : base(s)
        {
            InitializeComponent();

            Macro = Config.Get<FKConfig>().General.Macros;

            (s as SupportController).IsTemplate = true;
            (s as SupportController).IDesignHelper.Settings.UseMainMenu = false;
            Set();
        }


        public void Set()
        {
            TextKey.Text = KeyInterop.KeyFromVirtualKey(Config.Get<FKConfig>().General.Macros.Hotkey).ToString();
            Forcekey.Text = KeyInterop.KeyFromVirtualKey(Config.Get<FKConfig>().General.Macros.ForceMoveHotkey).ToString();
        }

        private void EnterKey(object sender, MouseButtonEventArgs e)
        {
            if (sender.CastVisual<StackPanel>().FindParent<Border>().FindParent<Border>().Opacity != 1)
                return;

            TextKey.Visibility = Visibility.Collapsed;
            KeyChoose.Visibility = Visibility.Visible;            
        }

        private void EnterKeys(object sender, MouseButtonEventArgs e)
        {
            if (sender.CastVisual<StackPanel>().FindParent<Border>().FindParent<Border>().Opacity != 1)
                return;

            Forcekey.Visibility = Visibility.Collapsed;
            FKeyChoose.Visibility = Visibility.Visible;
        }

        private void KeyHandler(object sender, KeyEventArgs e)
        {
            TextKey.Visibility = Visibility.Visible;
            KeyChoose.Visibility = Visibility.Collapsed;

            TextKey.Text = e.Key.ToString();

            Config.Get<FKConfig>().General.Macros.Hotkey = KeyInterop.VirtualKeyFromKey(e.Key);

            if (Controller.GameManager.Instance.GManager.GRef.Attacher != null)
            {
                Helpers.Hotkey.Unregister();
                Helpers.Hotkey.Register(Controller.GameManager.Instance.GManager.GRef.Attacher.D3Window.WindowHandle);
            }
        }

        private void FKeyHandler(object sender, KeyEventArgs e)
        {
            Forcekey.Visibility = Visibility.Visible;
            FKeyChoose.Visibility = Visibility.Collapsed;

            Forcekey.Text = e.Key.ToString();

            Config.Get<FKConfig>().General.Macros.ForceMoveHotkey = KeyInterop.VirtualKeyFromKey(e.Key);
        }

        private void Focus(object sender, EventArgs e)
        {
            KeyChoose.Focus();
        }

        private void FFocus(object sender, EventArgs e)
        {
            FKeyChoose.Focus();
        }
    }
}
