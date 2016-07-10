using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDriveIoc
{
    public class IocContainer
    {
        public static IWindsorContainer container;
        public static void Init()
        {
            /*if(container == null)
                container = new WindsorContainer();            */
        }

        public static T Resolve<T>()
        {
            return (T)container.Resolve(typeof(T));
        }
    }
}