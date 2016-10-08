using AutoDriveAPI.CustomExceptions;
using AutoDriveAPI.Util;
using AutoDriveEntities;
using AutoDriveServices;
using AutoDriveServices.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class UserController : ApiController
    {
        private IUserService UserServices { get; }

        public UserController(IUserService userservice)
        {
            UserServices = userservice;
        }
        // GET: api/Area
        public HttpResponseMessage Get()
        {
            
            throw new ApiDataException(9001, Constants.ErrorCode9001, HttpStatusCode.NotFound);
        }

        // GET: api/Area/?id=""&code=""
        public HttpResponseMessage Get(string id)
        {
            var result = UserServices.GetUserDetails(id);

            if (result != null)
                return Request.CreateResponse(result);
            throw new ApiDataException(9011, "User Not Found", HttpStatusCode.NotFound);
        }
        
        [HttpGet]
        public HttpResponseMessage GetByCode(string code)
        {
           
            throw new ApiDataException(9002, Constants.ErrorCode9002, HttpStatusCode.NotFound);
        }
        // POST: api/Area
        [HttpPost]
        public HttpResponseMessage Post([FromBody]AreaEntity value)
        {
            
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // PUT: api/Area/5
        public HttpResponseMessage Put([FromBody]AreaEntity value)
        {
            
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Area/5
        public HttpResponseMessage Delete(string id)
        {
            
            throw new ApiDataException(8002, Constants.ErrorCode8002, HttpStatusCode.InternalServerError);
        }
    }
}
