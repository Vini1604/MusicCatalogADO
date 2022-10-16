using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models
{
    public class AlbumMusic
    {
        public Guid AlbumId { get; set; }
        public string AlbumName { get; set; }
        public Guid MusicId { get; set; }
        public string MusicName { get; set; }
    }
}
