using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using IRU.Services.Models;

namespace IRU.Services
{
    public interface IDataService
    {
        Task<bool> SaveDataAsync(IEnumerable<RecordModel> records, CancellationToken cancellationToken);
    }
}
