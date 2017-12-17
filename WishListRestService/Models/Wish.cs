namespace WishListRestService.Models
{
    public class Wish
    {
        public int WishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool Claimed { get; set; }
    }
}