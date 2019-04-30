using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using IRU.Services.Configuration;
using IRU.Services.Parsers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IRU.Services
{
    public class RecordLoader : IRecordLoader
    {
        private readonly IRecordFactory _recordFactory;

        private readonly ILogger<RecordLoader> _log;

        private readonly StringBuilder _buffer;

        //todo: rad from config
        private readonly int _bufferSize;

        private int _position;

        public RecordLoader(IRecordFactory recordFactory, RecordLoaderConfiguration loaderConfig, ILogger<RecordLoader> log)
        {
            this._recordFactory = recordFactory;
            this._log = log;

            this._buffer = new StringBuilder();
            this._bufferSize = loaderConfig.BufferSize;
        }


        public async Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(CancellationToken cancellationToken)
        {
            return await this._recordFactory.GetRecordsAsync<TRecord>(cancellationToken);
        }

        public async Task<bool> LoadAsync<TRecord>(Stream stream, CancellationToken cancellationToken)
        {
            using (var streamReader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
            {
                this.InitializeBuffer();
                do
                {
                    while (this._position <= this._bufferSize && !streamReader.EndOfStream)
                    {
                        var value = await streamReader.ReadLineAsync();
                        this._buffer.AppendLine(value);
                        this._position++;
                    }

                    await this.FlushBufferAsync<TRecord>(cancellationToken);

                    this.InitializeBuffer();
                }
                while (!streamReader.EndOfStream);
            }

            return true;
        }

        private async Task<bool> FlushBufferAsync<TRecord>(CancellationToken cancellationToken)
        {
            return await this._recordFactory.LoadDataAsync(this._buffer.ToString(), typeof(TRecord), cancellationToken);
        }

        private void InitializeBuffer()
        {
            this._position = 0;
            this._buffer.Clear();
        }
    }
}
