using Abp.Domain.Repositories;
using Abp.Domain.Services;
using SpotifyCache.Analytics.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Domain.Tracks
{
    public class TrackManager : DomainService, ITrackManager
    {
        private readonly ISimilarityAlgorithm _algorithm;
        private readonly IRepository<Track, string> _trackRepository;

        public TrackManager(ISimilarityAlgorithm algorithm, IRepository<Track, string> trackRepository)
        {
            _algorithm = algorithm;
            _trackRepository = trackRepository;
        }

        public async Task<List<Track>> FindRelatedTracks(string targetTrackId)
        {
            var ids = await FindRelatedTrackIds(targetTrackId);
            ids.Prepend(targetTrackId);
            return _trackRepository.GetAll()
                    .Where(x => ids.Contains(x.Id))
                    .OrderBy(x => ids.IndexOf(x.Id))
                    .ToList();
        }

        public async Task<List<string>> FindRelatedTrackIds(string targetTrackId)
        {
            var allTracks = await _trackRepository.GetAllListAsync();
            var targetTrack =  allTracks.Find(x => x.Id == targetTrackId);
            allTracks.Remove(targetTrack);
            return _algorithm.Predict(targetTrack, allTracks);
        }
    }
}
