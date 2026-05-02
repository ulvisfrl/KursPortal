using AutoMapper;
using KursPortal.DTOs.DTOs.CartDtos;
using KursPortal.DTOs.DTOs.CategoryDtos;
using KursPortal.DTOs.DTOs.CourseDtos;
using KursPortal.Entity.Entities;

namespace KursPortal.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<Course, ResultCourseDto>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, ResultCategoryDto>();

            CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Course.Title))
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.Course.ImageUrl))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Course.Price));

            CreateMap<Cart, ResultCartDto>();
        }
    }
}
