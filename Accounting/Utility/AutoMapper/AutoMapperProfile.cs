using AutoMapper;
using Accounting.Models;
using DAL.Models;

namespace Accounting.Utilitis.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>()
            .ReverseMap();

        }
    }
}
