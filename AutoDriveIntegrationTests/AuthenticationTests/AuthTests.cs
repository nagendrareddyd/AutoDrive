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

		//[Test]
		public void LoginTest()
		{
			client.DefaultRequestHeaders.Add("grant_type", "password");
			client.DefaultRequestHeaders.Add("username", "nagenda");
			client.DefaultRequestHeaders.Add("password", "nagendra~1");
			_response = client.GetAsync("token").Result;
			var responseResult =
				JsonConvert.DeserializeObject<List<ProductEntity>>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseResult.Any(), true);
		}

		[Test]
		public void RegisterUserTest()
		{				
			StringContent data = new StringContent("{\"userName\":\"nage1234\",\"password\":\"nagendra~1\",\"confirmPassword\":\"nagendra~1\"}", Encoding.UTF8,"application/json");
			_response = client.PostAsync("Register", data).Result; //GetAsync("accounts/register").Result;
			/*var responseResult =
				JsonConvert.DeserializeObject<List<UserEntity>>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseResult.Any(), true);*/
		}
	}
}
