using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FindersKeepers.Controller;
using FindersKeepers.Controller.GameManagerData;

namespace FindersKeepers.Controller
{
    public class WatchThread : IDisposable
    {
        public static HashSet<int> _handled = new HashSet<int>();
        public static ManualResetEvent _stop = new ManualResetEvent(false);
        public GameManagerData.GameManagerAccounts Accounts;
        public Thread Thread;
        public WatcherThread WatcherThread;
        public bool MyTurn = false;
        public Action Invoke;

        public HashSet<short> Tasks = new HashSet<short>();

        //public Dictionary<int, Task> Tasks = new Dictionary<int, Task>();

        public void Add(List<Action> Thread, GameManagerAccounts Account)
        {
            Tasks.Add(Account.GameMangerDataID);

            _stop.WaitOne();

            Task.Factory.StartNew(() =>
            {
                Account.DiabloIII.Process.WaitForExit();
                Tasks.Remove(Account.GameMangerDataID);

            }, TaskCreationOptions.LongRunning);
        }

        public void UpdateThread()
        {
            do
            {

            }

            while (Tasks.Count != 0);
        }

        public WatchThread()
        {
           /* Thread = new System.Threading.Thread(() =>
            {
                Invoke = () => {
                    GameManager.Actors.Enumerate();
                };
                do
                {
                 
                    /* try
                     {
                         if (Accounts.DiabloIII.Process.HasExited)
                             break;

                         GameManager.Actors.Enumerate();
                         //GameManager.Attacher.SetUpdate();
                     }

                     catch (Exception e)
                     {
                         Extensions.Execute.UIThread(() =>
                         {
                             System.Windows.MessageBox.Show(e.ToString());
                         });
                     }
                }
                while (!Accounts.DiabloIII.Process.HasExited);

                Extensions.Execute.UIThread(() =>
                {
                    System.Windows.MessageBox.Show("Exit muthafucka");
                });
            });

            Thread.Start();*/
        }

        public void Dispose()
        {

        }

        public void Update()
        {
            Extensions.Execute.UIThread(() =>
            {
                System.Windows.MessageBox.Show("HxxxEJ");
            });
        }
    }
}
