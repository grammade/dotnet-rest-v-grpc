using log4net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using netGrpcOne.Repository;
using Newtonsoft.Json;

namespace netGrpcOne.Controller;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILog log = LogManager.GetLogger(type: typeof(ProductController));
    [HttpGet("small/{n}")]
    public async Task<IActionResult> GetProductSmall(int n)
    {
        log.Info($"small: {n}");
        var prodList = ProductRepository.getProductSmallSize(n);
        var res = new
        {
            products = prodList
        };
        return Ok(await Task.FromResult(res));
    }
    [HttpGet("big/{n}")]
    public async Task<IActionResult> GetProductBig(int n)
    {
        log.Info($"big: {n}");
        var prodList = ProductRepository.getProductsBigSize(n);
        var res = new
        {
            products = prodList
        };
        return Ok(await Task.FromResult(res));
    }
    [HttpGet("complex/{n}")]
    public async Task<IActionResult> GetProductComplex(int n)
    {
        log.Info($"complex: {n}");
        var prodList = ProductRepository.getProductComplexObject(n);
        var res = new
        {
            products = prodList
        };
        return Ok(await Task.FromResult(res));
    }
}
