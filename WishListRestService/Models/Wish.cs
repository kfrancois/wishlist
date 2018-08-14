using Newtonsoft.Json;

namespace WishListRestService.Models
{
    public class Wish
    {
        [JsonProperty("wishId")]
        public int WishId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("claimed")]
        public bool Claimed { get; set; }
    }
}