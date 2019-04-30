using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IRU.Services.Parsers
{
    public interface IRecordFactory
    {
        Task<bool> LoadDataAsync(string data, Type type, CancellationToken cancellationToken);

        Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(CancellationToken cancellationToken);

        //where TRecord : new();
    }
}
