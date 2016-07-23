using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveIntegrationTests
{
    public class ErrorResponse
    {
        /* {
              "statusCode": 9002,
              "statusMessage": "No Area found",
              "reasonPhrase": "ApiDataException"
         }*/
        
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string ReasonPhrase { get; set; }
    }
}
