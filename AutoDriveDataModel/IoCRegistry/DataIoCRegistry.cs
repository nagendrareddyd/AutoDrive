using AutoDriveDataModel.Repository;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveDataModel.UnitOfWork;
using AutoDriveIoc;
using Castle.MicroKernel.Registration;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.IoCRegistry
{
    public class DataIoCRegistry
    {
        static DataIoCRegistry()
        {
            IocContainer.container.Register(Component.For<IMongoDataAccess>().ImplementedBy<MongoDataAccess>());            
            IocContainer.container.Register(Component.For(typeof(IMongoRepository<>)).ImplementedBy(typeof(MongoRepository<>)));
            IocContainer.container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork.UnitOfWork>());            
        }

        public static void Init() { }
    }
}
