using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using SpotifyCache.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Domain.Tracks
{
    public class TrackManager : DomainService, ITrackManager
    {
        private readonly IRepository<Track, string> _trackRepository;
        private readonly IRepository<PercentileBucket> _percentileRepository;

        public TrackManager(IRepository<Track, string> trackRepository, IRepository<PercentileBucket> percentileRepository)
        {
            _trackRepository = trackRepository;
            _percentileRepository = percentileRepository;
        }

        public IEnumerable<PercentileBucket> GetBuckets(Track entity, IEnumerable<PercentileBucket> allBuckets)
        {
            return allBuckets.GroupBy(
                x => x.Statistic,
                (key, group) => group
                    .FirstOrDefault(bucket => bucket.Min < entity.GetStatistic(key)
                        && bucket.Max >= entity.GetStatistic(key)
                    ));
        }

        public IEnumerable<PercentileBucket> GetBuckets(Track entity)
        {
            return GetBuckets(entity, _percentileRepository.GetAll());
        }

        public async Task CreateTracks(List<Track> entities)
        {
            var buckets = await _percentileRepository.GetAll().ToListAsync();
            foreach(var entity in entities)
            {
                await _trackRepository.InsertAsync(entity);
                foreach (var bucket in GetBuckets(entity, buckets))
                {
                    if(bucket != null)
                    {
                        bucket.Count++;
                    }
                }
            }
        }
        
    }
}
