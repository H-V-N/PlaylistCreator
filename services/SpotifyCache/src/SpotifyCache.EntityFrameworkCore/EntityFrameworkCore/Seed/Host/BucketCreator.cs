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

            foreach(var stat in Enum.GetValues(typeof(Statistic)).Cast<Statistic>())
            {
                CreateStatBucket(stat);
            }
            _context.SaveChanges();
        }

        private void CreateStatBucket(Statistic stat, int buckets = 20)
        {
            var initialQuery = _context.Tracks.AsQueryable()
                    .Select(stat.ToExpression())
                    .OrderBy(x => x);

            var tracksPerBucket = initialQuery.Count() / buckets;

            var min = initialQuery.First();
            for(var i = 0; i < buckets; i++)
            {
                var max = initialQuery.Skip(((i + 1) * tracksPerBucket) - 1).First();
                _context.PercentileBuckets.Add(new PercentileBucket
                {
                    Min = min,
                    Max = max,
                    Statistic = stat,
                    Count = i
                });
                min = max;
            }

        }
    }
}
