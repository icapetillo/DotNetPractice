using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloverTest1.Services.Clover.Models
{
    public class CloverItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public int? PriceInCents { get; set; }
        [JsonIgnore]
        public decimal? Price { get; set; }
        [JsonPropertyName("modifiers")]
        public List<CloverModifier>? Modifiers { get; set; }
        [JsonPropertyName("category")]
        public string? Category { get; set; }
    }

    public class CloverModifier
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("price")]
        public int? PriceInCents { get; set; }
        [JsonIgnore]
        public decimal? Price { get; set; }
    }
}
