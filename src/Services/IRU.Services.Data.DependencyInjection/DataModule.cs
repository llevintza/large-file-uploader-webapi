using System;

using Autofac;

using IRU.Common.DependencyInjection;

namespace IRU.Services.Data.DependencyInjection
{
    public class DataModule : IoCModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<RecordLoader>().As<IRecordLoader>();
        }
    }
}
