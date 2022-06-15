using Newtonsoft.Json;

namespace HalloCosmosDB
{
    public class Pizza
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


        //[JsonProperty("vegetarisch")]
        //public bool Vegetarisch { get; set; }
    }
}
