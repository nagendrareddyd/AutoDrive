﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveAPI.Repository.Interfaces
{
    interface IMongoRepository<T>
    {
        IQueryable<T> FindAll(string collectionName);
    }
}
