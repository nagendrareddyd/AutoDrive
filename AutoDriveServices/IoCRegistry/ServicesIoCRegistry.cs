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
        static ServicesIoCRegistry()
        {
            AutoDriveDataModel.IoCRegistry.DataIoCRegistry.Init();
        }

        public static void Init() {            
            IocContainer.container.Register(Component.For<IProductServices>().ImplementedBy<ProductServices>());
        }
    }
}
