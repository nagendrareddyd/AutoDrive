using System;
using System.Collections.Generic;
using AutoDriveEntities;
using AutoDriveDataModel.UnitOfWork;
using Model = AutoDriveDataModel.Models;
using System.Linq;
using NLog;
using MongoDB.Driver;
using MongoDB.Bson;

namespace AutoDriveServices.Suburb
{
    public class SuburbService : ISuburbService
    {
        private IUnitOfWork _unitOfWork { get; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SuburbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Delete(string id)
        {
            try
            {
                _unitOfWork.GetSuburbRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                return false;
            }
        }

        public IEnumerable<SuburbEntity> GetAllSuburbs()
        {            
            var suburbs = _unitOfWork.GetSuburbRepository.FindAll();
            if (suburbs.Any())
            {
                return AutoMapperSetup.AutoMap.Map<List<Model.Suburb>, List<SuburbEntity>>(suburbs.ToList());
            }            
            return null;            
        }

        public SuburbEntity GetSuburb(string id)
        {
            var suburb = _unitOfWork.GetSuburbRepository.GetById(id);
            if (suburb != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Suburb, SuburbEntity>(suburb);
            }
            return null;
        }

        public bool Save(SuburbEntity suburb)
        {
            try
            {
                var _suburb = new Model.Suburb()
                {
                    PostCode = suburb.PostCode,
                    SuburbName = suburb.Suburb
                };
                _unitOfWork.GetSuburbRepository.Insert(_suburb);
                return true;
            }
            catch (Exception ex)
            {
                //log the exception
                return false;
            }
        }

        public bool Update(SuburbEntity suburb)
        {
            if (!string.IsNullOrEmpty(suburb.Id))
            {
                var _suburb = _unitOfWork.GetSuburbRepository.GetById(suburb.Id);
                if (_suburb != null)
                {
                    
                    return Update(_suburb);
                }
            }
            logger.Log(LogLevel.Info, "No Student found");
            return false;
        }
		public IEnumerable<SuburbEntity> GetMatchedSuburbs(string contains)
		{
			var st = ".*"+ contains + ".*";
			var regex = new BsonRegularExpression(st);
			var builder = Builders<Model.Suburb>.Filter;            
            var filter = builder.Regex("Display",new BsonRegularExpression(st));            
            var suburbs = _unitOfWork.GetSuburbRepository.GetByFilter(filter);
			if (suburbs.Any())
			{
				return AutoMapperSetup.AutoMap.Map<List<Model.Suburb>, List<SuburbEntity>>(suburbs.ToList());
			}
			return null;
		}
		private bool Update(Model.Suburb suburb)
        {
            var builder = Builders<Model.Suburb>.Filter;
            var filter = builder.Eq(x => x.SuburbName, suburb.SuburbName);
            var updatebuilder = Builders<Model.Suburb>.Update;
            var updates = updatebuilder
                .Set(t => t.SuburbName, suburb.SuburbName)
                .Set(t => t.PostCode, suburb.PostCode);
            return _unitOfWork.GetSuburbRepository.Update(filter, updates);
        }
    }
}
