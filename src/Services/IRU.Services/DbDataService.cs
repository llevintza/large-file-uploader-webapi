using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using IRU.DataAccessLayer.Entities;
using IRU.DataAccessLayer.Repositories;
using IRU.Services.Models;

namespace IRU.Services
{
    public class DbDataService : IDataService
    {
        private readonly IMapper _mapper;

        private readonly IRecordsRepository _recordsRepository;

        public DbDataService(IMapper mapper, IRecordsRepository recordsRepository)
        {
            this._mapper = mapper;
            this._recordsRepository = recordsRepository;
        }

        public async Task<bool> SaveDataAsync(IEnumerable<RecordModel> records, CancellationToken cancellationToken)
        {
            var entities = this._mapper.Map<RecordModel[], RecordEntity[]>(records.ToArray());

            var result = await this._recordsRepository.SaveRecordsAsync(entities, cancellationToken);

            return entities.Length == result;
        }
    }
}
