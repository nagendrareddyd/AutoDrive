using AutoDriveEntities;
using System.Collections.Generic;

namespace AutoDriveServices.Suburb
{
    public interface ISuburbService
    {
        IEnumerable<SuburbEntity> GetAllSuburbs();
        SuburbEntity GetSuburb(string id);
        bool Update(SuburbEntity suburb);
        bool Save(SuburbEntity suburb);
        bool Delete(string id);
    }
}
