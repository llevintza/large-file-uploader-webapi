using System;
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
                .ForMember(dest => dest.Color, opts => opts.MapFrom(src => $"{src.Color.Value}".ToLower()))
                .ForMember(dest => dest.ColorCode, opts => opts.MapFrom(src => src.Article.ColorCode))
                .ForMember(dest => dest.DeliveredIn, opts => opts.MapFrom(src => src.DeliveredInterval))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Article.Description))
                .ForMember(dest => dest.DiscountPrice, opts => opts.MapFrom(src => src.Discount))
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Key))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.Size, opts => opts.MapFrom(src => src.Size))
                .ForMember(dest => dest.Q1, opts => opts.MapFrom(src => $"{src.Category.Value}"))
                .ReverseMap()
                .ForPath(dest => dest.Color, opts => opts.MapFrom(src => ParseColor(src.Color)))
                .ForPath(dest => dest.Category, opts => opts.MapFrom(src => ParseCategory(src.Q1)));

            config.CreateMap<Models.StockModel, JsonModels.StockModel>()
                .ForMember(dest => dest.Color, opts => opts.MapFrom(src => $"{src.Color.Value}".ToLower()));
            config.CreateMap<Models.ArticleModel, JsonModels.ArticleModel>();

            config.CreateMap<Models.StockModel, IRU.DataAccessLayer.Entities.StockItem>()
                .ForMember(dest => dest.ColorId, opts => opts.MapFrom(src => src.Color.Id))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.ArticleCode, opts => opts.MapFrom(src => src.Article.ArticleCode));

            config.CreateMap<Models.ArticleModel, DataAccessLayer.Entities.Article>();
            config.CreateMap<Models.ColorModel, DataAccessLayer.Entities.Color>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => $"{src.Value}"));

            config.CreateMap<Models.CategoryModel, DataAccessLayer.Entities.Category>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => $"{src.Value}"));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<RecordLoader>().As<IRecordLoader>();
            builder.RegisterType<DbDataService>().As<IDataService>();
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

        private static Models.ColorModel ParseColor(string value) => new Models.ColorModel { Value = (Models.Colors)Enum.Parse(typeof(Models.Colors), value, ignoreCase: true) };

        private static Models.CategoryModel ParseCategory(string value) => new Models.CategoryModel { Value = (Models.Categories)Enum.Parse(typeof(Models.Categories), value, ignoreCase: true) };
    }
}
