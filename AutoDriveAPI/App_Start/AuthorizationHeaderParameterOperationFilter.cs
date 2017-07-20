using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace AutoDriveAPI.App_Start
{
    /// <summary>
    /// Ioperation filter for adding azuthorization header
    /// </summary>
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                description = "access token",
                required = true,
                type = "string",
                @in = "header"
            });
        }
    }
}