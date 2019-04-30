using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using IRU.DataAccessLayer.Entities;

namespace IRU.DataAccessLayer.Repositories
{
    public interface IRecordsRepository
    {
        Task<int> SaveRecordsAsync(IEnumerable<RecordEntity> entities, CancellationToken cancellationToken);
    }
}
