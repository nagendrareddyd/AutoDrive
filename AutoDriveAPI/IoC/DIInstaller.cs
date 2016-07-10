using AutoDriveIoc;
using AutoDriveServices.IoCRegistry;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDriveAPI.IoC
{
    public class DIInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            IocContainer.container = container;
            ServicesIoCRegistry.Init();
        }
    }
}