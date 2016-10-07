using AutoDriveAPI.CustomExceptions;
using AutoDriveAPI.Util;
using AutoDriveServices.Suburb;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoDriveEntities;
using System.Collections.Generic;

namespace AutoDriveAPI.Controllers
{
    public class SuburbController : ApiController
    {        
        private ISuburbService SuburbService { get; set; }
       
        public SuburbController(ISuburbService suburbservice)
        {
            SuburbService = suburbservice;            
        }
		// GET: Suburb 
        [HttpGet]     
		public HttpResponseMessage Get(string contains)
		{
			var result = SuburbService.GetMatchedSuburbs(contains);
			if (result != null)
				return Request.CreateResponse(result);
			throw new ApiDataException(9011, Constants.ErrorCode9011, HttpStatusCode.NotFound);
		}

        public HttpResponseMessage Get()
        {
            throw new ApiDataException(9011, Constants.ErrorCode9011, HttpStatusCode.NotFound);
        }
    }
}