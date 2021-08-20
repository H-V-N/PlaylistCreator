using AutoMapper;
using SpotifyCache.Domain.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Cache.Dto
{
    public class TrackMapProfile : Profile
    {
        public TrackMapProfile()
        {
            CreateMap<TrackDto, Track>()
                .ForMember(dest => dest.ArtistIds, x => x.MapFrom(
                    dto => $"[{string.Join(", ", dto.Artists.Select(a => $"'{a.Name}'"))}]"))
                .ForMember(dest => dest.Artists, x => x.MapFrom(
                    dto => $"[{string.Join(", ", dto.Artists.Select(a => $"'{a.Id}'"))}]"))
                .ForMember(dest => dest.DurationMs, x => x.MapFrom(dto => (float)dto.DurationMs))
                .ForMember(dest => dest.Year, x => x.MapFrom(dto => dto.Album.Year.Length >=4 ? float.Parse(dto.Album.Year.Substring(0,4)) : 1900f));
        }
    }
}
