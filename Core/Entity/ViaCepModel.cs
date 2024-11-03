using System.Text.Json.Serialization;

namespace Core.Entity
{
    public class ViaCepModel
    {
        [JsonPropertyName("ddd")]
        public string Ddd { get; set; }
    }
}
