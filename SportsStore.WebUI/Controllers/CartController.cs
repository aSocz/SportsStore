using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.Infrastructure.Identity;
using SportsStore.WebUI.Extensions;
using SportsStore.WebUI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IUserInformationService userInformationService;

        public CartController(
            IProductService productService,
            IOrderService orderService,
            IUserInformationService userInformationService)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userInformationService = userInformationService;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            var viewModel = new CartIndexViewModel
            {
                Items = cart.Items
                            .Select(
                                 i => new CartItemViewModel
                                 {
                                     ProductName = i.Product.Name,
                                     ProductPrice = i.Product.Price,
                                     Quantity = i.Quantity,
                                     ProductId = i.Product.ProductId
                                 })
                            .ToList(),
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            var product = productService.GetProduct(productId);

            if (product != default(Product))
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            var product = productService.GetProduct(productId);

            if (product != default(Product))
            {
                cart.RemoveItem(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart) => PartialView(cart);

        public async Task<ViewResult> Checkout()
        {
            var viewModel = new ShippingInformationViewModel();
            if (!User.Identity.IsAuthenticated)
            {
                return View(viewModel);
            }

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var userInformation = userInformationService.GetUserInformation(user.Id);

            viewModel.Address = Mapper.Map<Address, AddressViewModel>(userInformation.Address);
            viewModel.Name = user.UserName;
            viewModel.Email = user.Email;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Checkout(Cart cart, ShippingInformationViewModel viewModel)
        {
            if (!cart.Items.Any())
            {
                ModelState.AddModelError(string.Empty, "Koszyk jest pusty");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var address = viewModel.GetAddress();
            var order = new Order
            {
                Address = address,
                CartLines = cart.Items.ToList(),
                ClientName = viewModel.Name,
                GiftWrap = viewModel.GiftWrap,
                ClientEmail = viewModel.Email,
                OrderDate = DateTime.UtcNow
            };

            if (User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                order.UserInformationId = user.Id;
            }

            await orderService.AddOrder(order);

            Session.ClearCart();

            return View("Completed");
        }

        private SportsStoreUserManager UserManager => HttpContext.GetOwinContext()
                                                                 .GetUserManager<SportsStoreUserManager>();
    }
}