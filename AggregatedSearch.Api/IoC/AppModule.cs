using Autofac;

namespace AggregatedSearch.Api.IoC
{
    /// <summary>
    /// Главный DI модуль
    /// </summary>
    public class AppModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ClientModule>();
            builder.RegisterModule<ValidatorModule>();
        }
    }
}