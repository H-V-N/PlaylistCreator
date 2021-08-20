using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Domain.Tracks
{
    public interface ITrackManager : IDomainService
    {
        public Task<List<Track>> FindRelatedTracks(string targetTrackId);
        public Task<List<string>> FindRelatedTrackIds(string targetTrackId);
    }
}
