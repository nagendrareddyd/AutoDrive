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
        private static IWindsorContainer container;
        public static void Init()
        {
            container = new WindsorContainer();
            //container.Register(Component.For<IMongoDataAccess>().ImplementedBy<MongoDataAccess>());
            //container.Register(Component.For(typeof(IMongoRepository<>)).ImplementedBy(typeof(MongoRepository<>)));
        }

        public static T Resolve<T>()
        {
            return (T)container.Resolve(typeof(T));
        }
    }
}