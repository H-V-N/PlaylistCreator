using SpotifyCache.Cache.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Cache
{
    public interface ICacheAppService
    {
        public Task<List<RelatedArtistOutputDto>> RelatedArtists(RelatedArtistInputDto input);
        public List<string> GetTracksToAdd(List<string> input);
        public Task AddTracks(AddTracksInputDto input);
    }
}
