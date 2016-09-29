using AutoDriveAPI.CustomExceptions;
using AutoDriveAPI.Util;
using AutoDriveEntities;
using AutoDriveServices.Instructor;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class InstructorController : ApiController
    {
        private IInstructorService InstructorServices { get; set; }
        public InstructorController(IInstructorService instructorService)
        {
            InstructorServices = instructorService;
        }
        // GET: api/Instructor
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var result = InstructorServices.GetAllInstructors();
            if (result != null && result.ToList().Any())
                return Request.CreateResponse(result);
            throw new ApiDataException(9011, Constants.ErrorCode9011, HttpStatusCode.NotFound);
        }
        [HttpGet]
        public HttpResponseMessage InstructorCode()
        {
            var result = InstructorServices.GetInstructorCode();
            return Request.CreateResponse(result);            
        }
        // GET: api/Instructor/5
        public HttpResponseMessage Get(string id)
        {
            InstructorEntity instructor = null;
            if (!string.IsNullOrEmpty(id))
            {
                instructor = InstructorServices.GetInstructor(id);
            }
            if (instructor != null)
                return Request.CreateResponse(instructor);
            throw new ApiDataException(9012, Constants.ErrorCode9012, HttpStatusCode.NotFound);
        }
        [HttpGet]
        public HttpResponseMessage GetByCode(string code)
        {
            InstructorEntity instructor = null;
            if (!string.IsNullOrEmpty(code))
            {
                instructor = InstructorServices.GetInstructorByCode(code);
            }
            if (instructor != null)
                return Request.CreateResponse(instructor);
            throw new ApiDataException(9012, Constants.ErrorCode9012, HttpStatusCode.NotFound);
        }
        // POST: api/Instructor
        public HttpResponseMessage Post([FromBody]InstructorEntity value)
        {
            var instructor = InstructorServices.GetInstructorByCode(value.InstructorCode);
            if (instructor != null)
                throw new ApiDataException(9013, Constants.ErrorCode9013, HttpStatusCode.Conflict);

            if (InstructorServices.Save(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // PUT: api/Instructor/5
        public HttpResponseMessage Put([FromBody]InstructorEntity value)
        {
            var instructor = InstructorServices.GetInstructor(value.Id);
            if (instructor == null)
                throw new ApiDataException(9012, Constants.ErrorCode9012, HttpStatusCode.NotFound);

            if (InstructorServices.Update(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Instructor/5
        public HttpResponseMessage Delete(string id)
        {
            var instructor = InstructorServices.GetInstructor(id);
            if (instructor == null)
                throw new ApiDataException(9012, Constants.ErrorCode9012, HttpStatusCode.NotFound);

            if (InstructorServices.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.DeletedSuccessfully);
            }
            throw new ApiDataException(8002, Constants.ErrorCode8002, HttpStatusCode.InternalServerError);
        }
    }
}
