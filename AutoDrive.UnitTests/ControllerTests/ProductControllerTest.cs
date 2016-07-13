using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveDataModel.UnitOfWork;
using AutoDriveServices.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AutoDrive.UnitTests.TestData;
using Moq;
using AutoDriveAPI.Controllers;
using System.Web.Http;
using System.Web.Http.Hosting;
using Newtonsoft.Json;
using System.Net;
using AutoDrive.UnitTests.Helper;

namespace AutoDrive.UnitTests.ControllerTests
{
    public class ProductControllerTest
    {
        private IProductServices _productService;        
        private IUnitOfWork _unitOfWork;
        private List<Product> _products;        
        private IMongoRepository<Product> _productRepository;                
        private HttpClient _client;
        private HttpResponseMessage _response;        
        private const string ServiceBaseURL = "http://localhost:50875/";

        [OneTimeSetUp]
        public void Setup()
        {
            _products = SetUpProducts();
            _productRepository = SetUpProductRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.GetProductRepository).Returns(_productRepository);            
            _unitOfWork = unitOfWork.Object;
            _productService = new ProductServices(_unitOfWork);
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        [SetUp]
        public void ReInitialiseTest()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        [Test]
        public void GetAllProducts()
        {
            var productController = new ProductsController(_productService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "api/products")
                }
            };
            productController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _response = productController.Get();
            var responseResult = JsonConvert.DeserializeObject<List<Product>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
            var comparer = new ProductComparer();
            CollectionAssert.AreEqual(
            responseResult.OrderBy(product => product, comparer),
            _products.OrderBy(product => product, comparer), comparer);
        }

        [TearDown]
        public void DisposeTest()
        {
            if (_client != null)
                _client = null;
            if (_response != null)
                _response = null;
        }

        [OneTimeTearDown]
        public void DisposeObjects()
        {
            _products = null;
            _productRepository = null;
            _unitOfWork = null;
            if (_client != null)
                _client = null;
            if (_response != null)
                _response = null;
        }

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
    }
}
