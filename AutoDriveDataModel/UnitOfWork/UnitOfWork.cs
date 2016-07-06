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
    public class UnitOfWork : IDisposable
    {
        private IMongoRepository<Product> _productRepository;
        
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
