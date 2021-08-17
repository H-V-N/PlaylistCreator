using Abp.Domain.Entities;
using SpotifyCache.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Domain.Tracks
{
    public enum Key {
        C,
        CSharp,
        D,
        DSharp,
        E,
        F,
        FSharp,
        G,
        GSharp,
        A,
        ASharp,
        B
    }
    public enum Mode
    {
        Minor,
        Major
    }
    public class Track : Entity<string>
    {
        public string Name { get; set; }
        public string Artists { get; set; }
        public string ArtistIds { get; set; }
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
        //this should be an int, but its cleaner to treat it the
        //same as the rest of the statistics
        public float DurationMs { get; set; } 

        //yikes... ran into this pattern before, I wonder
        //if theres a better way to map enums to types/variables.
        //usually seems like an antipattern, but for our purposes
        //i dont see a way around it
        public float GetStatistic(Statistic statistic)
        {
            switch (statistic)
            {
                case Statistic.Danceability:
                    return Danceability;
                case Statistic.Energy:
                    return Energy;
                case Statistic.Loudness:
                    return Loudness;
                case Statistic.Speechiness:
                    return Speechiness;
                case Statistic.Acousticness:
                    return Acousticness;
                case Statistic.Instrumentalness:
                    return Instrumentalness;
                case Statistic.Liveness:
                    return Liveness;
                case Statistic.Valence:
                    return Valence;
                case Statistic.Tempo:
                    return Tempo;
                case Statistic.DurationMs:
                    return DurationMs;
                default:
                    return 0;
            }
        }
    }
}
