using Google.Protobuf;
using Grpc.Core;
using log4net;
using log4net.Core;
using netGrpcOne.Controller;
using netGrpcOne.Repository;
using Proto.Product;

namespace netGrpcOne.Services;

public class ProductGRPCService : ProductProtoService.ProductProtoServiceBase
{
    private readonly ILog log = LogManager.GetLogger(type: typeof(ProductGRPCService));

    public override Task<ProtoProductResponse> GetProductBigSize(ProtoProductRequest request, ServerCallContext context)
    {
        log.Info("grpc get product big");
        var res = new ProtoProductResponse();
        var products = ProductRepository.getProductsBigSize(request.Count)
            .Select(p => new ProtoProduct
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Blob = ByteString.CopyFrom(p.Blob)
            });
        res.Products.AddRange(products);
        return Task.FromResult(res);
    }

    public override Task<ProtoProductResponse> GetProductComplex(ProtoProductRequest request, ServerCallContext context)
    {
        log.Info("grpc get product complex");
        var res = new ProtoProductResponse();
        var products = ProductRepository.getProductComplexObject(request.Count)
            .Select(p => new ProtoProduct 
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price, 
                Description = p.Description,
                Category = p.Category,
                Tags = { p.Tags },
                Attributes = { p.Attributes },
                Images = { p.Images.Select(img => new ProductImage {
                    Url = img.Url,
                    AltText = img.AltText,
                    Width = img.Width,
                    Height = img.Height
                })},
                Dimensions = new ProductDimensions
                {
                    Length = (double)p.Dimensions.Length,
                    Width = (double)p.Dimensions.Width,
                    Height = (double)p.Dimensions.Height,
                    Unit = p.Dimensions.Unit
                },
                CreatedAt = ((DateTimeOffset)p.CreatedAt).ToUnixTimeSeconds(),
                IsAvailable = p.IsAvailable
            });
        res.Products.AddRange(products);
        return Task.FromResult(res);
    }

    public override Task<ProtoProductSmall> GetProductSmallSize(ProtoProductRequest request, ServerCallContext context)
    {
        log.Info("grpc get product small");
        var res = new ProtoProductSmall();
        var dimensions = ProductRepository.getProductSmallSize(request.Count)
            .Select(p => new ProductDimensions
            {
                Width = (double)p.Width,
                Height = (double)p.Height,
                Length = (double)p.Length,
                Unit = p.Unit
            });
        res.Dimensions.AddRange(dimensions);
        return Task.FromResult(res);
    }
}
