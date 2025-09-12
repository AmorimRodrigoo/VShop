using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class ProductController : Controller
{
   private readonly IProductService _productService;
   private readonly ICategoryService _categoryService;
   
   public ProductController(IProductService productService, ICategoryService categoryService)
   {
      _productService = productService;
      _categoryService = categoryService;
   }     
   
   [HttpGet]
   public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
   {
      var result = await _productService.GetAllProducts();

      if (result is null)
      {
         return View("Error");
      }
      return View(result);
   }
   
   [HttpGet]
   public async Task<IActionResult> CreateProduct()
   {
      //viewbag é um objeto dinâmico que pode ser usado para passar dados do controlador para a View
      ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
      
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
   {
      if (ModelState.IsValid)
      {
         var result = await _productService.CreateProduct(productVM);

         if (result != null)
            return RedirectToAction(nameof(Index));
      }
      else
      {
         ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
      }
      return View(productVM);
   }
}