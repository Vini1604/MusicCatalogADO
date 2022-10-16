using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models.Dtos
{
    public class AlbumMusicDTO
    {
        public Guid AlbumId { get; set; }
        public Guid MusicId { get; set; }
    }
}
