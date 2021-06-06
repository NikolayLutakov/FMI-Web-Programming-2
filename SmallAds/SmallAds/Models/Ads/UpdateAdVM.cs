using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmallAds.Models.Ads
{
    public class UpdateAdVM
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        [Required(ErrorMessage = "*This field is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "*This field is required")]
        public string Text { get; set; }
    }
}
