using AspNet.Identity.MongoDB;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.UnitOfWork
{
	public interface IUnitOfWork
	{
		IMongoRepository<Product> GetProductRepository { get; }
        IMongoRepository<ApplicationUser> GetUserRepository { get; }
        IMongoRepository<IdentityRole> GetRoleRepository { get; }
        IMongoRepository<Client> GetClientRepository { get; }
    }
}
