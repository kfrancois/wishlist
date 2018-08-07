using Newtonsoft.Json;

namespace WishList.Model
{
    public class Wish
    {
        // private Category category;
        //[JsonProperty("wishId")]
        public int WishId { get; set; }
        //[JsonProperty("name")]
        public string Name { get; set; }
        //[JsonProperty("description")]
        public string Description { get; set; }
        //[JsonProperty("price")]
        public double Price { get; set; }
        //[JsonProperty("claimed")]
        public bool Claimed { get; set; }

        public Wish()
        {

        }

        public Wish(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
            Claimed = false;
        }

    }
}
