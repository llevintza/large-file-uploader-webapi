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
            config.CreateMap<Models.StockModel, Parsers.Models.RecordModel>()
                .ForMember(dest => dest.ArticleCode, opts => opts.MapFrom(src => src.Article.ArticleCode))
                .ForMember(dest => dest.Color, opts => opts.MapFrom(src => src.Color))
                .ForMember(dest => dest.ColorCode, opts => opts.MapFrom(src => src.Article.ColorCode))
                .ForMember(dest => dest.DeliveredIn, opts => opts.MapFrom(src => src.DeliveredInterval))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Article.Description))
                .ForMember(dest => dest.DiscountPrice, opts => opts.MapFrom(src => src.Discount))
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Key))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.Size, opts => opts.MapFrom(src => src.Size))
                .ForMember(dest => dest.Q1, opts => opts.MapFrom(src => src.Category))
                .ReverseMap();

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
