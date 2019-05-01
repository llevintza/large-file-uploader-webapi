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
            builder.RegisterType<StockRepository>().As<IStockRepository>();
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();

                var opt = new DbContextOptionsBuilder<DatabaseContext>();
                opt.UseSqlServer(config.GetConnectionString("RecordsDb"));
                
                return new DatabaseContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
