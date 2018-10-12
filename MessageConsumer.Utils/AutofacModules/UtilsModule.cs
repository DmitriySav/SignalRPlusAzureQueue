using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MessageConsumer.Util;
using MessageConsumer.Utils.Interfaces;

namespace MessageConsumer.Utils.AutofacModules
{
    public class UtilsModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordUtil>()
                .As<IPasswordUtil>();
            base.Load(builder);
        }


        
    }
}
