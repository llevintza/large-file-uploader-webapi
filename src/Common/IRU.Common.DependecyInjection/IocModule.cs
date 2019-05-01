using Autofac;

using AutoMapper;

namespace IRU.Common.DependencyInjection
{
    public abstract class IoCModule : Module
    {
        public virtual void InitializeAutoMapper(IMapperConfigurationExpression config) { }
    }
}
