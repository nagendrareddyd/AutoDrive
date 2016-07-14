using AutoDriveEntities;
using AutoDriveServices;
using AutoDriveServices.MongoIdentity;
using AutoDriveServices.Product;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class AccountsController : ApiController
    {
		private IUserService UserServices { get; }

		public AccountsController(IUserService userservice)
		{
			UserServices = userservice;
		}
		private ApplicationUserManager _userManager;
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}
		private IAuthenticationManager Authentication
		{
			get { return Request.GetOwinContext().Authentication; }
		}
		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(UserEntity userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			UserServices.UserManager = UserManager;
			IdentityResult result = await UserServices.RegisterUser(userModel);

			IHttpActionResult errorResult = GetErrorResult(result);

			if (errorResult != null)
			{
				return errorResult;
			}

			return Ok();
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}

	}
}
