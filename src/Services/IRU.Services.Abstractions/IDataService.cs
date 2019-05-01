using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IRU.Services
{
    public interface IDataService
    {
        Task<bool> SaveDataAsync<TRecord>(IEnumerable<TRecord> records, CancellationToken cancellationToken);
    }
}
