using AutoDriveAPI.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AutoDriveAPI.Repository
{
    public class MongoDataAccess : IMongoDataAccess
    {
        public IMongoDatabase GetMongoDB()
        {
            var _client = new MongoClient(ConfigurationManager.AppSettings["MongoConnection"].ToString()); // "mongodb://localhost:27017");
            var MongoDB = _client.GetDatabase(ConfigurationManager.AppSettings["MongoDB"].ToString());
            return MongoDB;
        }
    }
}