using AspNet.Identity.MongoDB;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
namespace AutoDriveDataModel.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMongoRepository<Product> GetProductRepository { get; }
        IMongoRepository<ApplicationUser> GetUserRepository { get; }
        IMongoRepository<IdentityRole> GetRoleRepository { get; }
        IMongoRepository<Area> GetAreaRepository { get; }
        IMongoRepository<Instructor> GetInstructorRepository { get; }
        IMongoRepository<Student> GetStudentRepository { get; }
        IMongoRepository<Suburb> GetSuburbRepository { get; }
        IMongoRepository<Booking> GetBookingRepository { get; }
        IMongoRepository<UserModel> GetUserModelRepository { get; }
    }
}
