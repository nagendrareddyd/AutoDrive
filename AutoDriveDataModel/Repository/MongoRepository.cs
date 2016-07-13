using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace AutoDriveDataModel.Repository
{
    public class MongoRepository<T> : IMongoRepository<T> 
    {
        public string CollectionName { get; set; }

        protected IMongoCollection<T> _collection;
        private IMongoDataAccess MongoDataAccessObj { get; }
        private IMongoDatabase MongoDB { get; }
        public MongoRepository(IMongoDataAccess mongoDataAccess)
        {
            MongoDataAccessObj = mongoDataAccess;
            MongoDB = MongoDataAccessObj.GetMongoDB();
        }

        public IQueryable<T> FindAll() 
        {            
            return MongoDB.GetCollection<T>(CollectionName).AsQueryable();
        }
        /// <summary>
		/// Get collection as IMongoCollection
		/// </summary>
		/// <returns>IMongo Collection</returns>
		public IMongoCollection<T> GetAll()
        {
            return MongoDB.GetCollection<T>(CollectionName);
        }
        public async Task<IList<T>> Find(Expression<Func<T, bool>> query)
        {
            // Return the enumerable of the collection
            return await _collection.Find<T>(query).ToListAsync();
        }
    }
}