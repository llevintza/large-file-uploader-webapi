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
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _log;

        private readonly IRecordLoader _loader;

        public DataService(
            IRecordLoader loader,
            ILogger<DataService> log)
        {
            this._loader = loader;
            this._log = log;

        }
        
        public async Task<bool> ProcessFileAsync(Stream stream, CancellationToken cancellationToken)
        {
            await this._loader.LoadAsync<RecordModel>(stream, cancellationToken);

            var records = await this._loader.GetRecordsAsync<RecordModel>(cancellationToken);

            return false;
        }
    }
}
