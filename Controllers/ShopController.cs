using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopContext _context;
        public ShopController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult ShopList()
        {
            try
            {
                var ptdList = from a in _context.Products
                              join b in _context.Categories
                              on a.PrId equals b.CategoryId
                              into Cate
                              from b in Cate.DefaultIfEmpty()

                              select new Product
                              {
                                  ProductId = a.ProductId,
                                  ProductName = a.ProductName,
                                  PrId = a.PrId,
                                  CategoryName = b.CategoryName,

                                  CategoryId = b == null ? 0 : b.CategoryId,

                              };
                return View(ptdList);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public IActionResult Create(Product product)
        {
            loadPPl();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try

            {
                if (ModelState.IsValid)
                {
                    if (product.ProductId == 0)
                    {
                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Entry(product).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("ShopList");

                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ShopList");
            }
        }
        private void loadPPl()
        {
            try
            {
                List<Product> prdlist = new List<Product>();
                prdlist = _context.Products.ToList();
                prdlist.Insert(0, new Product { ProductId = 0, ProductName = "Please Select" });

                ViewBag.ProductList = prdlist;
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<IActionResult> Delete(int ProductId)
        {
            try
            {
                var ptd = await _context.Products.FindAsync(ProductId);
                if (ptd != null)
                {
                    _context.Products.Remove(ptd); 
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("ShopList");

            }
            catch (Exception ex)
            {
                return RedirectToAction("ShopList");
            }
        }

    }
}
   

