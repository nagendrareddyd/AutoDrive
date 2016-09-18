using System;
using System.Collections.Generic;
using AutoDriveEntities;
using AutoDriveDataModel.UnitOfWork;
using Model = AutoDriveDataModel.Models;
using System.Linq;
using NLog;
using MongoDB.Driver;

namespace AutoDriveServices.Address
{
    public class AddressService : IAddressService
    {
        private IUnitOfWork _unitOfWork { get; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Delete(string id)
        {
            try
            {
                _unitOfWork.GetAddressRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                return false;
            }

        }

        public IEnumerable<AddressEntity> GetAll()
        {
            var states = _unitOfWork.GetAddressRepository.FindAll();
            if (states.Any())
            {
                return AutoMapperSetup.AutoMap.Map<List<Model.Address>, List<AddressEntity>>(states.ToList());
            }
            return null;
        }

        public bool Save(AddressEntity address)
        {
            try
            {
                var _address = new Model.Address()
                {
                    Street = address.Street,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode
                };
                _unitOfWork.GetAddressRepository.Insert(_address);
                return true;
            }
            catch (Exception ex)
            {
                //log the exception
                return false;
            }
        }

        public bool Update(AddressEntity address)
        {
            try
            {
                var _address = new Model.Address()
                {
                    Id = address.Id,
                    City = address.City,
                    State =address.State,
                    Street = address.Street,
                    PostalCode =address.PostalCode
                };
                _unitOfWork.GetAddressRepository.Insert(_address);
                return true;
            }
            catch (Exception ex)
            {
                //log the exception
                return false;
            }
        }

        public AddressEntity GetAddress(string id)
        {
            var address = _unitOfWork.GetAddressRepository.GetById(id);
            if (address != null)
            {
                return AutoMapperSetup.AutoMap.Map<Model.Address, AddressEntity>(address);
            }
            return null;
        }
    }
}
