using AspNet.Identity.MongoDB;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.UnitOfWork;
using AutoDriveEntities;
using AutoDriveServices.MongoIdentity;
using AutoDriveServices.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork { get; }
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ApplicationUserManager UserManager { get; set; }

        public IAuthenticationManager AuthenticationManager { get; set; }

        public async Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id))
            {
                return SignInStatus.LockedOut;
            }
            if (await UserManager.CheckPasswordAsync(user, password))
            {
                return SignInStatus.Success;
                //return await SignInOrTwoFactor(user, isPersistent);
            }
            if (shouldLockout)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await UserManager.AccessFailedAsync(user.Id);
                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    return SignInStatus.LockedOut;
                }
            }
            return SignInStatus.Failure;
        }
                
		public async Task<IdentityResult> RegisterUser(UserEntity userModel)
		{
			ApplicationUser user = new ApplicationUser
			{
				UserName = userModel.UserName,
				Email = userModel.UserName
			};

			var result = await UserManager.CreateAsync(user, userModel.Password);

			return result;
		}
	}
}
