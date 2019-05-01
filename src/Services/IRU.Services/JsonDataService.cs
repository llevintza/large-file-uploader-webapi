using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using IRU.Services.Configuration;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IRU.Services
{
    public class JsonDataService : IDataService
    {
        private readonly JsonDataServiceConfiguration _jsonConfig;

        private readonly IMapper _mapper;

        public JsonDataService(JsonDataServiceConfiguration jsonConfig, IMapper mapper, IConfiguration configuration)
        {
            this._jsonConfig = jsonConfig;
            this._mapper = mapper;
        }

        public async Task<bool> SaveDataAsync<TRecord>(IEnumerable<TRecord> records, CancellationToken cancellationToken)
        {
            var items = this._mapper.Map<TRecord[], JsonModels.StockModel[]>(records.ToArray());

            var path = Path.Combine(this._jsonConfig.Path, this._jsonConfig.FileName);

            using (var file = File.CreateText(path))
            {
                var serializer = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                serializer.Serialize(file, items);
            }

            return await Task.FromResult(true);
        }
    }
}
