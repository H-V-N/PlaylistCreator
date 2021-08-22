using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics.Dto
{
    public class AnalyticsDto
    {
        public List<TopPlaylistDto> TopPlaylists { get; set; }
        public AccuracyDto Accuracy { get; set; }
    }
}
