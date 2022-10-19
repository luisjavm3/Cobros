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

        public async Task<LoanDetailsDto> GetByIdWithDetails(int id)
        {
            var existing = await _unitOfWork.Loans.GetDetails(id);

            if (existing == null)
                throw new NotFoundException($"Loan with Id: {id} not found.");

            // Restrict access if loan is not in allowed Cobro.
            if (User.Role == Role.USER)
                if (!User.CobroIds.Contains(existing.CobroId))
                    throw new AccessForbiddenException("Action forbidden.");

            return _mapper.Map<LoanDetailsDto>(existing);
        }

        public async Task InsertLoan(int cobroId, LoanCreateDto loanCreateDto)
        {
            var existingCobro = await _unitOfWork.Cobros.GetByIdWithLoansAsync(cobroId);

            if (existingCobro == null)
                throw new AppException($"Cobro with Id: {cobroId} not found.");

            if (loanCreateDto.RoutePosition > existingCobro.Loans.Count() + 1)
                throw new AppException("RoutePosition must be less than or equal to the total of active loans in Cobro + 1.");

            // Restrict user
            if (User.Role == Role.USER)
                if (!User.CobroIds.Contains(cobroId))
                    throw new AccessForbiddenException("Access Forbidden.");

            // Check Customer existence.
            var existingCustomer = await _unitOfWork.Customers.GetByIdWithActiveLoan(loanCreateDto.CustomerId);

            if (existingCustomer == null)
                throw new AppException($"Customer with Id: {loanCreateDto.CustomerId} does not exist.");

            // Customer cannot get more than one active loan.
            if (existingCustomer.Loans.Count() > 0)
                throw new AppException($"Customer has an active loan with LoanId: {existingCustomer.Loans.First().Id}");

            var toInsert = _mapper.Map<Loan>(loanCreateDto);
            toInsert.CobroId = cobroId;
            
            // Insert in the last route position plus one - the easiest way.
            if(loanCreateDto.RoutePosition == existingCobro.Loans.Count() + 1)
            {
                await _unitOfWork.Loans.InsertAsync(toInsert);
                await _unitOfWork.CompleteAsync();
                return;
            }

            // Sort route when toInsert-loan position is less than Cobro total loans.
            try
            {
                _unitOfWork.BeginTransaccion();

                // TOTAL - POSITION + 1
                int iterations = existingCobro.Loans.Count() - loanCreateDto.RoutePosition + 1;
                int position = existingCobro.Loans.Count();

                for (int i = 0; i < iterations; i++)
                {
                    var loan = existingCobro.Loans.FirstOrDefault(l => l.RoutePosition == position);
                    loan.RoutePosition = position + 1;
                    _unitOfWork.Loans.Update(loan);
                    await _unitOfWork.CompleteAsync();
                    position--;
                }

                await _unitOfWork.Loans.InsertAsync(toInsert);
                await _unitOfWork.CompleteAsync();

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw new AppException("Cannot insert loan.");
            }
        }

        public async Task UpdateRoutePosition(int id, LoanUpdateDto loanUpdateDto)
        {
            var existing = await _unitOfWork.Loans.GetByIdAsync(id);

            if (existing == null)
                throw new AppException($"Loan with Id: {id} does not exist.");

            if(User.Role == Role.USER)
                if(!User.CobroIds.Contains(existing.CobroId))
                    throw new AccessForbiddenException("Access Forbiden.");

            int startPosition = existing.RoutePosition;
            int targetPosition = loanUpdateDto.RoutePosition;

            if (targetPosition <= 0)
                throw new AppException("RoutePosition must be greater than 0.");
                
            if(targetPosition == startPosition)
                return;

            var cobroId = existing.CobroId;

            if(targetPosition < startPosition)
            {
                try
                {
                    _unitOfWork.BeginTransaccion();

                    for (int i = startPosition; i > targetPosition; i--)
                    {
                        var loan = await _unitOfWork.Loans.GetByCobroIdAndRoutePosition(cobroId, i-1);
                        loan.RoutePosition = i;
                        _unitOfWork.Loans.Update(loan);
                    }

                    existing.RoutePosition = targetPosition;
                    _unitOfWork.Loans.Update(existing);
                    await _unitOfWork.CompleteAsync();
                    return;
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    throw new AppException($"Cannot reorder Cobro with ID: {existing.CobroId}");
                }
            }


            //existing.RoutePosition = loanUpdateDto.RoutePosition;
            //_unitOfWork.Loans.Update(existing);
            //_unitOfWork.CompleteAsync();

            throw new NotImplementedException();
        }
    }
}
