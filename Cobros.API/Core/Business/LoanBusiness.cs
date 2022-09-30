using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Entities;
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

            // Restrict access if loan is not in allowed Cobro.
            if (User.Role == Role.USER)
                if (!User.CobroIds.Contains(existing.CobroId))
                    throw new AccessForbiddenException("Action forbidden.");

            return _mapper.Map<LoanDto>(existing);
        }

        public async Task InsertLoan(int cobroId, LoanCreateDto loanCreateDto)
        {
            var existingCobro = await _unitOfWork.Cobros.GetByIdAsync(cobroId);

            if (existingCobro == null)
                throw new AppException($"Cobro with Id: {cobroId} not found.");

            // Restrict user
            if (User.Role == Role.USER)
                if (!User.CobroIds.Contains(cobroId))
                    throw new AccessForbiddenException("Access Forbidden");

            // Check Customer existence.
            var existingCustomer = await _unitOfWork.Customers.GetByIdAsync(loanCreateDto.CustomerId);

            if (existingCustomer == null)
                throw new AppException($"Customer with Id: {loanCreateDto.CustomerId} does not exist.");

            var toInsert = _mapper.Map<Loan>(loanCreateDto);
            toInsert.CobroId = cobroId;

            // More logic here

            await _unitOfWork.Loans.InsertAsync(toInsert);
            await _unitOfWork.CompleteAsync();
        }
    }
}
