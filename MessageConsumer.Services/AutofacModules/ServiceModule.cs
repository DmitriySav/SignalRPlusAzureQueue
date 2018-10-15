using Autofac;
using MessageConsumer.Services.Interfaces;


namespace MessageConsumer.Services.AutofacModules
{
    public class ServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(HubGroupManager<>))
                .As(typeof(IHubGroupManager<>))
                .SingleInstance();           
            builder.RegisterType<MessageGetter>()
                .As<IMessageService>();
            builder.RegisterType<UserService>()
                .As<IUserService>();
            base.Load(builder);
        }
    }
}
