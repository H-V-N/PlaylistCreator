using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics
{
    public class AnalyticsAppService : SpotifyCacheAppServiceBase, IAnalyticsAppService
    {
        private readonly IRepository<PercentileBucket> _percentileRepository;

        public AnalyticsAppService(IRepository<PercentileBucket> percentileRepository)
        {
            _percentileRepository = percentileRepository;
        }

        public Task<List<PercentileBucket>> GetAllPercentiles()
        {
            return _percentileRepository.GetAllListAsync();
        }
    }
}
