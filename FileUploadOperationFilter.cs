using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasFileUpload = context.MethodInfo.GetParameters()
            .Any(p => p.ParameterType == typeof(IFormFile) ||
                      p.ParameterType == typeof(List<IFormFile>) ||
                      p.ParameterType == typeof(IFormFile[]) ||
                      (p.ParameterType.IsClass && p.ParameterType.GetProperties().Any(prop =>
                          prop.PropertyType == typeof(IFormFile) ||
                          prop.PropertyType == typeof(List<IFormFile>) ||
                          prop.PropertyType == typeof(IFormFile[]))));

        if (!hasFileUpload)
            return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = {
                            ["media"] = new OpenApiSchema
                            {
                                Type = "array",
                                Items = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            }
                        },
                        Required = new HashSet<string> { "media" }
                    }
                }
            }
        };
    }
}