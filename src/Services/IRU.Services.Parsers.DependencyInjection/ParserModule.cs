using Autofac;

using IRU.Common.DependencyInjection;

namespace IRU.Services.Parsers.DependencyInjection
{
    public class ParserModule : IoCModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecordFactory>().As<IRecordFactory>();
        }
    }
}
