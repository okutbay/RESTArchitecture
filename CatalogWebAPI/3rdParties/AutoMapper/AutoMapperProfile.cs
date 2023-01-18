using AutoMapper;
using CatalogLibrary.Entity;
using CatalogLibrary.ViewModel;

namespace CatalogWebAPI._3rdParties.AutoMapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        #region CategoryViewModel, Category

        CreateMap<CategoryViewModel, Category>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}"))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}"))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => $"{src.Date}"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Description}"))
            ;

        CreateMap<Category, CategoryViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}"))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}"))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => $"{src.Date}"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Description}"))
            ;

        #endregion

        #region ItemViewModel, Item

        CreateMap<ItemViewModel, Item>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}"))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}"))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => $"{src.Date}"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Description}"))
            ;

        CreateMap<Item, ItemViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}"))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}"))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => $"{src.Date}"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Description}"))
            ;

        #endregion
        



    }
}
