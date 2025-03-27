using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Grpc.Net.Client;
using Google.Protobuf;
using System;
using Proto.Product;

[MemoryDiagnoser] 
public class GrpcVsRestBenchmark
{
    private static readonly HttpClient httpClient = new HttpClient();
    private static readonly GrpcChannel grpcChannel = GrpcChannel.ForAddress(
        "https://localhost:6001"
    );
    private static readonly ProductProtoService.ProductProtoServiceClient grpcClient = new ProductProtoService.ProductProtoServiceClient(grpcChannel);

    [Params(1)] 
    public int ProductCount;
    [Params(1000, 10000, 50000)]
    public int ParallelCount;

    // [Benchmark]
    // public async Task GrpcCallSmallSize()
    // {
    //     var request = new ProtoProductRequest { Count = ProductCount };
    //     var result = await grpcClient.GetProductSmallSizeAsync(request);
    // }

    // [Benchmark]
    // public async Task RestCallSmallSize()
    // {
    //     var response = await httpClient.GetStringAsync($"https://localhost:6001/product/small/{ProductCount}");
    //     var result = JsonSerializer.Deserialize<RestProductResponse>(response);
    // }

    // [Benchmark]
    // public async Task GrpcCallBigSize()
    // {
    //     var request = new ProtoProductRequest { Count = ProductCount };
    //     var result = await grpcClient.GetProductBigSizeAsync(request);
    // }

    // [Benchmark]
    // public async Task RestCallBigSize()
    // {
    //     var response = await httpClient.GetStringAsync($"https://localhost:6001/product/big/{ProductCount}");
    //     var result = JsonSerializer.Deserialize<RestProductResponse>(response);
    // }

    // [Benchmark]
    // public async Task GrpcCallComplexObject()
    // {
    //     var request = new ProtoProductRequest { Count = ProductCount };
    //     var result = await grpcClient.GetProductComplexAsync(request);
    // }

    // [Benchmark]
    // public async Task RestCallComplexObject()
    // {
    //     var response = await httpClient.GetStringAsync($"https://localhost:6001/product/complex/{ProductCount}");
    //     var result = JsonSerializer.Deserialize<RestProductResponse>(response);
    // }

    [Benchmark]
    public async Task GrpcCallBigSizeParallel()
    {
       var request = new ProtoProductRequest { Count = ProductCount };
       await Parallel.ForEachAsync(Enumerable.Range(0, ParallelCount), async (_, _) =>
       {
           var result = await grpcClient.GetProductSmallSizeAsync(request);
       });
    }

    [Benchmark]
    public async Task RestCallBigSizeParallel()
    {
       await Parallel.ForEachAsync(Enumerable.Range(0, ParallelCount), async (_, _) =>
       {
           var response = await httpClient.GetStringAsync($"https://localhost:6001/product/small/{ProductCount}");
           var result = JsonSerializer.Deserialize<RestProductResponse>(response);
       });
    }
}

// Define the REST response format
public class RestProductResponse
{
    public Product[] Products { get; set; }
}

// Define the Product model for REST
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public byte[] Blob { get; set; }
}
