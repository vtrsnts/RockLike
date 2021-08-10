using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RockLike.Models
{
    public class SiteLike
    {
        [ForeignKey("User")]
        public string IdUser { get; set; }
     
        [ForeignKey("Site")]
        public int IdSite { get; set; }
        public DateTime LastInteration { get; set; }
        public bool Like { get; set; }
        public virtual Site Site { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
