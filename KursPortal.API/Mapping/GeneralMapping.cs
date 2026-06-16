using AutoMapper;
using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.DTOs.DTOs.BlogCommentDtos;
using KursPortal.DTOs.DTOs.BlogDtos;
using KursPortal.DTOs.DTOs.CartDtos;
using KursPortal.DTOs.DTOs.CategoryDtos;
using KursPortal.DTOs.DTOs.ContactDtos;
using KursPortal.DTOs.DTOs.CourseDtos;
using KursPortal.DTOs.DTOs.FaqDtos;
using KursPortal.DTOs.DTOs.FavoriteDtos;
using KursPortal.DTOs.DTOs.SubscriberDtos;
using KursPortal.Entity.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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

            CreateMap<FavoriteItem, FavoriteItemDto>()
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.Course.ImageUrl))
                .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Course.Price))
                .ForMember(dest => dest.DiscountPrice,
                opt => opt.MapFrom(src => src.Course.DiscountPrice));

            CreateMap<Favorite, ResultFavoriteDto>();

            CreateMap<CreateContactDto, Contact>();
            CreateMap<UpdateContactDto, Contact>();
            CreateMap<Contact, ResultContactDto>();

            CreateMap<CreateSubscriberDto, Subscriber>();
            CreateMap<Subscriber, ResultSubscriberDto>();

            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>();
            CreateMap<Blog, ResultBlogDto>();

            CreateMap<CreateBlogCategoryDto, BlogCategory>();
            CreateMap<UpdateBlogCategoryDto, BlogCategory>();
            CreateMap<BlogCategory, ResultBlogCategoryDto>();

            CreateMap<CreateBlogCommentDto, BlogComment>();
            CreateMap<UpdateBlogCommentDto, BlogComment>();
            CreateMap<BlogComment, ResultBlogCommentDto>();

            CreateMap<Blog, ResultBlogDto>()
                   .ForMember(dest => dest.TeacherName,
                          opt => opt.MapFrom(src => src.Teacher.FirsName + " " + src.Teacher.LastName))
                   .ForMember(dest => dest.TeacherImage,
                          opt => opt.MapFrom(src => src.Teacher.ProfilePicture));


            CreateMap<CreateFaqDto, Faq>();
            CreateMap<UpdateFaqDto, Faq>();
            CreateMap<Faq, ResultFaqDto>();


        }
    }
}
