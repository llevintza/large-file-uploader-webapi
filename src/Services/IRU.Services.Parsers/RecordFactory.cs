using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using CsvHelper;

using IRU.Services.Parsers.Models;

using Microsoft.Extensions.Logging;

namespace IRU.Services.Parsers
{
    public class RecordFactory : IRecordFactory
    {
        private string[] _headers;

        private readonly List<RecordModel> _records;

        private readonly IMapper _mapper;

        private readonly ILogger<RecordFactory> _log;
        
        public RecordFactory(IMapper mapper, ILogger<RecordFactory> log)
        {
            this._mapper = mapper;
            this._log = log;
            this._records = new List<RecordModel>();
        }

        public async Task<bool> LoadDataAsync(string data, Type type, CancellationToken cancellationToken)
        {
            var dataSource = new StringBuilder();
            //todo: this should be passed as configurable to the reader
            if (this._headers != null && this._headers.Any())
            {
                dataSource.AppendLine(string.Join(",", this._headers));
            }

            dataSource.Append(data);

            using (var reader = new StringReader(dataSource.ToString()))
            {
                using (var csv = new CsvReader(reader))
                {
                    if (this._headers == null)
                    {
                        await csv.ReadAsync();
                        csv.ReadHeader();
                        this._headers = csv.Context.HeaderRecord;
                    }

                    while (await csv.ReadAsync())
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        var record = csv.GetRecord<RecordModel>();

                        this._records.Add(record);
                    }
                }

                return true;
            }
        }

        public async Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(CancellationToken cancellationToken)
        {
            if (!this._records.Any())
            {
                throw new InvalidOperationException("There are no records yet!");
            }


            var result = this._mapper.Map<RecordModel[], TRecord[]>(this._records.ToArray());

            return await Task.FromResult(result);
        }
    }
}
