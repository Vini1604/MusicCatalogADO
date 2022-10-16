using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models
{
    public class Music
    {
        public Guid MusicId { get; set; }
        public string MusicName { get; set; }
        public string Duration { get; set; }
    }
}
