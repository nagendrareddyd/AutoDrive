using AutoDrive.UnitTests.TestData;
using AutoDriveAPI.Controllers;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using AutoDriveServices;
using AutoDriveServices.MasterData;
using AutoDriveServices.Product;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace AutoDrive.UnitTests.ControllerTests
{
    public class AreaControllerTests
    {
        private IAreaService _areaService;
        private IUnitOfWork _unitOfWork;
        private List<Area> _areas;
        private IMongoRepository<Area> _areaRepository;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://localhost:50875/";

        [OneTimeSetUp]
        public void Setup()
        {
            _areas = SetUpAreas();
            _areaRepository = SetUpAreaRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.GetAreaRepository).Returns(_areaRepository);
            _unitOfWork = unitOfWork.Object;
            _areaService = new AreaService(_unitOfWork);
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
            AutoMapperSetup.Init();
        }
        [SetUp]
        public void ReInitialiseTest()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }
        [Test]
        public void GetAllAreas()
        {
            var areaController = new AreaController(_areaService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "api/area")
                }
            };
            areaController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _response = areaController.Get();
            var responseResult = JsonConvert.DeserializeObject<List<AreaEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
            /*var comparer = new ProductComparer();
            CollectionAssert.AreEqual(
            responseResult.OrderBy(product => product, comparer),
            _products.OrderBy(product => product, comparer), comparer);*/
        }

        [Test]
        public void GetAreaByRightId()
        {
            var areaController = new AreaController(_areaService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "api/area")
                }
            };
            areaController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _response = areaController.Get("5791e186c0a1342f54f4372b");
            var responseResult = JsonConvert.DeserializeObject<AreaEntity>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual("EP1", responseResult.AreaCode);            
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
            _areas = null;
            _areaRepository = null;
            _unitOfWork = null;
            if (_client != null)
                _client = null;
            if (_response != null)
                _response = null;
        }

        #region private methods
        private IMongoRepository<Area> SetUpAreaRepository()
        {
            var mockRepo = new Mock<IMongoRepository<Area>>(MockBehavior.Default);
            mockRepo.Setup(p => p.FindAll()).Returns(_areas.AsQueryable());
            mockRepo.Setup(p => p.GetById(It.IsAny<string>())).Returns(new Func<string, Area>(id => _areas.Find(p => p.Id.ToString() == id)));
            return mockRepo.Object;
        }

        private static List<Area> SetUpAreas()
        {
            return DataInitializer.GetAllAreas();
        }
        #endregion
    }
}
