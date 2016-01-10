using System;

namespace DockerServices
{
    public class DockerService : DockerServiceBase
    {
        private Action start;
        private Action stop;

        public DockerService(Action start, Action stop)
        {
            this.start = start;
            this.stop = stop;
        }

        protected override void Start()
        {
            start();
        }

        protected override void Stop()
        {
            stop();
        }
    }
}
