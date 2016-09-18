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
    public class AreaTests
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
        public void GetAllAreasTest()
        {
            _response = client.GetAsync("area/").Result;            
            var responseResult =
                JsonConvert.DeserializeObject<List<AreaEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
        [Test]
        public void GetAreasByRightIdTest()
        {
            _response = client.GetAsync("area/?id=5791e186c0a1342f54f4372b").Result;
            var responseResult =
                JsonConvert.DeserializeObject<AreaEntity>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.AreaCode, "Ep101");
        }

        [Test]
        public void GetAreasByWrongIdTest()
        {
            _response = client.GetAsync("area/?id=5791e186c0a1342f").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);             
             Assert.AreEqual(responseResult.StatusCode, "9002");
        }

        [Test]
        public void InsertAreaTest()
        {
            var area = new AreaEntity()
            {
                Id = "",
                AreaCode = "ST101",
                Name = "StLeonards"
            };
            var param = JsonConvert.SerializeObject(area);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");            
            _response = client.PostAsync("area/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test]
        public void InsertExistingAreaCodeTest()
        {
            var area = new AreaEntity()
            {
                Id = "",
                AreaCode = "ST101",
                Name = "StLeonards"
            };
            var param = JsonConvert.SerializeObject(area);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PostAsync("area/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.Conflict, _response.StatusCode);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9003");
        }

        [Test]
        public void UpdateAreaTest()
        {
            var area = new AreaEntity()
            {
                Id = "5792e51e3ee651481c1a46ca",
                AreaCode = "ST101",
                Name = "StLeonards01"
            };
            var param = JsonConvert.SerializeObject(area);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PutAsync("area/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test]
        public void UpdateAreaWithNonExistingIdTest()
        {
            var area = new AreaEntity()
            {
                Id = "5792e51e3ee6514",
                AreaCode = "ST101",
                Name = "StLeonards01"
            };
            var param = JsonConvert.SerializeObject(area);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            _response = client.PutAsync("area/", contentPost).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9002");
        }

        [Test]
        public void DeleteAreasByRightIdTest()
        {
            _response = client.DeleteAsync("area/57922c833ee6521f54668ac9").Result;            
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);            
        }
        
        [Test]
        public void DeleteAreasByWrongIdTest()
        {
            _response = client.DeleteAsync("area/57922c833ee6521f54668ac9").Result;
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
            var responseResult =
               JsonConvert.DeserializeObject<ErrorResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(responseResult.StatusCode, "9002");
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
