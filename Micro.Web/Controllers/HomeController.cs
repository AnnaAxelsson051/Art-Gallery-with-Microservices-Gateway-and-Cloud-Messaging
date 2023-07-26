using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Micro.Web.Models;
using Newtonsoft.Json;
using Micro.Web.Service;
using Microsoft.AspNetCore.Authorization;

namespace Micro.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    //Getting all the products to display 
    public async Task<IActionResult> Index()
    {
        List<ProductDto>? list = new();

        ResponseDto? response = await _productService.GetAllProductsAsync();

        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(list);
    }


    //Getting certain product with details
    [Authorize]
    public async Task<IActionResult> ProductDetails(int productId)
    {
        ProductDto? model = new();

        ResponseDto? response = await _productService.GetProductByIdAsync(productId);

        if (response != null && response.IsSuccess)
        {
            model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}

