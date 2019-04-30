using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using IRU.DataAccessLayer.DbContext;
using IRU.DataAccessLayer.Entities;

namespace IRU.DataAccessLayer.Repositories
{
    public class RecordsRepository : IRecordsRepository, IDisposable
    {
        private readonly RecordsDbContext _dbContext;

        public RecordsRepository(RecordsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> SaveRecordsAsync(IEnumerable<RecordEntity> entities, CancellationToken cancellationToken)
        {
            await this._dbContext.Records.AddRangeAsync(entities, cancellationToken);

            return await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            this._dbContext?.Dispose();
        }
    }
}
