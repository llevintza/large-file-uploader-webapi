using Autofac;

using AutoMapper;

using IRU.Common.DependencyInjection;

namespace IRU.Services.DependencyInjection
{
    public class ServicesModule : IoCModule
    {
        public override void InitializeAutoMapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<Parsers.Models.RecordModel, Models.RecordModel>();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<RecordLoader>().As<IRecordLoader>();
        }
    }
}
