using Abp.Domain.Services;
using SpotifyCache.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Domain.Tracks
{
    public interface ITrackManager : IDomainService
    {
        /// <summary>
        /// Gets statistics buckets for a track's attributes. NOTE: makes a roundtrip to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<PercentileBucket> GetBuckets(Track entity);

        /// <summary>
        /// Gets statistics buckets for a track's attributes
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="allBuckets">an existing list of buckets pulled from the database</param>
        /// <returns></returns>
        IEnumerable<PercentileBucket> GetBuckets(Track entity, IEnumerable<PercentileBucket> allBuckets);

        /// <summary>
        /// Batch inserts a list of tracks
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task CreateTracks(List<Track> entities);
    }
}
