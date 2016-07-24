using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using MongoDB.Driver;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = AutoDriveDataModel.Models;

namespace AutoDriveServices.MasterData
{
    public class AreaService : IAreaService
    {
        private IUnitOfWork _unitOfWork { get; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Public constructor.
        /// </summary>
        public AreaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all the Areas.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetAllAreas()
        {
            var areas = _unitOfWork.GetAreaRepository.FindAll();
            if (areas.Any())
            {
                return AutoMapperSetup.AutoMap.Map<List<Model.Area>, List<AreaEntity>>(areas.ToList());
            }
            return null;
        }

        public AreaEntity GetArea(string id)
        {
            var area = _unitOfWork.GetAreaRepository.GetById(id);
            if (area != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Area, AreaEntity>(area);
            }
            return null;
        }

        public AreaEntity GetAreaByCode(string code)
        {
            var builder = Builders<Model.Area>.Filter;
            var filter = builder.Eq(x => x.AreaCode, code);
            var area = _unitOfWork.GetAreaRepository.GetByFilter(filter).FirstOrDefault();
            if (area != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Area, AreaEntity>(area);
            }
            return null;
        }

        public bool Update(AreaEntity area)
        {
            if (!string.IsNullOrEmpty(area.Id))
            {
                var _area = _unitOfWork.GetAreaRepository.GetById(area.Id);
                if (_area != null)
                {
                    _area.AreaCode = area.AreaCode;
                    _area.Name = area.Name;
                    return Update(_area);
                }
            }
            logger.Log(LogLevel.Info, "No Area found");
            return false;
        }
        public bool Save(AreaEntity area)
        {
            try
            {
                var _area = new Model.Area()
                {
                    AreaCode = area.AreaCode,
                    Name = area.Name
                };
                _unitOfWork.GetAreaRepository.Insert(_area);
                return true;
            }
            catch (Exception ex)
            {
                //log the exception
                return false;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                _unitOfWork.GetAreaRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                return false;
            }
        }
        private bool Update(Model.Area area)
        {
            var builder = Builders<Model.Area>.Filter;
            var filter = builder.Eq(x => x.Id, area.Id);
            var updatebuilder = Builders<Model.Area>.Update;
            var updates = updatebuilder
                .Set(t => t.AreaCode, area.AreaCode)
                .Set(t => t.Name, area.Name);

            return _unitOfWork.GetAreaRepository.Update(filter, updates);
        }
    }
}
