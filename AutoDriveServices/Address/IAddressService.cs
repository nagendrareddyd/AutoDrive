using AutoDriveEntities;
using System.Collections.Generic;

namespace AutoDriveServices.Address
{
    public interface IAddressService
    {
        IEnumerable<AddressEntity> GetAll();
        AddressEntity GetAddress(string id);
        bool Update(AddressEntity state);
        bool Save(AddressEntity state);
        bool Delete(string id);
    }
}
