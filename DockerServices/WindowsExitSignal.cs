using System;
using System.Runtime.InteropServices;

namespace DockerServices
{
    internal class WindowsExitSignal : IExitSignal
    {
        private delegate bool HandlerRoutine(CtrlTypes CtrlType);
        private HandlerRoutine m_hr;

        public event EventHandler Signaled;

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        private enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public WindowsExitSignal()
        {
            m_hr = new HandlerRoutine(ConsoleCtrlCheck);
            SetConsoleCtrlHandler(m_hr, true);
        }

        private bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                case CtrlTypes.CTRL_BREAK_EVENT:
                case CtrlTypes.CTRL_CLOSE_EVENT:
                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    if (Signaled != null)
                        Signaled(this, EventArgs.Empty);
                    break;
            }
            return true;
        }
    }
}