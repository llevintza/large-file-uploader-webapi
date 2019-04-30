using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IRU.Services
{
    public interface IDataService
    {
        Task<bool> ProcessFileAsync(Stream stream, CancellationToken cancellationToken);
    }
}
