﻿using AutoDriveAPI.Ioc;
using AutoDriveAPI.Models;
using AutoDriveAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private IMongoRepository<Product> MongoRepositoryObj = IocContainer.Resolve<IMongoRepository<Product>>();
        
        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            var data = MongoRepositoryObj.FindAll("Products");
            return data;
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
