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

namespace AutoDriveIntegrationTests
{
    public class ProductTest
    {
        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://testing/";
        private HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
            /*client.DefaultRequestHeaders.Add("Authorization", "Basic YWtoaWw6YWtoaWw=");
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            _response = client.PostAsync("login", null).Result;

            if (_response != null && _response.Headers != null && _response.Headers.Contains("Token") && _response.Headers.GetValues("Token") != null)
            {
                client.DefaultRequestHeaders.Clear();
                _token = ((string[])(_response.Headers.GetValues("Token")))[0];
                client.DefaultRequestHeaders.Add("Token", _token);
            }*/
        }

        [Test]
        public void GetAllProductsTest()
        {
            _response = client.GetAsync("products/product/All/").Result;
            var responseResult =
                JsonConvert.DeserializeObject<List<ProductEntity>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
    }
}
