using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics.Dto
{
    public class TopPlaylistDto
    {
        public string Name { get; set; }
        public string Artists { get; set; }
        public int Count { get; set; }
    }
}
