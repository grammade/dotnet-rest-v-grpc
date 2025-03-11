using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using netGrpcOne.Repository;

namespace netGrpcOne.Controller;

[ApiController]
[Route("{controller}")]
public class ProductController : ControllerBase
{
    [HttpGet("{n}")]
    public async Task<IActionResult> GetProducts(int n)
    {
        var prodList = ProductRepository.getProducts(n);
        var res = new
        {
            products = prodList
        };
        return Ok(await Task.FromResult(res));
    }
}
