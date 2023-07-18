using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AggregatedSearch.Api.Infrastructure
{
    /// <summary>
    /// DocumentFilter для отображения HealthCheck в Swagger
    /// </summary>
    public class PingEndpointDocumentFilter : IDocumentFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
        {
            var pathItem = new OpenApiPathItem();
            var operation = new OpenApiOperation();
            operation.Tags.Add(new OpenApiTag { Name = "HealthCheck" });
            operation.Responses.Add(StatusCodes.Status200OK.ToString(), new OpenApiResponse());
            operation.Responses.Add(StatusCodes.Status500InternalServerError.ToString(), new OpenApiResponse());
            operation.Summary = "Проверка доступности сервиса";
            pathItem.AddOperation(OperationType.Get, operation);
            openApiDocument.Paths.Add("/Ping", pathItem);
        }
    }
}