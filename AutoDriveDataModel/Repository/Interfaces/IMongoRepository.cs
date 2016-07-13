using AutoDriveDataModel.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.Repository.Interfaces
{
    public interface IMongoRepository<T> 
    {
        IQueryable<T> FindAll();
        string CollectionName { get; set; }
        IMongoCollection<T> GetAll();
    }
}
