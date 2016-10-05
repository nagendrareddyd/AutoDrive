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
    public class InstructorTests
    {
        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://autodrive/api/";
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
        public void GetAllInstructorTest()
        {
            _response = client.GetAsync("instructor/").Result;
            var responseResult =
                JsonConvert.DeserializeObject<List<InstructorEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
        [Test]
        public void GetInstructorByRightIdTest()
        {
            _response = client.GetAsync("instructor/579375ff3ee6514914cb2822").Result;
            var responseResult =
                JsonConvert.DeserializeObject<InstructorEntity>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.InstructorCode, "INS101");
        }

        [Test]
        public void GetInstructorByWrongIdTest()
        {
            _response = client.GetAsync("instructor/5791e186c0a1342f").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9012");
        }

        [Test]
        public void InsertInstructorTest()
        {
            var instructor = new InstructorEntity()
            {
                Id = "",
                Address = "U6, 8 Herbert St",
                Email = "ravikumar@gmail.com",
                Gender="Male",
                Home = "",
                Mobile="0455522425",
                InstructorCode="INS1020",
                FirstName="Ravi Kumar",
                LastName="Kammari",
                Status="Active",
                Suburb = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Parrramata"
                },
                Areas = new List<AreaEntity>()
                {
                    new AreaEntity()
                    {
                        AreaCode = "EP101",
                        Name = "Epping101"
                    },
                    new AreaEntity()
                    {
                        AreaCode = "EP102",
                        Name = "Epping102"
                    }
                }
            };

            var param = JsonConvert.SerializeObject(instructor);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PostAsync("instructor/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test]
        public void InsertExistingInstructorCodeTest()
        {
            var instructor = new InstructorEntity()
            {
                Id = "57941c103ee6510e00e246eb",
                Address = "U6, 8 Herbert St",
                Email = "ravikumar@gmail.com",
                Gender = "Male",
                Home = "",
                Mobile = "0455522425",
                InstructorCode = "INS102",
                FirstName = "Ravi Kumar",
                Status = "Active",
                Suburb = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Parrramata"
                },
                Areas = new List<AreaEntity>()
                {
                    new AreaEntity()
                    {
                        AreaCode = "EP101",
                        Name = "Epping101"
                    },
                    new AreaEntity()
                    {
                        AreaCode = "EP102",
                        Name = "Epping102"
                    }
                }
            };
            var param = JsonConvert.SerializeObject(instructor);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PostAsync("instructor/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.Conflict, _response.StatusCode);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9013");
        }

        [Test]
        public void UpdateInstructorTest()
        {
            var instructor = new InstructorEntity()
            {
                Id = "57941c823ee6510e00e246ec",
                Address = "U6, 8 Herbert St",
                Email = "ravikumar533@gmail.com",
                Gender = "Male",
                Home = "",
                Mobile = "0455522425",
                InstructorCode = "INS102",
                FirstName = "Ravi Kumar",
                Status = "Active",
                Suburb = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Parrramata"
                },
                Areas = new List<AreaEntity>()
                {
                    new AreaEntity()
                    {
                        AreaCode = "EP101",
                        Name = "Epping101"
                    },
                    new AreaEntity()
                    {
                        AreaCode = "EP102",
                        Name = "Epping102"
                    }
                }
            };
            var param = JsonConvert.SerializeObject(instructor);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PutAsync("instructor/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test]
        public void UpdateInstructorWithNonExistingIdTest()
        {
            var instructor = new InstructorEntity()
            {
                Id = "212121211212",
                Address = "U6, 8 Herbert St",
                Email = "ravikumar@gmail.com",
                Gender = "Male",
                Home = "",
                Mobile = "0455522425",
                InstructorCode = "INS102",
                FirstName = "Ravi Kumar",
                Status = "Active",
                Suburb = new SuburbEntity()
                {
                    PostalCode = "2011",
                    SuburbName = "Parrramata"
                },
                Areas = new List<AreaEntity>()
                {
                    new AreaEntity()
                    {
                        AreaCode = "EP101",
                        Name = "Epping101"
                    },
                    new AreaEntity()
                    {
                        AreaCode = "EP102",
                        Name = "Epping102"
                    }
                }
            };
            var param = JsonConvert.SerializeObject(instructor);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PutAsync("instructor/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9012");
        }

        [Test]
        public void DeleteInstructorsByRightIdTest()
        {
            _response = client.DeleteAsync("instructor/57941c103ee6510e00e246eb").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void DeleteInstructorsByWrongIdTest()
        {
            _response = client.DeleteAsync("instructor/57922c833e1f54668ac9").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9012");
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
