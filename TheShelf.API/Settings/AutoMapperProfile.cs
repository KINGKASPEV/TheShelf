using AutoMapper;
using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;

namespace TheShelf.API.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
            CreateMap<User, UserReturnDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore the Id property when mapping from Dto to entity

        }
    }
}

