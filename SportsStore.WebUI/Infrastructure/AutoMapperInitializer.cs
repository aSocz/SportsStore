using AutoMapper;
using SportsStore.Business.Models;
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
                cfg.CreateMap<Product, ManageProductViewModel>()
                   .ForMember(
                        p => p.HasThumbnail,
                        opt => opt.MapFrom(p => p.Thumbnail != null && p.Thumbnail.IsFilled()));
                cfg.CreateMap<Product, ProductViewModel>()
                   .ForMember(
                        p => p.HasThumbnail,
                        opt => opt.MapFrom(p => p.Thumbnail != null && p.Thumbnail.IsFilled()));
                cfg.CreateMap<Product, ProductDetailsViewModel>()
                   .ForMember(p => p.Categories, opt => opt.Ignore())
                   .ForMember(p => p.ImageType, opt => opt.MapFrom(src => src.Thumbnail.ImageType))
                   .ForMember(p => p.ImageData, opt => opt.MapFrom(src => src.Thumbnail.ImageData))
                   .ReverseMap();
                cfg.CreateMap<Category, ManageCategoryViewModel>().ReverseMap();
                cfg.CreateMap<AddressDto, AddressViewModel>().ReverseMap();
                cfg.CreateMap<AddressDto, Address>().ReverseMap();
                cfg.CreateMap<AddressViewModel, Address>().ReverseMap();
                cfg.CreateMap<AccountDto, AccountDetailsViewModel>()
                   .ForMember(a => a.Name, opt => opt.MapFrom(src => src.UserName))
                   .ForMember(a => a.Address, opt => opt.MapFrom(src => src.AddressDto))
                   .ForMember(a => a.ReturnUrl, opt => opt.MapFrom(src => string.Empty));
                cfg.CreateMap<AccountDetailsViewModel, AccountDto>()
                   .ForMember(a => a.UserName, opt => opt.MapFrom(src => src.Name))
                   .ForMember(a => a.AddressDto, opt => opt.MapFrom(src => src.Address));
            });
        }

        private static void CreateViewModelsToModels()
        {
        }
    }
}