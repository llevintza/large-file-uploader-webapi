using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using IRU.Services.Models;

using Microsoft.Extensions.Logging;

namespace IRU.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _log;

        private readonly IRecordLoader _loader;

        public FileService(
            IRecordLoader loader,
            ILogger<FileService> log)
        {
            this._loader = loader;
            this._log = log;
        }

        public async Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(Stream stream, CancellationToken cancellationToken)
        {
            await this._loader.LoadAsync<TRecord>(stream, cancellationToken);

            var records = await this._loader.GetRecordsAsync<TRecord>(cancellationToken);

            return records;
        }
    }
}
