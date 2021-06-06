using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallAds.Entities
{
    public class User
    {
        public User()
        {
            Ads = new HashSet<Ad>();
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public int AddressId { get; set; }

        public virtual Addresess Address { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
