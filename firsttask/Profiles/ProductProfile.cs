using AutoMapper;
namespace firsttask.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, Models.ProductViewModel>().ForMember(dest => dest.CategoryName, d => d.MapFrom(src => src.Category.CategoryName));
            CreateMap<Models.Category, Models.CategoryViewModel>().ForMember(dest => dest.ProductsNames, d => d.MapFrom(src => src.Products.Select(p => p.ProductName).ToList()));
            CreateMap<Models.PostProductModel, Models.Product>();
            CreateMap<Models.PostCategoryModel, Models.Category>();
            CreateMap<Models.PostProductModel, Models.Product>();
            CreateMap<Models.RegisterViewModel, Models.AppliacationUser>();
            CreateMap<Models.ResidViewModel, Models.Resid>();
            CreateMap<Models.HavaleViewModel, Models.Havale>();
        }
    }
}
