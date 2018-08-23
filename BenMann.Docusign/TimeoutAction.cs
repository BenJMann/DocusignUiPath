using System;
using System.Threading;
using System.Threading.Tasks;

namespace BenMann.Docusign
{
    public class TimeoutAction
    {
        private const int waitStep = 100;
        private int waited = 0;
        private Thread ActionThread { get; set; }
        private Thread TimeoutThread { get; set; }
        private AutoResetEvent ThreadSynchronizer { get; set; }
        private bool _success;
        private bool _timout;

        public TimeoutAction(int waitLimit, Action action)
        {
            ThreadSynchronizer = new AutoResetEvent(false);
            ActionThread = new Thread(new ThreadStart(delegate
            {
                action.Invoke();
                if (_timout) return;
                _timout = true;
                _success = true;
                ThreadSynchronizer.Set();
            }));

            TimeoutThread = new Thread(new ThreadStart(delegate
            {
                int toSleep = 0;
                while (waited < waitLimit)
                {
                    toSleep = waitLimit - waited >= waitStep ? waitStep : waitLimit - waited;
                    Thread.Sleep(toSleep);
                    waited += toSleep;
                    if (_success) return;
                }
                _timout = true;
                _success = false;
                ThreadSynchronizer.Set();
            }));
        }

        public bool Start()
        {
            ActionThread.Start();
            TimeoutThread.Start();

            ThreadSynchronizer.WaitOne();

            ThreadSynchronizer.Close();
            return _success;
        }
    }
}