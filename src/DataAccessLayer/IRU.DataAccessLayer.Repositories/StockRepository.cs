using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using IRU.DataAccessLayer.DbContext;
using IRU.DataAccessLayer.Entities;

namespace IRU.DataAccessLayer.Repositories
{
    public class StockRepository : IStockRepository, IDisposable
    {
        private readonly DatabaseContext _dbContext;

        public StockRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> SaveRecordsAsync(IEnumerable<StockItem> entities, CancellationToken cancellationToken)
        {
            await this._dbContext.StockItems.AddRangeAsync(entities, cancellationToken);

            return await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            this._dbContext?.Dispose();
        }
    }
}
