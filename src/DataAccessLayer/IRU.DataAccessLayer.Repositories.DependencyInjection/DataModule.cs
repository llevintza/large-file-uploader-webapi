using Autofac;

using IRU.Common.DependencyInjection;
using IRU.DataAccessLayer.DbContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IRU.DataAccessLayer.Repositories.DependencyInjection
{
    public class DataModule : IoCModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<RecordsRepository>().As<IRecordsRepository>();
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();

                var opt = new DbContextOptionsBuilder<RecordsDbContext>();
                opt.UseSqlServer(config.GetConnectionString("RecordsDb"));
                
                return new RecordsDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
