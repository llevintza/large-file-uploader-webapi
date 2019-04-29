using AutoMapper;

using IRU.Common.DependencyInjection;

namespace IRU.LargeFileUploader.WebApp.IoC
{
    public class WebAppModule : IoCModule
    {
        public override void InitializeAutoMapper(IMapperConfigurationExpression config)
        {
            //config.CreateMap<>()
        }
    }
}
