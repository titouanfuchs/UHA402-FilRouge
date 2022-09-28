using System.Text.Json.Serialization;

namespace ShapeAPI.Models.Shapes
{
    public class Position
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double? Z { get; set; }
    }
}
