using AutoDriveAPI.CustomExceptions;
using AutoDriveAPI.Util;
using AutoDriveEntities;
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
    public class AreaController : ApiController
    {
        private IAreaService AreaServices { get; set; }
        
        public AreaController(IAreaService areaService)
        {
            AreaServices = areaService;
        }
        [HttpGet]
        // GET: api/Area
        public HttpResponseMessage Get()
        {
            var areas = AreaServices.GetAllAreas();
            if (areas != null && areas.ToList().Any())
            {
                Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return Request.CreateResponse(areas);
            }
            throw new ApiDataException(9001, Constants.ErrorCode9001, HttpStatusCode.NotFound);
        }

        // GET: api/Area/?id=""&code=""
        public HttpResponseMessage Get(string id)
        {
            AreaEntity area = null;
            if (!string.IsNullOrEmpty(id))
            {
                area = AreaServices.GetArea(id);
            }            
            if (area != null)
                return Request.CreateResponse(area);
            throw new ApiDataException(9002, Constants.ErrorCode9002, HttpStatusCode.NotFound);            
        }
        
        [HttpGet]
        public HttpResponseMessage GetByCode(string code)
        {
            AreaEntity area = null;
            if (!string.IsNullOrEmpty(code))
            {
                area = AreaServices.GetAreaByCode(code);
            }
            if (area != null)
                return Request.CreateResponse(area);
            throw new ApiDataException(9002, Constants.ErrorCode9002, HttpStatusCode.NotFound);
        }
        // POST: api/Area
        [HttpPost]
        public HttpResponseMessage Post([FromBody]AreaEntity value)
        {
            var area = AreaServices.GetAreaByCode(value.AreaCode);
            if(area != null)
                throw new ApiDataException(9003, Constants.ErrorCode9003, HttpStatusCode.Conflict);

            if (AreaServices.Save(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // PUT: api/Area/5
        public HttpResponseMessage Put([FromBody]AreaEntity value)
        {
            var area = AreaServices.GetArea(value.Id);
            if (area == null)
                throw new ApiDataException(9002, Constants.ErrorCode9002, HttpStatusCode.NotFound);

            if (AreaServices.Update(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Area/5
        public HttpResponseMessage Delete(string id)
        {
            var area = AreaServices.GetArea(id);
            if (area == null)
                throw new ApiDataException(9002, Constants.ErrorCode9002, HttpStatusCode.NotFound);

            if (AreaServices.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.DeletedSuccessfully);
            }
            throw new ApiDataException(8002, Constants.ErrorCode8002, HttpStatusCode.InternalServerError);
        }
    }
}
