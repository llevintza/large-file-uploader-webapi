using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IRU.Services
{
    public interface IRecordLoader
    {
        Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(CancellationToken cancellationToken);

        Task<bool> LoadAsync<TRecord>(Stream stream, CancellationToken cancellationToken);

    }
}
