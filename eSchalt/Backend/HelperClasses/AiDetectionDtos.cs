using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eSchalt.Backend.HelperClasses
{
    public class AiDetectionResult
    {
        [JsonPropertyName("components")]
        public List<AiComponentDto> Components { get; set; } = new();
        
        [JsonPropertyName("imageWidth")]
        public int ImageWidth { get; set; }
        
        [JsonPropertyName("imageHeight")]
        public int ImageHeight { get; set; }
        
        [JsonPropertyName("warnings")]
        public List<string> Warnings { get; set; } = new();
    }

    public class AiComponentDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("xPosTopLeft")]
        public double XPosTopLeft { get; set; }
        
        [JsonPropertyName("yPosTopLeft")]
        public double YPosTopLeft { get; set; }
        
        [JsonPropertyName("xPosBottomRight")]
        public double XPosBottomRight { get; set; }
        
        [JsonPropertyName("yPosBottomRight")]
        public double YPosBottomRight { get; set; }
    }
}


