using SpotifyCache.Playlists.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Playlists
{
    public interface IPlaylistAppService
    {
        public Task<PlaylistDto> CreatePlaylist(CreatePlaylistDto input);
        public Task<DetailedPlaylistDto> CreateDetailedPlaylist(CreatePlaylistDto input);
        public Task UpdateLikes(UpdatePlaylistDto input);
        public Task UpdateDislikes(UpdatePlaylistDto input);
    }
}
