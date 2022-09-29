using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class LoanBusiness : ILoanBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoanBusiness(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private UserAuthenticatedDto User => (UserAuthenticatedDto)_httpContextAccessor.HttpContext.Items["User"];

        public async Task<PaginationResult<LoanDto>> GetAllByCobroId(int cobroId, PaginationParameters paginationParameters)
        {
            var loans = await _unitOfWork.Loans.GetAllByCobroIdAndSortedByRoutePositionASC(cobroId);
            var source = _mapper.Map<IEnumerable<LoanDto>>(loans);

            var result = new PaginationResult<LoanDto>(source, paginationParameters);

            return result;
        }

        public async Task<LoanDto> GetById(int id)
        {
            var existing = await _unitOfWork.Loans.GetByIdAsync(id);

            if (existing == null)
                throw new NotFoundException($"Loan with Id: {id} not found.");

            //if (existing.)

            //    return _mapper.Map<LoanDto>(existing);

            throw new NotImplementedException();
        }
    }
}
