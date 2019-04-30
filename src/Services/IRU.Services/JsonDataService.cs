using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using IRU.Services.Configuration;
using IRU.Services.Models;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

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

        public async Task<bool> SaveDataAsync(IEnumerable<RecordModel> records, CancellationToken cancellationToken)
        {
            var items = this._mapper.Map<RecordModel[], JsonRecordModel[]>(records.ToArray());

            var path = Path.Combine(this._jsonConfig.Path, this._jsonConfig.FileName);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, items);
            }

            return await Task.FromResult(true);
        }
    }

    //todo: move to separate file
    public class JsonRecordModel
    {
        public string Key { get; set; }

        public string ArticleCode { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string DiscountPrice { get; set; }

        public string DeliveredIn { get; set; }

        public string Q1 { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }
    }
}
