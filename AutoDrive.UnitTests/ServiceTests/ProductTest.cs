using System;
using AutoDriveAPI.Controllers;
using System.Net.Http;
using NUnit.Framework;
using AutoDriveServices.IoCRegistry;
using AutoDriveServices.Product;
using AutoDriveDataModel.UnitOfWork;
using AutoDriveDataModel.Models;
using System.Collections.Generic;
using AutoDriveDataModel.Repository.Interfaces;
using Moq;
using System.Linq;
using AutoDriveEntities;
using AutoDrive.UnitTests.Helper;
using AutoDrive.UnitTests.TestData;
using AutoDriveServices;

namespace AutoDrive.UnitTests.Tests
{    
	[TestFixture]
    public class ProductTest
    {
		#region private variables
		private IProductServices _productService;
		private IUnitOfWork _unitOfWork;
		private List<Product> _products;
		private IMongoRepository<Product> _productRepo;
		#endregion

		[OneTimeSetUp]
        public void Setup()
        {            
			AutoMapperSetup.Init();
			_products = SetUpProducts();
        }
        
		[SetUp]
		public void ReIntitializeTest()
		{
			_productRepo = SetUpProductRepository();
			var unitofwork = new Mock<IUnitOfWork>();
			unitofwork.Setup(s => s.GetProductRepository).Returns(_productRepo);
			_unitOfWork = unitofwork.Object;
			_productService = new ProductServices(_unitOfWork);
		}

        [Test]
        public void GetReturnsProducts()
        {
			var products = _productService.GetAllProducts();
			var productlist = products.Select(productEntity => new Product
			{
				ProductId = productEntity.ProductId,
				ProductName = productEntity.ProductName				
			}).ToList();
			var comparer = new ProductComparer();
			NUnit.Framework.CollectionAssert.AreEqual(productlist.OrderBy(p => p, comparer), _products.OrderBy(p => p, comparer), comparer);

			/*
            // Arrange
            var controller = new ProductsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get();

            // Assert
            Product product;
            Assert.IsTrue(response.TryGetContentValue<Product>(out product));
            Assert.AreEqual(10, product.ProductId);*/
		}

		[TearDown]
		public void DisposeTest()
		{
			_productService = null;
			_unitOfWork = null;
			_productRepo = null;
		}

		[OneTimeTearDown]
		public void DisposeAllObjects()
		{
			_products = null;
		}

		#region private methods
		private IMongoRepository<Product> SetUpProductRepository()
		{
			var mockRepo = new Mock<IMongoRepository<Product>>(MockBehavior.Default);
			mockRepo.Setup(p => p.FindAll()).Returns(_products.AsQueryable());
			return mockRepo.Object;
		}
		private static List<Product> SetUpProducts()
		{
			var prodId = new int();
			var products = DataInitializer.GetAllProducts();
			foreach (Product prod in products)
				prod.ProductId = ++prodId;
			return products;

		}
		#endregion
	}
}
