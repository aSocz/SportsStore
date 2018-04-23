using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public ViewResult Index()
        {
            var products = productService.GetAllProducts();
            var productsViewModel = Mapper.Map<Product[], ManageProductViewModel[]>(products.ToArray());
            return View(productsViewModel);
        }

        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Edit(int productid)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Delete(int productid)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Details(int productid)
        {
            throw new System.NotImplementedException();
        }
    }
}