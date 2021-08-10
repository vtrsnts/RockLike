using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockLike.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<SiteLike> ListUserSiteLike { get; set; }
    }
}
