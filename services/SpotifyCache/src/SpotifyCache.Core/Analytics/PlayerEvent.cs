using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics
{
    public enum EventType
    {
        SongFinished,
        SongLiked,
        SongSkipped,
        SongDisliked
    }
    public class PlayerEvent : Entity<int>, IHasCreationTime
    {
        public DateTime CreationTime { get; set; }

        public EventType Event { get; set; }

    }
}
