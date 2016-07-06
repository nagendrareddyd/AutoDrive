using AutoDriveDataModel.Repository;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveIoc;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDriveServices.Product;
namespace AutoDriveServices.IoCRegistry
{
    public class ServicesIoCRegistry
    {
        public ServicesIoCRegistry()
        {
           IocContainer.container.Register(Component.For<IProductServices>().ImplementedBy<ProductServices>());
        }

        public static void Init() {
            AutoDriveDataModel.IoCRegistry.DataIoCRegistry.Init();
        }
    }
}
