using Mono.Unix;
using Mono.Unix.Native;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DockerServices
{
    internal class UnixExitSignal : IExitSignal
    {
        public event EventHandler Signaled;

        public UnixExitSignal()
        {
            UnixSignal[] signals = new UnixSignal[]
            {
                new UnixSignal(Signum.SIGTERM),
                new UnixSignal(Signum.SIGINT),
                new UnixSignal(Signum.SIGUSR1),
            };
            Task.Factory.StartNew(() =>
            {
                int index = UnixSignal.WaitAny(signals, Timeout.Infinite);
                if (Signaled != null)
                    Signaled(this, EventArgs.Empty);
            });
        }
    }
}