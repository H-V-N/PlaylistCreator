using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SpotifyCache.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Cache.Dto
{
    public class RelatedArtistInputDto
    {
        public List<ArtistDto> Artists { get; set; }
        public int MoreCount { get; set; }
    }

    [AutoMapTo(typeof(Artist))]
    public class ArtistDto : IEntityDto<string>
    {
        public string Id { get; set; }
        public bool SearchedRelatedArtists { get; set; }
    }
}
