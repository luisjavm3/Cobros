using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class LoanBusiness : ILoanBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<Loan>> GetAllByCobroId(int cobroId)
        {

            throw new NotImplementedException();
        }
    }
}
