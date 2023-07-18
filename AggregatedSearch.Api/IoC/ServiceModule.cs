using AggregatedSearch.BusinessLogic;
using AggregatedSearch.BusinessLogic.Services.RouteCacheService;
using Autofac;

namespace AggregatedSearch.Api.IoC
{
    /// <summary>
    /// DI модуль с сервисами
    /// </summary>
    public class ServiceModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(BusinessLogicAssembly.Value)
                .Where(x => x.Name.EndsWith("Service"))
                .Except<RouteCacheService>(x => x.AsImplementedInterfaces().SingleInstance())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}