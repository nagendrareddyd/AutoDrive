using AutoDriveAPI.CustomExceptions;
using AutoDriveAPI.Util;
using AutoDriveEntities;
using AutoDriveServices.Student;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class StudentController : ApiController
    {
        private IStudentService StudentServices { get; set; }

        public StudentController(IStudentService studentService)
        {
            StudentServices = studentService;
        }

        // GET: api/Student
        public HttpResponseMessage Get()
        {
            var result = StudentServices.GetAllStudents();
            if (result != null && result.ToList().Any())
                return Request.CreateResponse(result);
            throw new ApiDataException(9021, Constants.ErrorCode9021, HttpStatusCode.NotFound);
        }

        // GET: api/Student/5
        public HttpResponseMessage Get(string id)
        {
            StudentEntity student = null;
            if (!string.IsNullOrEmpty(id))
            {
                student = StudentServices.GetStudent(id);
            }
            if (student != null)
                return Request.CreateResponse(student);
            throw new ApiDataException(9022, Constants.ErrorCode9022, HttpStatusCode.NotFound);
        }

        [HttpGet]
        public HttpResponseMessage GetByCode(string code)
        {
            StudentEntity student = null;
            if (!string.IsNullOrEmpty(code))
            {
                student = StudentServices.GetStudentByCode(code);
            }
            if (student != null)
                return Request.CreateResponse(student);
            throw new ApiDataException(9022, Constants.ErrorCode9022, HttpStatusCode.NotFound);
        }

        // POST: api/Student
        public HttpResponseMessage Post([FromBody]StudentEntity value)
        {
            var student = StudentServices.GetStudentByCode(value.StudentCode);
            if (student != null)
                throw new ApiDataException(9023, Constants.ErrorCode9023, HttpStatusCode.Conflict);

            if (StudentServices.Save(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // PUT: api/Student/5
        public HttpResponseMessage Put([FromBody]StudentEntity value)
        {
            var student = StudentServices.GetStudent(value.Id);
            if (student == null)
                throw new ApiDataException(9022, Constants.ErrorCode9022, HttpStatusCode.NotFound);

            if (StudentServices.Update(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.SavedSuccessfully);
            }
            throw new ApiDataException(8001, Constants.ErrorCode8001, HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Student/5
        public HttpResponseMessage Delete(string id)
        {
            var student = StudentServices.GetStudent(id);
            if (student == null)
                throw new ApiDataException(9022, Constants.ErrorCode9022, HttpStatusCode.NotFound);

            if (StudentServices.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, Constants.DeletedSuccessfully);
            }
            throw new ApiDataException(8002, Constants.ErrorCode8002, HttpStatusCode.InternalServerError);
        }
    }
}
