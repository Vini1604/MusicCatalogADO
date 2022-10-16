using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models
{
    public class Gender
    {
        public Guid GenderId { get; set; }
        public string GenderDescription { get; set; }
    }
}
