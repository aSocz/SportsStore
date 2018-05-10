using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Infrastructure
{
    public static class AutoMapperInitializer
    {
        public static void Initialize()
        {
            CreateModelsToViewModels();
            CreateViewModelsToModels();

            Mapper.AssertConfigurationIsValid();
        }

        private static void CreateModelsToViewModels()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ManageProductViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Product, ProductDetailsViewModel>().ForMember(p => p.Categories, opt => opt.Ignore()).ReverseMap();
                cfg.CreateMap<Category, ManageCategoryViewModel>().ReverseMap();
            });
        }

        private static void CreateViewModelsToModels()
        {
        }
    }
}