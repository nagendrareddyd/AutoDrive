using AspNet.Identity.MongoDB;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using System;

namespace AutoDriveDataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private IMongoRepository<Product> _productRepository;
        private IMongoRepository<ApplicationUser> _userRepository;
        private IMongoRepository<UserModel> _userModelRepository;
        private IMongoRepository<IdentityRole> _roleRepository;
        private IMongoRepository<Area> _areaRepository;
        private IMongoRepository<Instructor> _instructorRepository;
        private IMongoRepository<Student> _studentRepository;
        private IMongoRepository<Suburb> _suburbRepository;
        private IMongoRepository<Booking> _bookingRepository;
        public IMongoRepository<Product> GetProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<Product>>();
                    _productRepository.CollectionName = "Products";
                }
                return _productRepository;
            }
        }
        public IMongoRepository<ApplicationUser> GetUserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<ApplicationUser>>();
                    _userRepository.CollectionName = "users";
                }
                return _userRepository;
            }
        }
        public IMongoRepository<UserModel> GetUserModelRepository
        {
            get
            {
                if (_userModelRepository == null)
                {
                    _userModelRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<UserModel>>();
                    _userModelRepository.CollectionName = "users";
                }
                return _userModelRepository;
            }
        }
        public IMongoRepository<IdentityRole> GetRoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<IdentityRole>>();
                    _roleRepository.CollectionName = "roles";
                }
                return _roleRepository;
            }
        }
        public IMongoRepository<Area> GetAreaRepository
        {
            get
            {
                if (_areaRepository == null)
                {
                    _areaRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<Area>>();
                    _areaRepository.CollectionName = "Areas";
                }
                return _areaRepository;
            }
        }
        public IMongoRepository<Instructor> GetInstructorRepository
        {
            get
            {
                if (_instructorRepository == null)
                {
                    _instructorRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<Instructor>>();
                    _instructorRepository.CollectionName = "Instructors";
                }
                return _instructorRepository;
            }
        }
        public IMongoRepository<Student> GetStudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<Student>>();
                    _studentRepository.CollectionName = "Students";
                }
                return _studentRepository;
            }
        }
        public IMongoRepository<Suburb> GetSuburbRepository
        {
            get
            {
                if (_suburbRepository == null)
                {
                    _suburbRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<Suburb>>();
                    _suburbRepository.CollectionName = "Suburbs";
                }
                return _suburbRepository;
            }
        }
        public IMongoRepository<Booking> GetBookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = AutoDriveIoc.IocContainer.Resolve<IMongoRepository<Booking>>();
                    _bookingRepository.CollectionName = "Bookings";
                }
                return _bookingRepository;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

