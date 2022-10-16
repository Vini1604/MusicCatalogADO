using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models
{
    public class Album
    {
        public Guid AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string Year { get; set; }
        public Guid ArtistId { get; set; }
        public String ArtistName { get; set; }
        public Guid GenderId { get; set; }
        public String GenderDescription { get; set; }
    }
}
