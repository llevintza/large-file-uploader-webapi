using Autofac;

using AutoMapper;

using IRU.Common.DependencyInjection;

namespace IRU.Services.Parsers.DependencyInjection
{
    public class ParserModule : IoCModule
    {
        public override void InitializeAutoMapper(IMapperConfigurationExpression config)
        {
            //config.CreateMap<>()
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecordFactory>().As<IRecordFactory>();
        }
    }
}
