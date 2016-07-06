using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoDriveAPI.Controllers;
using System.Web.Http;
using System.Net.Http;
using NUnit.Framework;
using AutoDriveServices.IoCRegistry;

namespace AutoDriveAPI.Tests
{
    [TestClass]
    public class ProductTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            ServicesIoCRegistry.Init();
        }
        
        [TestMethod]
        public void GetReturnsProducts()
        {
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
    }
}
