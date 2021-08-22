using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SpotifyCache.Domain;
using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics.Playlists
{
    //todo: eventually would be interesting to store KNN weights and parameters, and apply
    //an unsupervised neural network to adjust params on the fly. out of scope for right now.
    public class Playlist : Entity<Guid>, IHasCreationTime
    {
        public string TrackId { get; set; }
        public Track Track { get; set; }

        public DateTime CreationTime { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
    }
}
