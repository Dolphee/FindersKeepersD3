using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FindersKeepers.Controller
{
    public class WatcherThread : IDisposable
    {
        private Timer Timer;
        private readonly object TimerLock = new object();
        private const int UpdateInterval = 30;
        private readonly object TaskLock = new object();
        private List<Action> Task = new List<Action>();

        public WatcherThread()
        {
            Timer = new Timer(OnTick, null, Timeout.Infinite, UpdateInterval);
        }

        public void Start()
        {
            Timer.Change(0, UpdateInterval);
        }

        public void Stop()
        {
            Timer.Change(Timeout.Infinite, UpdateInterval);
        }

        public void Dispose()
        {
            Stop();
            Timer.Dispose();
        }

        private void OnTick(object state)
        {
            if (Monitor.TryEnter(TimerLock))
            {
                try
                {
                    Update();
                }
                finally
                {
                    Monitor.Exit(TimerLock);
                }
            }
        }

        private void Update()
        {
            lock (TaskLock)
            {
                foreach (Action Tasks in Task)
                {
                    Tasks.TryInvoke();
                }
            }
        }

        public void AddTask(Action Tasks)
        {
            lock (TaskLock)
            {
                Task.Add(Tasks);
            }
        }
    }
}
