﻿using ChatInterfaces;
using ChatSocketCommunicationService.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ChatSocketCommunicationService.ModuleDefinitions
{
    public class Module:IModule
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<ISocketCommunicationService, SocketCommunicationService>();
        }
    }
}
