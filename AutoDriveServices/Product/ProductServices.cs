using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = AutoDriveDataModel.Models;

namespace AutoDriveServices.Product
{
    public class ProductServices : IProductServices
    {
        private UnitOfWork _unitOfWork { get; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ProductServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.GetProductRepository.FindAll();
            if (products.Any())
            {   
                var productsModel = AutoMapperSetup.AutoMap.Map<List<Model.Product>, List<ProductEntity>>(products.ToList());
                return productsModel;
            }
            return null;
        }
    }
}
