using netGrpcOne.Entity;
using System.Security.Cryptography;

namespace netGrpcOne.Repository;

public class ProductRepository
{
    private static Random rand = new Random();
    public static List<Product> getProductsBigSize(int n)
    {
        return Enumerable.Range(0, n).Select(i =>
            new Product
            {
                Id = i,
                Price = rand.Next(0, 10000),
                Name = i.ToString(),
                Blob = GenerateRandomBlob(1024 * 30)
            }).ToList();
    }

    public static List<ProductDimensions> getProductSmallSize(int n)
    {
        return Enumerable.Range(0, n).Select(i =>
            new ProductDimensions
            {
                Height = i,
                Length = rand.Next(0, 1000),
                Unit = "cm",
                Width = i
            }).ToList();
    }

    public static List<Product> getProductComplexObject(int n)
    {
        var res = Enumerable.Range(0, n).Select(i =>
            new Product
            {
                Id = i,
                Name = "Example Product",
                Description = "Lorem Ipsum",
                Price = 99.99d,
                Category = "Electronics",
                Tags = new List<string> { "tag1", "tag2", "tag3" },
                Attributes = new Dictionary<string, string> { { "color", "red" }, { "size", "large" } },
                Images = new List<ProductImage> {
                    new ProductImage { Url = "image1.jpg", AltText = "Image 1", Width = 100, Height = 100 },
                    new ProductImage { Url = "image2.jpg", AltText = "Image 2", Width = 200, Height = 200 },
                    new ProductImage { Url = "image3.jpg", AltText = "Image 3", Width = 200, Height = 200 },
                    new ProductImage { Url = "image4.jpg", AltText = "Image 4", Width = 200, Height = 200 }
                },
                Dimensions = new ProductDimensions { Length = 10, Width = 5, Height = 2, Unit = "cm" },
                CreatedAt = DateTime.UtcNow,
                IsAvailable = true
            }
        );
        return res.ToList();
    }

    public static byte[] GenerateRandomBlob(int byteCount)
    {
        byte[] randomBytes = new byte[byteCount];
        RandomNumberGenerator.Fill(randomBytes);
        return randomBytes;
    }
}
