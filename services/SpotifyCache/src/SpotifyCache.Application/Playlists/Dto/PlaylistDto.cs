using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Playlists.Dto
{

    public class PlaylistDto
    {
        public Guid PlaylistId { get; set; }
        public List<string> TrackIds { get; set; }
    }
}
