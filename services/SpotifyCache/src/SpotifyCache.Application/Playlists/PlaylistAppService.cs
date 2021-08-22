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
        private readonly IRepository<Playlist, Guid> _playlistRepository;
        private readonly ITrackManager _trackManager;

        public PlaylistAppService(IRepository<Playlist, Guid> playlistRepository, ITrackManager trackManager)
        {
            _playlistRepository = playlistRepository;
            _trackManager = trackManager;
        }

        public async Task<PlaylistDto> CreatePlaylist(CreatePlaylistDto input)
        {
            var relatedTracks = await _trackManager.FindRelatedTrackIds(input.TrackId);
            var playlistId = await _playlistRepository.InsertAndGetIdAsync(new Playlist { TrackId = input.TrackId });
            return new PlaylistDto
            {
                PlaylistId = playlistId,
                TrackIds = relatedTracks
            };
        }

        public async Task<DetailedPlaylistDto> CreateDetailedPlaylist(CreatePlaylistDto input)
        {
            var myTracks = await _trackManager.FindRelatedTracks(input.TrackId);
            var playlistId = await _playlistRepository.InsertAndGetIdAsync(new Playlist { TrackId = input.TrackId });
            return new DetailedPlaylistDto
            {
                PlaylistId = playlistId,
                Tracks = myTracks
            };
        }

        public async Task UpdateLikes(UpdatePlaylistDto input)
        {
            var playlist = await _playlistRepository.GetAsync(input.PlaylistId);
            playlist.LikeCount++;
        }

        public async Task UpdateDislikes(UpdatePlaylistDto input)
        {
            var playlist = await _playlistRepository.GetAsync(input.PlaylistId);
            playlist.DislikeCount++;
        }
    }
}
