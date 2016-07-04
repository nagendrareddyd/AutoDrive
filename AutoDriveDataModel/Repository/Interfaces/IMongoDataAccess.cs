using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDriveDataModel.Repository.Interfaces
{
    public interface IMongoDataAccess
    {
        IMongoDatabase GetMongoDB();
    }
}