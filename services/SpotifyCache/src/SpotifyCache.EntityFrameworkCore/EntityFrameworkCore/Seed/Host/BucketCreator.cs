using EFCore.BulkExtensions;
using SpotifyCache.Analytics;
using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.EntityFrameworkCore.Seed.Host
{
    public class BucketCreator
    {
        private readonly SpotifyCacheDbContext _context;

        public BucketCreator(SpotifyCacheDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.Truncate<PercentileBucket>();
            _context.SaveChanges();
            var totalTracks = _context.Tracks.Count()+1;
            if (totalTracks < 100) return; //not enough data to calculate accurate buckets

            var lowerIndex = totalTracks / 4;
            var upperIndex = lowerIndex * 3;

            foreach(var stat in Enum.GetValues(typeof(Statistic)).Cast<Statistic>())
            {
                CreateStatBucket(stat, lowerIndex, upperIndex);
            }
            _context.SaveChanges();
        }

        private void CreateStatBucket(Statistic stat, int lowerIndex, int upperIndex)
        {
            var initialQuery = _context.Tracks.AsQueryable()
                    .Select(stat.ToExpression())
                    .OrderBy(x => x);

            var lower = initialQuery.Skip(lowerIndex).First();
            var upper = initialQuery.Skip(upperIndex).First();
            var outlierThreshold = (upper - lower) * 1.5f;
            lower -= outlierThreshold;
            upper += outlierThreshold;
            var increment = (upper - lower) / 20;
            
            for(var i = 0; i < 20; i++)
            {
                var bucket = new PercentileBucket
                {
                    Min = lower * i,
                    Max = lower * (i + 1),
                    Statistic = stat,
                    Count = 0
                };
                bucket.Count = _context.Tracks.AsQueryable()
                        .Select(stat.ToExpression())
                        .Where(x => x >= bucket.Min && x < bucket.Max)
                        .Count();

                _context.PercentileBuckets.Add(bucket);
            }

        }
    }
}
