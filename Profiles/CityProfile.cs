using AutoMapper;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Profiles
{
    public class CityProfile: Profile
    {
        public CityProfile()
        {
            CreateMap<Users, UsersCreationDTO>(); //from entity to dto(get)
            CreateMap<UsersCreationDTO, Users>(); //from dto to entity (post)

            CreateMap<CityDto, CreationDto>();
            CreateMap<CreationDto, CityDto>();
        }
    }
}
