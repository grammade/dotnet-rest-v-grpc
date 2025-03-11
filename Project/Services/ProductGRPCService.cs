using Google.Protobuf;
using Grpc.Core;
using netGrpcOne.Repository;

namespace netGrpcOne.Services;

public class ProductGRPCService : ProductProtoService.ProductProtoServiceBase
{
    public override async Task<ProtoProductResponse> GetProducts(ProtoProductRequest request, ServerCallContext context)
    {
        var res = new ProtoProductResponse();
        var products = ProductRepository.getProducts(request.Count) 
            .Select(p => new ProtoProduct
            {
                Id = p.id,
                Name = p.name,
                Price = p.price,
                Blob = ByteString.CopyFrom(p.blob)
            });
        res.Products.AddRange(products);
        return res;
    }
}
