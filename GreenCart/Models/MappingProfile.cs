using AutoMapper;
using GreenCart.ViewModels;

namespace GreenCart.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductFormViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, ApplicationUser>();
        }
    }
}
