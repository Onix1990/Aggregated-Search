using AggregatedSearch.BusinessLogic;
using Autofac;

namespace AggregatedSearch.Api.IoC
{
    /// <summary>
    /// DI модуль с клиентами
    /// </summary>
    public class ClientModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(BusinessLogicAssembly.Value)
                .Where(x => x.Name.EndsWith("Client"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}