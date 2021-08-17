using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics
{
    //this would be better implemented in a write-heavy sorted KKV database like scylla or cassandra, so we could partition on the type of statistic and the bucket
    public class PercentileBucket : Entity<int>
    {
        public Statistic Statistic { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public int Count { get; set; }
    }

    public enum Statistic
    {
        Danceability,
        Energy,
        Loudness,
        Speechiness,
        Acousticness,
        Instrumentalness,
        Liveness,
        Valence,
        Tempo,
        DurationMs,
    }
}
