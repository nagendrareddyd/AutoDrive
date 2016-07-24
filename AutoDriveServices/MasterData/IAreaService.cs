using AutoDriveEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices.MasterData
{
    public interface IAreaService
    {
        IEnumerable<AreaEntity> GetAllAreas();
        AreaEntity GetArea(string id);
        AreaEntity GetAreaByCode(string code);
        bool Update(AreaEntity area);
        bool Save(AreaEntity area);
        bool Delete(string id);
    }
}