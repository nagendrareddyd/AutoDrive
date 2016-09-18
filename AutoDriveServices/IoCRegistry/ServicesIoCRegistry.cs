using AutoDriveIoc;
using Castle.MicroKernel.Registration;
using AutoDriveServices.Product;
using AutoDriveServices.MasterData;
using AutoDriveServices.Instructor;
using AutoDriveServices.Student;
using AutoDriveServices.Suburb;
using AutoDriveServices.Address;

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
            IocContainer.container.Register(Component.For<ISuburbService>().ImplementedBy<ISuburbService>());
            IocContainer.container.Register(Component.For<IAddressService>().ImplementedBy<IAddressService>());
        }
    }
}
