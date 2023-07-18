using AggregatedSearch.BusinessLogic;
using Autofac;

namespace AggregatedSearch.Api.IoC
{
    /// <summary>
    /// DI модуль с валидациями
    /// </summary>
    public class ValidatorModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(BusinessLogicAssembly.Value)
                .Where(x => x.Name.EndsWith("Validator"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}