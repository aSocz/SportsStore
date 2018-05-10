using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Areas.Admin.Models;

namespace SportsStore.WebUI.Extensions
{
    public static class CategoryExtensions
    {
        public static Category ToCategory(this ManageCategoryViewModel viewModel) => Mapper.Map<Category>(viewModel);

        public static ManageCategoryViewModel ToViewModel(this Category category) =>
            Mapper.Map<ManageCategoryViewModel>(category);
    }
}