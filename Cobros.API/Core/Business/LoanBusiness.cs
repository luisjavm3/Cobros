using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Loan;
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

        public async Task<IEnumerable<LoanDto>> GetAllByCobroId(int cobroId)
        {
            var cobros = await _unitOfWork.Loans.GetAllByCobroIdAndSortedByRoutePositionASC(cobroId);

            return _mapper.Map<IEnumerable<LoanDto>>(cobros);
        }
    }
}
