using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IRU.Services
{
    public interface IFileService
    {
        Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(Stream stream, CancellationToken cancellationToken);
    }
}
