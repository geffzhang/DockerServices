using System;

namespace DockerServices
{
    internal interface IExitSignal
    {
        event EventHandler Signaled;
    }
}