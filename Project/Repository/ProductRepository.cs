using netGrpcOne.Entity;
using System.Security.Cryptography;

namespace netGrpcOne.Repository;

public class ProductRepository
{
    private static Random rand = new Random();
    public static List<Product> getProducts(int n)
    {
        return Enumerable.Range(0, n).Select(i =>
            new Product
            {
                id = i,
                price = rand.Next(0, 10000),
                name = i.ToString(),
                blob = GenerateRandomBlob(1024 * 50)
            }).ToList();
    }

    public static byte[] GenerateRandomBlob(int byteCount)
    {
        byte[] randomBytes = new byte[byteCount];
        RandomNumberGenerator.Fill(randomBytes);
        return randomBytes;
    }
}
