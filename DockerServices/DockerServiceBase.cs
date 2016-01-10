using System;
using System.Threading;

namespace DockerServices
{
    public abstract class DockerServiceBase
    {
        public void Run()
        {
            bool active = true;
            IExitSignal exitSignal = GetExitSignal();
            if (exitSignal == null) throw new InvalidOperationException("Invalid platform");
            exitSignal.Signaled += (s, e) => { active = false; };
            Start();
            while (active)
                Thread.Sleep(1000);
            Stop();
        }

        protected abstract void Start();
        protected abstract void Stop();

        private IExitSignal GetExitSignal()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    return new UnixExitSignal();
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    return new WindowsExitSignal();
            }
            return null;
        }
    }
}
