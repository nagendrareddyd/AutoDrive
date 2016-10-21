using AutoDriveDataModel.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Web;

namespace AutoDriveDataModel.Repository
{
    public class MongoDataAccess : IMongoDataAccess
    {
        public IMongoDatabase GetMongoDB()
        {
            //var _client = new MongoClient(ConfigurationManager.AppSettings["MongoConnection"].ToString()); // "mongodb://localhost:27017");
            var _username = ConfigurationManager.AppSettings["MongoDBUserName"].ToString();
            var _password = ConfigurationManager.AppSettings["MongoDBPassword"].ToString();
            var _host = ConfigurationManager.AppSettings["MongoDBHost"].ToString();
            var _dbname = ConfigurationManager.AppSettings["MongoDB"].ToString();
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(_host, 10250);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(_dbname, _username);
            MongoIdentityEvidence evidence = new PasswordEvidence(_password);

            settings.Credentials = new List<MongoCredential>()
                {
                    new MongoCredential("SCRAM-SHA-1", identity, evidence)
                };
            var _client = new MongoClient(settings);
            var MongoDB = _client.GetDatabase(ConfigurationManager.AppSettings["MongoDB"].ToString());
            return MongoDB;
        }
    }
}