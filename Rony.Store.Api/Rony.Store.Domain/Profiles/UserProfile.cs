using AutoMapper;
using Rony.Store.Domain.DTOs.Users;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Domain.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserFormDTO, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
        CreateMap<User, UserDTO>().ReverseMap();
    }
}