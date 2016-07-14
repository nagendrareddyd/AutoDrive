using AutoDriveEntities;
using AutoDriveServices.MongoIdentity;
using AutoDriveServices.Util;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices
{
    public interface IUserService
    {
		ApplicationUserManager UserManager { get; set; }
		Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);
        ClientEntity FindClient(string clientId);
		Task<IdentityResult> RegisterUser(UserEntity userModel);

	}
}
