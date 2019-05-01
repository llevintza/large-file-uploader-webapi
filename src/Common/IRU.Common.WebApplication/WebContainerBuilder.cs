using System.Collections.Generic;

using Autofac;

using AutoMapper;

using IRU.Common.DependencyInjection;

namespace IRU.Common.WebApplication
{
    public class WebContainerBuilder
    {
        private readonly List<IoCModule> _modules = new List<IoCModule>();

        public void RegisterModule<TModule>()
            where TModule : IoCModule, new()
        {
            this._modules.Add(new TModule());
        }

        public void InitializeAutoFac(ContainerBuilder builder)
        {
            foreach (var module in this._modules)
            {
                builder.RegisterModule(module);
            }
        }

        public void InitializeAutoMapper(ContainerBuilder builder)
        {
            MapperConfiguration configuration = new MapperConfiguration(
                config =>
                {
                    foreach (var module in this._modules)
                    {
                        module.InitializeAutoMapper(config);
                    }
                });

            IMapper mapper = new Mapper(configuration);

            builder.RegisterInstance(mapper);
        }
    }
}
