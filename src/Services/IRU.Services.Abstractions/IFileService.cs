using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IRU.Services
{
    public interface IFileService
    {
        Task<bool> ProcessFileAsync(Stream stream, CancellationToken cancellationToken);
    }
}
