using AutoDriveEntities;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveIntegrationTests
{
    public class StudentTests
    {
        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://testing/api/";
        private HttpClient client;
        private string token;
        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
            // replace the token  as per the request
            token = ConfigurationManager.AppSettings["AppToken"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", token);
            /*MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            _response = client.PostAsync("login", null).Result;

            if (_response != null && _response.Headers != null && _response.Headers.Contains("Token") && _response.Headers.GetValues("Token") != null)
            {
                client.DefaultRequestHeaders.Clear();
                _token = ((string[])(_response.Headers.GetValues("Token")))[0];
                client.DefaultRequestHeaders.Add("Token", _token);
            }*/
        }

        [SetUp]
        public void ReInitializeTest()
        {
            client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
            client.DefaultRequestHeaders.Add("Authorization", token);
        }

        [Test]
        public void GetAllStudentsTest()
        {
            _response = client.GetAsync("student/").Result;
            var responseResult =
                JsonConvert.DeserializeObject<List<StudentEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
        [Test]
        public void GetStudentByRightIdTest()
        {
            _response = client.GetAsync("student/579375ff3ee6514914cb2822").Result;
            var responseResult =
                JsonConvert.DeserializeObject<StudentEntity>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.StudentCode, "ST101");
        }
        [Test]
        public void GetStudentByWrongIdTest()
        {
            _response = client.GetAsync("student/579375ff3ee6514922").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9022");
        }
        [Test]
        public void InsertStudentTest()
        {
            var student = new StudentEntity()
            {
                DOB = "18/07/1983",
                Email = "ravi@gmail.com",
                FirstName = "S_ravikumar",
                Gender = "Male",
                Instructor = new StudentInstructor
                {
                    Id = "579375ff3ee6514914cb2822",
                    InstructorName = "Nagi"
                },
                LicenceCountry = "Australia",
                LicenceExpireOn = "18/07/2019",
                LicenceNumber = "25556324",
                LicenceState = "NSW",
                Mobile ="04041525636",
                PickUpLocation = "near chatswood station",
                Status = "Active",
                StudentCode = "ST102",
                Suburbs = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Artarmon"
                }
            };
            var param = JsonConvert.SerializeObject(student);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PostAsync("student/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test]
        public void InsertExistingStudentCodeTest()
        {
            var student = new StudentEntity()
            {
                Id= "57946b9a3ee6515e7c077c9a",
                DOB = "18/07/1983",
                Email = "ravi@gmail.com",
                FirstName = "S_ravikumar",
                Gender = "Male",
                Instructor = new StudentInstructor
                {
                    Id = "579375ff3ee6514914cb2822",
                    InstructorName = "Nagi"
                },
                LicenceCountry = "Australia",
                LicenceExpireOn = "18/07/2019",
                LicenceNumber = "25556324",
                LicenceState = "NSW",
                Mobile = "04041525636",
                PickUpLocation = "near chatswood station",
                Status = "Active",
                StudentCode = "ST102",
                Suburbs = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Artarmon"
                }
            };
            var param = JsonConvert.SerializeObject(student);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PostAsync("student/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.Conflict, _response.StatusCode);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9023");
        }
        [Test]
        public void UpdateStudentTest()
        {
            var student = new StudentEntity()
            {
                Id = "57946b9a3ee6515e7c077c9a",
                DOB = "18/07/1983",
                Email = "ravi@gmail.com.au",
                FirstName = "S_ravikumar",
                Gender = "Male",
                Instructor = new StudentInstructor
                {
                    Id = "579375ff3ee6514914cb2822",
                    InstructorName = "Nagi"
                },
                LicenceCountry = "Australia",
                LicenceExpireOn = "18/07/2019",
                LicenceNumber = "25556324",
                LicenceState = "NSW",
                Mobile = "04041525636",
                PickUpLocation = "near chatswood station",
                Status = "Active",
                StudentCode = "ST102",
                Suburbs = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Artarmon"
                }
            };
            var param = JsonConvert.SerializeObject(student);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PutAsync("student/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
        [Test]
        public void UpdateStudentWithNonExistingIdTest()
        {
            var student = new StudentEntity()
            {
                Id = "57946b9a3ee6515e7c077",
                DOB = "18/07/1983",
                Email = "ravi@gmail.com",
                FirstName = "S_ravikumar",
                Gender = "Male",
                Instructor = new StudentInstructor
                {
                    Id = "579375ff3ee6514914cb2822",
                    InstructorName = "Nagi"
                },
                LicenceCountry = "Australia",
                LicenceExpireOn = "18/07/2019",
                LicenceNumber = "25556324",
                LicenceState = "NSW",
                Mobile = "04041525636",
                PickUpLocation = "near chatswood station",
                Status = "Active",
                StudentCode = "ST102",
                Suburbs = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Artarmon"
                }
            };
            var param = JsonConvert.SerializeObject(student);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PutAsync("student/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9022");
        }
        [Test]
        public void DeleteStudentByRightIdTest()
        {
            _response = client.DeleteAsync("student/57946b9a3ee6515e7c077c9a").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void DeleteStudentByWrongIdTest()
        {
            _response = client.DeleteAsync("student/57922c833e1f58ac9").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9022");
        }
        [TearDown]
        public void DisposeTest()
        {
            if (_response != null)
                _response.Dispose();
            if (client != null)
                client.Dispose();
        }

        [OneTimeTearDown]
        public void DisposeObjects()
        {
            client = null;
        }
    }
}
