using Autofac;
using MessageConsumer.Domain.Core;
using MessageConsumer.Domain.Interfaces;
using MessageConsumer.Repositories;

namespace MessageConsumer.Infrastructure.Data.AutofacModules
{
    public class InfrastructureDataModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserContext>();

            builder.RegisterType<UserRepository>()
                .As<IRepository<User>>();
            base.Load(builder);
        }
    }
}
