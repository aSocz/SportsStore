using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Areas.Admin.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.WebUI.Extensions
{
    public static class ProductExtensions
    {
        public static Product ToProduct(this ProductDetailsViewModel viewModel) => Mapper.Map<Product>(viewModel);

        public static ProductDetailsViewModel
            ToViewModel(this Product product, IEnumerable<SelectListItem> categories)
        {
            var viewModel = Mapper.Map<ProductDetailsViewModel>(product);
            viewModel.Categories = categories;

            return viewModel;
        }
    }
}