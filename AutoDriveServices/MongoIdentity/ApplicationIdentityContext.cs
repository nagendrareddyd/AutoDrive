using AspNet.Identity.MongoDB;
using AutoDriveDataModel.Models;
using AutoDriveDataModel.UnitOfWork;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoDriveServices.MongoIdentity
{
    public class ApplicationIdentityContext : IDisposable
    {
        public static ApplicationIdentityContext Create()
        {
            // todo add settings where appropriate to switch server & database in your own application
            var unitofwork = AutoDriveIoc.IocContainer.Resolve<IUnitOfWork>();
            var users = unitofwork.GetUserRepository.GetAll();
            var roles = unitofwork.GetRoleRepository.GetAll();
            return new ApplicationIdentityContext(users, roles);            
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, IMongoCollection<IdentityRole> roles)
        {
            Users = users;
            Roles = roles;
        }

        public IMongoCollection<IdentityRole> Roles { get; set; }

        public IMongoCollection<ApplicationUser> Users { get; set; }

        public Task<List<IdentityRole>> AllRolesAsync()
        {
            return Roles.Find(r => true).ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}
