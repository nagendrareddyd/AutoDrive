using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using AutoDriveEntities;
using Newtonsoft.Json;
using System.Net;
using System.Configuration;

namespace AutoDriveIntegrationTests
{
    public class ProductTest
    {
        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://testing/api/";
        private HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
            // replace the token  as per the request
            var token = ConfigurationManager.AppSettings["AppToken"].ToString();
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

       // [Test]
        public void GetAllProductsTest()
        {
            _response = client.GetAsync("products/").Result;
            var responseResult =
                JsonConvert.DeserializeObject<List<ProductEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
    }
}
