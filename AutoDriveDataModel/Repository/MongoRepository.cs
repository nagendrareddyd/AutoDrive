using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using AutoDriveIoc;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
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
        /// <summary>
        /// Get collection by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(string id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                return MongoDB.GetCollection<T>(CollectionName).Find(filter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return default(T);
            }            
        }

        public IList<T> GetByFilter(FilterDefinition<T> filter)
        {               
            return MongoDB.GetCollection<T>(CollectionName).Find(filter).ToList();
        }

        public void Insert(T entity)
        {
            MongoDB.GetCollection<T>(CollectionName).InsertOne(entity);
        }

        public bool Update(FilterDefinition<T> filter,UpdateDefinition<T> update)
        {
            try
            {
                var result = MongoDB.GetCollection<T>(CollectionName).UpdateOne(filter, update);
                if (result.IsAcknowledged)
                {
                    return result.ModifiedCount > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = MongoDB.GetCollection<T>(CollectionName).DeleteOne(filter);
                if(result.IsAcknowledged)
                {
                    return result.DeletedCount == 1;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
                return false;
            }
        }
    }
}