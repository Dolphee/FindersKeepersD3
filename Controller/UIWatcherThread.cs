using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FindersKeepers.Controller
{
    public class UIWatcherThread
    {
        public enum ThreadState { Loaded = -1, Loading = 0 }
        public Thread Thread;
        public Action _Action;

        public UIWatcherThread()
        {
            Thread = new Thread(new ThreadStart(WorkThread));            
        }

        public void Start()
        {
            Thread.Start();

        }

        public void WorkThread()
        {
            try
            {
                _Action.Invoke();
            }

            catch(Exception e)
            {
                Extensions.Execute.UIThread(() => 
                {
                    System.Windows.MessageBox.Show(e.ToString());
                });
            }
        }

        public void UpdateUI(Action _Action)
        {
            
        }


    }
}
