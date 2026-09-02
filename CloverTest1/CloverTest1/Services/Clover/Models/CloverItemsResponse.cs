using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloverTest1.Services.Clover.Models
{
    public class CloverItemsResponse
    {
        [JsonPropertyName("elements")]
        public List<CloverItem>? Elements { get; set; }
    }
}
