using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallAds.Entities
{
    public class Addresess
    {
        public Addresess()
        {
            Users = new HashSet<User>();
        }
        

        public int Id { get; set; }
        public string AddressText { get; set; }
        public int TownId { get; set; }

        public virtual Town Town { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
