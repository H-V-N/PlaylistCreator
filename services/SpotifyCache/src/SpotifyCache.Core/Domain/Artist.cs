using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Domain
{
    public class Artist : Entity<string>
    {
        public bool SearchedRelatedArtists { get; set; }
        public DateTime? LastSearchedSongs { get; set; }
    }
}
