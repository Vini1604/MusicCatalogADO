using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models.Dtos
{
    public class AlbumDTO
    {
        public string AlbumName { get; set; }
        public string Year { get; set; }
        public Guid ArtistId { get; set; }
        public Guid GenderId { get; set; }
    }
}
