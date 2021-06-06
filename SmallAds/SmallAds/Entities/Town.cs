using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallAds.Entities
{
    public class Town
    {
        public Town()
        {
            Addresesses = new HashSet<Addresess>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Addresess> Addresesses { get; set; }
    }
}
