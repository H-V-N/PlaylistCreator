﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.Cache.Dto
{
    public class RelatedArtistOutputDto
    {
        public string Id { get; set; }
        public bool ShouldSearchRelated { get; set; }
    }
}
