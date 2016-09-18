using AutoDriveServices.Address;
using AutoDriveServices.Suburb;
using System.Web.Http;

namespace AutoDriveAPI.Controllers
{
    public class SuburbController : ApiController
    {
        
        private ISuburbService SuburbService { get; set; }
        private IAddressService StateService { get; set; }

        public SuburbController(ISuburbService suburbservice,IAddressService stateservice)
        {
            SuburbService = suburbservice;
            StateService = stateservice;
        }
        // GET: Suburb      
     
    }
}