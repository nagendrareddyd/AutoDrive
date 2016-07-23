using AutoDrive.UnitTests.Helper;
using AutoDrive.UnitTests.TestData;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveDataModel.UnitOfWork;
using AutoDriveServices;
using AutoDriveServices.MasterData;
using AutoDriveServices.Product;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.UnitTests.ServiceTests
{
    [TestFixture]
    public class AreaTests
    {
        #region private variables
        private IAreaService _areaService;
        private IUnitOfWork _unitOfWork;
        private List<Area> _areas;
        private IMongoRepository<Area> _areaRepo;
        #endregion

        [OneTimeSetUp]
        public void Setup()
        {
            AutoMapperSetup.Init();
            _areas = SetUpAreas();
            AutoMapperSetup.Init();
        }
        [SetUp]
        public void ReIntitializeTest()
        {
            _areaRepo = SetUpAreaRepository();
            var unitofwork = new Mock<IUnitOfWork>();
            unitofwork.Setup(s => s.GetAreaRepository).Returns(_areaRepo);
            _unitOfWork = unitofwork.Object;
            _areaService = new AreaService(_unitOfWork);
        }
        [Test]
        public void GetReturnsAreas()
        {
            var areas = _areaService.GetAllAreas();
            var arealist = areas.Select(areaEntity => new Area
            {
                AreaCode = areaEntity.AreaCode,
                Name = areaEntity.Name
            }).ToList();

            Assert.AreEqual(areas.Count(), _areas.Count(),0);
            /*
            var comparer = new ProductComparer();
            NUnit.Framework.CollectionAssert.AreEqual(productlist.OrderBy(p => p, comparer), _products.OrderBy(p => p, comparer), comparer);
            */
        }
        [Test]
        public void GetAllAreasForNull()
        {
            _areas.Clear();
            var areas = _areaService.GetAllAreas();
            Assert.Null(areas);
            SetUpAreas();
        }
        [Test]
        public void GetAreaByRightId()
        {
            var area = _areaService.GetArea("5791e186c0a1342f54f4372b");
            if (area != null)
            {
                Assert.AreEqual(area.AreaCode, _areas.Find(a => a.AreaCode.Contains("EP1")).AreaCode);
            }
            else
            {
                Assert.Fail();
            }
        }
        [TearDown]
        public void DisposeTest()
        {
            _areaService = null;
            _unitOfWork = null;
            _areaRepo = null;
        }
        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _areas = null;
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
