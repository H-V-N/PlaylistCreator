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
        public Task<PlaylistDto> CreatePlaylist(string trackId);
        public Task<DetailedPlaylistDto> CreateDetailedPlaylist(string trackId);
        public Task UpdateLikes(string playlistId);
        public Task UpdateDislikes(string playlistId);
    }
}
