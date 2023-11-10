using AutoMapper;
using Domain.Dtos.Account;
using Domain.Dtos.Category;
using Domain.Dtos.PasswordCollection;
using Domain.Entities;

namespace Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Account, AccountDto>()
                   .ReverseMap();

            CreateMap<Account, UserRegisterRequestDto>()
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<Account, UserRegisterResponseDto>()
                  .ReverseMap();

            CreateMap<Account, UserLoginResponseDto>()
                 .ReverseMap();

            CreateMap<PasswordCollection, PasswordListDto>()
                .ReverseMap();
            
            CreateMap<PasswordCollection, PasswordCollectionDto>()
                .ReverseMap();

            CreateMap<Category, CategoryListDto>()
                .ForMember(dest => dest.PasswordCount,
                            opt => opt.MapFrom(src => src.PasswordCollections.Count))
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
               .ReverseMap();

        }
    }
}
