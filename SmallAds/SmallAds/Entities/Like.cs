

namespace SmallAds.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AdId { get; set; }

        public virtual Ad Ad { get; set; }
        public virtual User User { get; set; }
    }
}
