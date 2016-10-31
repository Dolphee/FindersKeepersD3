using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FindersKeepers.Controller;
using System.Windows.Input;

namespace FindersKeepers.Templates.Templating
{
    public class BasicEvents : UserControl
    {
        public static System.Windows.Forms.NotifyIcon Notify = null;
        public void Minimize(object sender, MouseButtonEventArgs e) { MinimizeBase(); }
        public void Minimize(object sender, MouseButtonEventHandler e) { MinimizeBase(); }
        public void CloseWindow(object sender, MouseButtonEventArgs e) { CloseWindowBase(); }
        public void Move(object sender, MouseButtonEventArgs e) { MainWindow.Window.DragMove(); }
        public void CloseWindowBase() { CloseFindersKeepers(); }

        public static void MinimizeBase()
        {
            Extensions.TryInvoke(() =>
            {
                MainWindow.Window.WindowState = System.Windows.WindowState.Minimized;

                Notify = new System.Windows.Forms.NotifyIcon();
                Notify.Icon = Properties.Resources.FKIcon;
                Notify.Text = "FindersKeepersD3 - " + Config.Get<FKConfig>().FindersKeepersVersion;
                Notify.Visible = true;
                Notify.Click +=
                    delegate (object senders, EventArgs args)
                    {
                        MainWindow.Window.Show();
                        MainWindow.Window.WindowState = WindowState.Normal;
                        Notify.Visible = false;
                        MainWindow.Window.Activate();
                        Notify.Dispose();
                    };
            });
        }

        public static void CloseFindersKeepers()
        {
            Extensions.TryInvoke(() =>
            {
                if (GameManager.Instance.GManager.GRef.D3Overlay != null)
                    GameManager.Instance.GManager.GRef.D3Overlay.Close();

                if (GameManager.Instance.GManager.GThread.Threads.Count > 0)
                    foreach (AccountWatcherThread Thread in GameManager.Instance.GManager.GThread.Threads.ToList())
                        Thread.ExitFlag = true;

                Extensions.Performance.Save();
                MainWindow.Config.Save();
            });

            System.Windows.Application.Current.Shutdown();
        }

        public static void TryBringBack()
        {
            if (MainWindow.Window.WindowState == WindowState.Minimized)
            {
                MainWindow.Window.Show();
                MainWindow.Window.WindowState = WindowState.Normal;
                MainWindow.Window.Activate();
                if (Notify != null)
                {
                    Notify.Visible = false;
                    Notify.Dispose();
                }
            }
        }
    }
}
