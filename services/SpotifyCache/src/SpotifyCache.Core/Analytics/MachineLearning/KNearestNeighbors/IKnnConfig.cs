using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics.MachineLearning.KNearestNeighbors
{
    public interface IKnnConfig
    {
        public int FeatureCount { get; }
        public int NeighborCount { get; }
        public Func<Track, float[]> FeatureTransformer { get; }
    }
}
