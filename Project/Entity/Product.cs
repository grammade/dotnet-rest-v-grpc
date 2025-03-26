using Newtonsoft.Json;

namespace netGrpcOne.Entity
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } // Added for more text data
        public double Price { get; set; }
        public string? Category { get; set; } // Added for categorization
        public List<string> Tags { get; set; } = new List<string>(); // Added for list of strings
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>(); // Added for key-value pairs
        public List<ProductImage> Images { get; set; } = new List<ProductImage>(); // Added for nested complex objects
        public ProductDimensions? Dimensions { get; set; } // Added for a nested complex object
        public byte[]? Blob { get; set; } // Keep for large data transfer tests
        public DateTime CreatedAt { get; set; } // Added a timestamp
        public bool IsAvailable { get; set; } // Added a boolean property
    }
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ProductImage
    {
        public string? Url { get; set; }
        public string? AltText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ProductDimensions
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string? Unit { get; set; }
    }
}
