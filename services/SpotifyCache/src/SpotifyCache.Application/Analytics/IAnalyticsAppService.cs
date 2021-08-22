using SpotifyCache.Analytics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics
{
    public interface IAnalyticsAppService
    {
        public Task<List<PercentileBucket>> GetAllPercentiles();

        public Task<AnalyticsDto> GetAnalytics();
    }
}
