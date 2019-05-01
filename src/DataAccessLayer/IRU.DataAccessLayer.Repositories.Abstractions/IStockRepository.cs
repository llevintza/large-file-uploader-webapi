using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using IRU.DataAccessLayer.Entities;

namespace IRU.DataAccessLayer.Repositories
{
    public interface IStockRepository
    {
        Task<int> SaveRecordsAsync(IEnumerable<StockItem> entities, CancellationToken cancellationToken);
    }
}
