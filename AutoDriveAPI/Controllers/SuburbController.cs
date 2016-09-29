using AutoDriveServices.Suburb;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class SuburbController : ApiController
    {
        
        private ISuburbService SuburbService { get; set; }
       
        public SuburbController(ISuburbService suburbservice)
        {
            SuburbService = suburbservice;            
        }
        // GET: Suburb      
     
    }
}