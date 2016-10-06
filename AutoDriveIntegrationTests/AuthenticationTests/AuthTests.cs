using AutoDriveEntities;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveIntegrationTests.AuthenticationTests
{
	public class AuthTests
	{
		private HttpResponseMessage _response;
		private const string ServiceBaseURL = "http://Autodrive/";
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
		public void LoginTest()
		{
            var parameters = new FormUrlEncodedContent(new[] {
                                    new KeyValuePair<string, string>("grant_type", "password"),
                                    new KeyValuePair<string, string>("userName", "nage224@here.com"),
                                    new KeyValuePair<string, string>("password", "Nagendra~1")                                    
                                    });
            _response = client.PostAsync("token", parameters).Result;			
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);			
		}

        [Test]
        public void RegisterUserTest()
        {
            var parameters = new FormUrlEncodedContent(new[] {
                                    new KeyValuePair<string, string>("userName", "nage224@here.com"),
                                    new KeyValuePair<string, string>("password", "Nagendra~1"),
                                    new KeyValuePair<string, string>("confirmPassword", "Nagendra~1")
                                    });
            _response = client.PostAsync("Register", parameters).Result; 
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }
	}
}
