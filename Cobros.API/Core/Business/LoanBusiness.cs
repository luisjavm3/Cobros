using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.Pagination;
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

        public async Task<PaginationResult<LoanDto>> GetAllByCobroId(int cobroId, PaginationParameters paginationParameters)
        {
            var loans = await _unitOfWork.Loans.GetAllByCobroIdAndSortedByRoutePositionASC(cobroId);
            var source = _mapper.Map<IEnumerable<LoanDto>>(loans);

            var result = new PaginationResult<LoanDto>(source, paginationParameters);

            return result;
        }
    }
}
