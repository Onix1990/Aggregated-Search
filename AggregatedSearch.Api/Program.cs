using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AggregatedSearch.Api.Infrastructure;
using AggregatedSearch.Api.IoC;
using AggregatedSearch.BusinessLogic;
using AggregatedSearch.HealthChecks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
    {
        x.DocumentFilter<PingEndpointDocumentFilter>();
        var currentAssembly = Assembly.GetExecutingAssembly();
        var xmlDocs = currentAssembly
            .GetReferencedAssemblies()
            .Union(new AssemblyName[]
            {
                currentAssembly.GetName()
            })
            .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? string.Empty, $"{a.Name}.xml"))
            .Where(File.Exists).ToArray();
        Array.ForEach(xmlDocs, d => { x.IncludeXmlComments(d); });
    }
);
builder.Services.AddHealthChecks().AddCheck<SearchServiceHealthCheck>(nameof(SearchServiceHealthCheck));
builder.Services.AddAutoMapper(BusinessLogicAssembly.Value);
builder.Services.AddMemoryCache();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new AppModule()));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/ping", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
        [HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError
    }
});
app.Run();