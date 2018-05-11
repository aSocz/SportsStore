using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Areas.Admin.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.WebUI.Extensions
{
    public static class ProductExtensions
    {
        public static Product ToProduct(this ProductDetailsViewModel viewModel)
        {
            var product = Mapper.Map<Product>(viewModel);
            product.IsActive = true;

            return product;
        }

        public static ProductDetailsViewModel
            ToViewModel(this Product product, IEnumerable<SelectListItem> categories)
        {
            var viewModel = Mapper.Map<ProductDetailsViewModel>(product);
            viewModel.Categories = categories;

            return viewModel;
        }
    }
}