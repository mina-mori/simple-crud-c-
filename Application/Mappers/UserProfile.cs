using Application.Helpers;
using Application.ViewModels;
using AutoMapper;
using DomainEntities.Entities;
namespace Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(dest => dest.AlphabetizeUserName, opt => opt.MapFrom(src => StringHelper.AlphabetizeUserName(src.FirstName + " " + src.LastName)));
            ;
            CreateMap<UserViewModel, User>();
            CreateMap<AddEditUserViewModel, User>();
        }


    }
}
