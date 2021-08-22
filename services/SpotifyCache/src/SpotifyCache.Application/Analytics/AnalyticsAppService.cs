using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SpotifyCache.Analytics.Dto;
using SpotifyCache.Analytics.Playlists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics
{
    public class AnalyticsAppService : SpotifyCacheAppServiceBase, IAnalyticsAppService
    {
        private readonly IRepository<PercentileBucket> _percentileRepository;
        private readonly IRepository<Playlist, Guid> _playlistRepository;

        public AnalyticsAppService(IRepository<PercentileBucket> percentileRepository, IRepository<Playlist, Guid> playlistRepository)
        {
            _percentileRepository = percentileRepository;
            _playlistRepository = playlistRepository;
        }

        public Task<List<PercentileBucket>> GetAllPercentiles()
        {
            return _percentileRepository.GetAllListAsync();
        }

        public async Task<AnalyticsDto> GetAnalytics()
        {
            var results = await _playlistRepository.GetAll()
                    .Include(x => x.Track)
                    .GroupBy(
                        x => new { x.Track.Artists, x.Track.Name },
                        (k, v) => new
                        {
                            k.Artists,
                            k.Name,
                            Count = v.Count(),
                            LikeCount = v.Sum(p => p.LikeCount),
                            DislikeCount = v.Sum(p => p.DislikeCount)
                        })
                    .OrderByDescending(x => x.Count)
                    .ToListAsync();

            return new AnalyticsDto
            {
                TopPlaylists = results.Take(10)
                    .Select(x => new TopPlaylistDto
                    {
                        Artists = x.Artists,
                        Name = x.Name,
                        Count = x.Count
                    }).ToList(),
                Accuracy = new AccuracyDto
                {
                    LikeCount = results.Sum(x => x.LikeCount),
                    DislikeCount = results.Sum(x => x.DislikeCount)
                }
            };
        }
    }
}
