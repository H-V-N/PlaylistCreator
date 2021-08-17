using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Cache.Dto
{
    public class AddTracksInputDto
    {
        public List<string> ArtistIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }
}
