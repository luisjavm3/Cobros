using AutoMapper;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Core.Model.DTO.Customer;
using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.DTO.PartialPayment;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Entities;

namespace Cobros.API.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User
            CreateMap<AuthRegisterDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserAuthenticatedDto>()
                .ForMember(x => x.CobroIds, opt => opt.MapFrom(src => src.Cobros.Select(c=>c.Id)));

            // Cobro
            CreateMap<Cobro, CobroDto>()
                .ForMember(x => x.Loans, opt => opt.MapFrom(src => src.Loans.Count()))
                .ForMember(x=>x.Balance, opt => opt.MapFrom(src => src.Loans.Aggregate(0, (acc, loan)=> acc + loan.Balance)));

            CreateMap<Cobro, CobroSimpleDto>();

            CreateMap<CobroCreateDto, Cobro>();

            // Loan
            CreateMap<Loan, LoanDto>()
                .ForMember(x => x.Total, opt => opt.MapFrom(src => src.Value + (src.Value * src.LoanInterest/ 100)))
                .ForMember(y => y.TotalPaid, opt => opt.MapFrom(src => src.PartialPayments.Aggregate(0, (acc, pp) => acc + pp.Value)));

            CreateMap<LoanCreateDto, Loan>()
                .ForMember(x=>x.Balance, opt => opt.MapFrom(src => src.Value + (src.Value * src.LoanInterest / 100)));

            CreateMap<Loan, LoanDetailsDto>()
                .ForMember(x => x.Total, opt => opt.MapFrom(src => src.Value + (src.Value * src.LoanInterest/ 100)))
                .ForMember(y => y.TotalPaid, opt => opt.MapFrom(src => src.PartialPayments.Aggregate(0, (acc, pp) => acc + pp.Value)));

            // PartialPayment
            CreateMap<PartialPayment, PartialPaymentDto>();

            // Customer
            CreateMap<Customer, CustomerDto>();
        }
    }
}
