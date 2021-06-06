using System.Collections.Generic;

namespace SmallAds.Entities
{
    public class Ad
    {
        public Ad()
        {
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
