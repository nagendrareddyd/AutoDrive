using AutoDriveAPI.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace AutoDriveAPI.Repository
{
    public class MongoRepository<T> : IMongoRepository<T>
    {
        protected IMongoCollection<T> _collection;
        private IMongoDataAccess MongoDataAccessObj { get; }
        private IMongoDatabase MongoDB { get; }
        public MongoRepository(IMongoDataAccess mongoDataAccess)
        {
            MongoDataAccessObj = mongoDataAccess;
            MongoDB = MongoDataAccessObj.GetMongoDB();
        }

        public IQueryable<T> FindAll(string collectionName)
        {
            return MongoDB.GetCollection<T>(collectionName).AsQueryable();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> query)
        {
            // Return the enumerable of the collection
            return await _collection.Find<T>(query).ToListAsync();
        }
    }
}