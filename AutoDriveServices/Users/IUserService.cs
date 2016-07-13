using AutoDriveEntities;
using AutoDriveServices.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices
{
    public interface IUserService
    {
        Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);
        ClientEntity FindClient(string clientId);
    }
}
