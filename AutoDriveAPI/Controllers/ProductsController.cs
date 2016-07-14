using AutoDriveAPI.CustomExceptions;
using AutoDriveServices.Product;
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
        private IProductServices ProductServices { get; }

        public ProductsController(IProductServices productRepository)
        {
            ProductServices = productRepository;
        }

        [Authorize]
        // GET: api/Products           
        public HttpResponseMessage Get()
        {            
            var data = ProductServices.GetAllProducts();    
            if(data != null && data.ToList().Any())        
                return Request.CreateResponse(data);
            throw new ApiDataException(9001, "No products found", HttpStatusCode.NotFound); 
            
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
