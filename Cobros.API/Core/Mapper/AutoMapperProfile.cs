using AutoMapper;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Core.Model.DTO.Loan;
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
            CreateMap<User, UserAuthenticatedDto>()
                .ForMember(x => x.CobroIds, opt => opt.MapFrom(src => src.Cobros.Select(c=>c.Id)));

            // Cobro
            CreateMap<Cobro, CobroDto>()
                .ForMember(x => x.Loans, opt => opt.MapFrom(src => src.Loans.Count()))
                .ForMember(x=>x.Balance, opt => opt.MapFrom(src => src.Loans.Aggregate(0, (acc, loan)=> acc + loan.Balance)));

            CreateMap<CobroCreateDto, Cobro>();

            // Loan
            CreateMap<Loan, LoanDto>()
                .ForMember(x => x.Total, opt => opt.MapFrom(src => src.Value + (src.Value * src.LoanInterest/ 100)))
                .ForMember(y => y.TotalPaid, opt => opt.MapFrom(src => src.PartialPayments.Aggregate(0, (acc, pp) => acc + pp.Value)));
            CreateMap<LoanCreateDto, Loan>()
                .ForMember(x=>x.Balance, opt => opt.MapFrom(src => src.Value + (src.Value * src.LoanInterest / 100)));

            //CreateMap<Loan, LoanDetailsDto>()
            //    .ForMember(y => y.TotalPaid, opt => opt.MapFrom(src => src.PartialPayments.Aggregate(0, (acc, pp) => acc + pp.Value)))
            //    .ForMember(x => x.Customer, opt => opt.MapFrom(s => new { s.Customer.Id, s.Customer.Name, s.Customer.NationalID }))
            //    .ForMember(x => x.PartialPayments, o => o.MapFrom(s => s.PartialPayments.Select(pp => new { pp.Value, pp.CreatedAt })));

            CreateMap<Loan, LoanDetailsDto>()
                .ForMember(y => y.TotalPaid, opt => opt.MapFrom(src => src.PartialPayments.Aggregate(0, (acc, pp) => acc + pp.Value)))
                //.ForMember(x => x.Customer, o => o.Ignore())
                .ForMember(x => x.PartialPayments, o => o.Ignore())
                .BeforeMap((src, des) => src.Customer.Loans = null);
        }
    }
}
