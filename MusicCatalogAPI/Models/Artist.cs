using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models
{
    public class Artist
    {
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}
