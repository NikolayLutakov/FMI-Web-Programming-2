using SmallAds.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallAds.Models.Users
{
    public class IndexVM
    {
        public User User { get; set; }
        public List<Ad> Ads { get; set; }
    }
}
