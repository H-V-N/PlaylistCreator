using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SpotifyCache.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Analytics
{
    public class CreationHistory : Entity<string>, IHasCreationTime
    {
        public string TrackId { get; set; }
        public Track Track { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
