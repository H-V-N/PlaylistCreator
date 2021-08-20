using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics.MachineLearning
{
    public interface ISimilarityAlgorithm
    {
        /// <summary>
        /// Finds similar tracks to a target track in a given dataset.
        /// </summary>
        /// <param name="target">The reference track</param>
        /// <param name="dataset">The list of tracks to pull from, excluding the reference track</param>
        /// <returns>an ordered list of similar tracks</returns>
        public List<string> Predict(Track target, IEnumerable<Track> dataset);
    }
}
