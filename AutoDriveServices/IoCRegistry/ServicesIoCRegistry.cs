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
using AutoDriveServices.MasterData;
using AutoDriveServices.Instructor;
using AutoDriveServices.Student;
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
            IocContainer.container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            IocContainer.container.Register(Component.For<IAreaService>().ImplementedBy<AreaService>());
            IocContainer.container.Register(Component.For<IInstructorService>().ImplementedBy<InstructorService>());
            IocContainer.container.Register(Component.For<IStudentService>().ImplementedBy<StudentService>());
        }
    }
}
