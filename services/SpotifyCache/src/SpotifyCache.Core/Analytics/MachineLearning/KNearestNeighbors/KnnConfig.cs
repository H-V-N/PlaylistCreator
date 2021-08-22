using Abp.Dependency;
using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics.MachineLearning.KNearestNeighbors
{
    public class KnnConfig : IKnnConfig
    {
        public int FeatureCount { get; } = 5;

        public int NeighborCount { get; } = 20;

        public Func<Track, float[]> FeatureTransformer { get; } = (t) => new float[]
        {
            t.Danceability,
            t.Energy,
            t.Valence,
            Math.Min(1.0f,t.Speechiness * 3),
            Math.Max((t.Year - 1900)/120, 0),
        };
    }
}
