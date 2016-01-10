using DockerServices;
using System;
using System.Threading;

namespace DockerServicesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new TestService();
            service.Run();
        }
    }

    class TestService : DockerServiceBase
    {
        protected override void Start()
        {
            Console.WriteLine("Started");
        }

        protected override void Stop()
        {
            Console.WriteLine("Stoping...");
            Thread.Sleep(2000);
            Console.WriteLine("Stoped");
        }
    }
}
