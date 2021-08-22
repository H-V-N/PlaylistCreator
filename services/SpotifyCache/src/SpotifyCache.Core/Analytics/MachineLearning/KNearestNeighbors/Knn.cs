using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KdTree;
using KdTree.Math;
using Abp.Dependency;

namespace SpotifyCache.Analytics.MachineLearning.KNearestNeighbors
{
    public class Knn : ISimilarityAlgorithm
    {
        private readonly IKnnConfig _config;

        public Knn(IKnnConfig config)
        {
            _config = config;
        }

        public List<string> Predict(Track target, IEnumerable<Track> dataset)
        {
            var tree = new KdTree<float, string>(_config.FeatureCount, new FloatMath());
            foreach (var track in dataset)
            {
                tree.Add(_config.FeatureTransformer(track), track.Id);
            }
            return tree.GetNearestNeighbours(_config.FeatureTransformer(target), _config.NeighborCount)
                    .Select(x => x.Value)
                    .ToList();
        }
    }
}
