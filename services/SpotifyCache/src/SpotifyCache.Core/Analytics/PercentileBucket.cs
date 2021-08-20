using Abp.Domain.Entities;
using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public static class StatisticsExtensions
    {

        public static Expression<Func<Track, float>> ToExpression(this Statistic statistic)
        {
            return statistic switch
            {
                Statistic.Danceability => x => x.Danceability,
                Statistic.Energy => x => x.Energy,
                Statistic.Loudness => x => x.Loudness,
                Statistic.Speechiness => x => x.Speechiness,
                Statistic.Acousticness => x => x.Acousticness,
                Statistic.Instrumentalness => x => x.Instrumentalness,
                Statistic.Liveness => x => x.Liveness,
                Statistic.Valence => x => x.Valence,
                Statistic.Tempo => x => x.Tempo,
                Statistic.DurationMs => x => x.DurationMs,
                _ => x => x.Danceability,
            };
        }
    }
}
