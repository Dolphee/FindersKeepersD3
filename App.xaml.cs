using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FindersKeepers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Threading.Mutex _instanceMutex = null;
        private List<string> Libraries = new List<string>() { "FK.Helpers.dll", "FK.UI.dll" };
        public static bool Mutex = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            _instanceMutex = new System.Threading.Mutex(true, "FindersKeepers", out createdNew);
            if (!createdNew)
            {
                //MessageBox.Show("FindersKeepers is already running!");
               // _instanceMutex = null;
               // Mutex = true;
                //Application.Current.Shutdown();
                //return;
            }

            foreach (string Lib in Libraries)
            {
                if (FindersKeepers.Helpers.PInvokers.CheckLibrary(Lib))
                {
                    MessageBox.Show("You're missing \n"+ AppDomain.CurrentDomain.BaseDirectory+ "" + Lib + ".\n\nFK cannot start without this file!", "Missing DLL Library");
                    Application.Current.Shutdown();
                    return;
                }
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_instanceMutex != null)
                _instanceMutex.ReleaseMutex();
            base.OnExit(e);
        }


    }
}
