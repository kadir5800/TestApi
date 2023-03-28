using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiCore.Infrastructure.Swager
{
    public class HeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null || operation.Parameters.Count == 0)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Zapi-Token",
                    In = ParameterLocation.Header,
                    Description = "Önceden Edinmiş Olduğunuz Token",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString("0f422e864dfbe3cdbb57c3edd9c0653c15219de7"),
                    }
                });
                //operation.Parameters.Add(new OpenApiParameter
                //{
                //    Name = "TestApi-Client",
                //    In = ParameterLocation.Header,
                //    Description = "istekte bulunan istemci tipi",
                //    Required = true,
                //    Schema = new OpenApiSchema
                //    {
                //        Type = "string",
                //        Default = new OpenApiString("TestApiWeb"),
                //    }
                //});
            }
        }
    }
}
