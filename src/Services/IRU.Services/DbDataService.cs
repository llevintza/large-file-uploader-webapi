using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using IRU.DataAccessLayer.Entities;
using IRU.DataAccessLayer.Repositories;

namespace IRU.Services
{
    public class DbDataService : IDataService
    {
        private readonly IMapper _mapper;

        private readonly IStockRepository _stockRepository;

        public DbDataService(IMapper mapper, IStockRepository stockRepository)
        {
            this._mapper = mapper;
            this._stockRepository = stockRepository;
        }

        public async Task<bool> SaveDataAsync<TRecord>(IEnumerable<TRecord> records, CancellationToken cancellationToken)
        {
            var entities = this._mapper.Map<TRecord[], StockItem[]>(records.ToArray());

            var result = await this._stockRepository.SaveRecordsAsync(entities, cancellationToken);

            return entities.Length == result;
        }
    }
}
