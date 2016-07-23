using AspNet.Identity.MongoDB;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository;
using AutoDriveDataModel.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable,IUnitOfWork
    {
        private IMongoRepository<Product> _productRepository;
        private IMongoRepository<ApplicationUser> _userRepository;
        private IMongoRepository<IdentityRole> _roleRepository;
        private IMongoRepository<Area> _areaRepository;

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
