using AutoDriveDataModel.Repository;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveIoc;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.IoCRegistry
{
    public class DataIoCRegistry
    {
        public DataIoCRegistry()
        {
            IocContainer.container.Register(Component.For<IMongoDataAccess>().ImplementedBy<MongoDataAccess>());            
            IocContainer.container.Register(Component.For(typeof(IMongoRepository<>)).ImplementedBy(typeof(MongoRepository<>)));
        }

        public static void Init() { }
    }
}
