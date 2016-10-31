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
using FindersKeepers.Controller;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for Running.xaml
    /// </summary>
    public partial class Stop : UserControl
    {
        public Stop()
        {
            InitializeComponent();
            //MainWindow.Window.Pause.Visibility = System.Windows.Visibility.Collapsed;
            //MainWindow.Window.StartPause.Visibility = System.Windows.Visibility.Visible;

            try
            {

                if (GameManager.Instance.GManager.GThread.Threads.Count > 0)
                {
                    foreach (var Thread in GameManager.Instance.GManager.GThread.Threads.ToList())
                        Thread.Suspend(true);
                }

                // GameManager.Instance.WatcherThread.Dispose();

                GameManager.Instance.GManager.GRef.Actors.Timer.Stop();

                GameManager.Instance = new GameManager();
                GameManager.Actions = new List<Action>();

                //GameManager.Instance.GManager.GRef.ItemOverlay = new List<OverlayItems>();
                // GameManager.Instance.WatcherThread = null;
                //Helpers.SceneHelper.Reset();
                // Buffs.Reset();

                // foreach (var x in GameManager.Instance.Accounts)
                //    x.DiabloIII.Dispose();

                // GameManager.Instance.Accounts = new List<Controller.GameManagerData.GameManagerAccounts>();
                //GameManager.Instance.GameManagerData = new Dictionary<short, Controller.GameManagerData.GameManagerData>();
                // GameManager.Instance.GManager.GRef.Actors.Break = false;

                // if (GameManager.Instance.GManager.GRef.D3Overlay != null)
                //  GameManager.Instance.GManager.GRef.D3Overlay.Delete();
            }

            catch(Exception e ){ 
                MessageBox.Show(e.ToString());
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

        private void MinimizeClick(object sender, MouseButtonEventArgs e)
        {
           // MainWindow.Window.Minimize();
        }
    }
}
