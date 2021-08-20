using Abp.Domain.Repositories;
using SpotifyCache.Analytics;
using SpotifyCache.Analytics.Playlists;
using SpotifyCache.Domain.Tracks;
using SpotifyCache.Playlists.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Playlists
{
    public class PlaylistAppService : SpotifyCacheAppServiceBase, IPlaylistAppService
    {
        private readonly IRepository<Playlist, string> _playlistRepository;
        private readonly ITrackManager _trackManager;

        public PlaylistAppService(IRepository<Playlist, string> playlistRepository, ITrackManager trackManager)
        {
            _playlistRepository = playlistRepository;
            _trackManager = trackManager;
        }

        public async Task<PlaylistDto> CreatePlaylist(string trackId)
        {
            var relatedTracks = await _trackManager.FindRelatedTrackIds(trackId);
            var playlistId = await _playlistRepository.InsertAndGetIdAsync(new Playlist { TrackId = trackId });
            return new PlaylistDto
            {
                PlaylistId = playlistId,
                TrackIds = relatedTracks
            };
        }

        public async Task<DetailedPlaylistDto> CreateDetailedPlaylist(string trackId)
        {
            var myTracks = await _trackManager.FindRelatedTracks(trackId);
            var playlistId = await _playlistRepository.InsertAndGetIdAsync(new Playlist { TrackId = trackId });
            return new DetailedPlaylistDto
            {
                PlaylistId = playlistId,
                Tracks = myTracks
            };
        }

        public async Task UpdateLikes(string playlistId)
        {
            var playlist = await _playlistRepository.GetAsync(playlistId);
            playlist.LikeCount++;
        }

        public async Task UpdateDislikes(string playlistId)
        {
            var playlist = await _playlistRepository.GetAsync(playlistId);
            playlist.DislikeCount++;
        }
    }
}
