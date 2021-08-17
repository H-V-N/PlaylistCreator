using Newtonsoft.Json;
using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Cache.Dto
{
    public class TrackDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SimpleArtistDto> Artists { get; set; }
        public float Danceability { get; set; }
        public float Energy { get; set; }
        public float Acousticness { get; set; }
        public float Instrumentalness { get; set; }
        public float Liveness { get; set; }
        public float Valence { get; set; }
        public Key Key { get; set; }
        public float Loudness { get; set; }
        public Mode Mode { get; set; }
        public float Speechiness { get; set; }
        public float Tempo { get; set; }

        //usually we use camelcase but this is coming directly from spotify
        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }
    }


    public class SimpleArtistDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
