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
    public class BookingTests
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
        public void GetAllBookingsTest()
        {
            _response = client.GetAsync("booking/").Result;
            var responseResult =
                JsonConvert.DeserializeObject<List<BookingEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
        [Test]
        public void GetBookingByRightIdTest()
        {
            _response = client.GetAsync("booking/579485e63ee651087471a913").Result;
            var responseResult =
                JsonConvert.DeserializeObject<BookingEntity>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Id, "579485e63ee651087471a913");
        }
        [Test]
        public void GetBookingByWrongIdTest()
        {
            _response = client.GetAsync("booking/5791e186c0a1342f").Result;
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
