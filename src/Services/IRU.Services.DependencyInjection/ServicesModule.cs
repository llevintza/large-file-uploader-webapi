using System.IO;

using Autofac;

using AutoMapper;

using IRU.Common.DependencyInjection;
using IRU.Services.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IRU.Services.DependencyInjection
{
    public class ServicesModule : IoCModule
    {
        public override void InitializeAutoMapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<Parsers.Models.RecordModel, Models.RecordModel>();

            config.CreateMap<Models.RecordModel, JsonRecordModel>();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<RecordLoader>().As<IRecordLoader>();
            //builder.RegisterType<DbDataService>().As<IDataService>();
            builder.RegisterType<JsonDataService>().As<IDataService>();

            builder.Register(
                context =>
                {
                    var configuration = context.Resolve<IConfiguration>();
                    var section = configuration.GetSection("CsvParser:RecordLoader");
                    var config = section.Get<RecordLoaderConfiguration>();

                    return config;
                }).As<RecordLoaderConfiguration>().SingleInstance();

            builder.Register(
                context =>
                {
                    var configuration = context.Resolve<IConfiguration>();
                    var section = configuration.GetSection("JsonDataService:Output");
                    var appEnv = context.Resolve<IHostingEnvironment>();

                    var config = section.Get<JsonDataServiceConfiguration>();
                    config.Path = Path.Combine(appEnv.ContentRootPath, config.Path);


                    return config;
                }).As<JsonDataServiceConfiguration>().SingleInstance();
        }
    }
}
