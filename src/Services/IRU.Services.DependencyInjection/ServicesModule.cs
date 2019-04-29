using System;

using Autofac;

using AutoMapper;

using IRU.Common.DependencyInjection;

namespace IRU.Services.DependencyInjection
{
    public class ServicesModule : IoCModule
    {
        public override void InitializeAutoMapper(IMapperConfigurationExpression config)
        {
            //config.CreateMap<>()
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataService>().As<IDataService>();
        }
    }
}
