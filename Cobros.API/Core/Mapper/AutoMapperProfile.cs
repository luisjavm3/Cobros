using AutoMapper;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Entities;

namespace Cobros.API.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthRegisterDto, User>();
            CreateMap<User, UserDto>();

            // Cobro
            CreateMap<Cobro, CobroDto>()
                .ForMember(x => x.Loans, opt => opt.MapFrom(src => src.Loans.Count()))
                .ForMember(x=>x.Balance, opt => opt.MapFrom(src => src.Loans.Aggregate(0, (acc, loan)=> acc + loan.Balance)));
        }
    }
}
