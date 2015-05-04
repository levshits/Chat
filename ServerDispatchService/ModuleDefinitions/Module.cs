using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatInterfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ServerDispatchService.ModuleDefinitions
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
            _container.RegisterType<IDispatchService, ServerDispatchService>();
        }
    }
}
