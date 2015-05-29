using System;
using System.Net;
using System.Threading;
using ChatInterfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ServerInitializationModule.ModuleDefinitions
{
    public class Module : IModule
    {
        private readonly IUnityContainer serviceProvider;

        public Module(IUnityContainer serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Initialize()
        {
            IServerChatService serverChatService = serviceProvider.Resolve<IServerChatService>();
            serverChatService.Start(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000 ), "Server");
            ThreadPool.QueueUserWorkItem((o) =>
            {
                while (Console.ReadLine() != "Stop")
                {
                    Thread.Yield();
                }
                serverChatService.Stop();
            });
            //new Thread(() =>
            //{
            //    for (int i = 0; i < 200; i++)
            //    {
            //        Thread.Yield();
            //        serverChatService.SendMessage(
            //            new ChatUser
            //            {
            //                IpAddress = "127.0.0.1",
            //                Port = 11000,
            //                Login = "Test"
            //            }, "dd");
            //    }
            //}).Start();
        }
    }
}
