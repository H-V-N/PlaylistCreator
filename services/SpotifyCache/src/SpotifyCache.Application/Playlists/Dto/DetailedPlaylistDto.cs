using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Playlists.Dto
{
    public class DetailedPlaylistDto
    {
        public Guid PlaylistId { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
