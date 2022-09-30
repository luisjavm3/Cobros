using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.PartialPayment;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class PartialPaymentBusiness : IPartialPaymentBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartialPaymentBusiness(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private UserAuthenticatedDto User => (UserAuthenticatedDto)_httpContextAccessor.HttpContext.Items["User"];

        public async Task InsertPartialPayment(int loanId, PartialPaymentCreateDto partialPaymentCreateDto)
        {
            var existingLoan = await _unitOfWork.Loans.GetByIdAsync(loanId);

            if (existingLoan == null)
                throw new ApplicationException($"Loan with Id: {loanId} does not exists.");

            // Restrict user if it is not an admin and does not access to specific Loan.
            if(User.Role == Role.USER)
                if (!User.CobroIds.Contains(existingLoan.CobroId))
                    throw new AccessForbiddenException("Loan is in a not allowed Cobro.");

            if (partialPaymentCreateDto.Value > existingLoan.Balance)
                throw new AppException($"Value: {partialPaymentCreateDto.Value} exceeds Loan-Balance: {existingLoan.Balance}");

            if(partialPaymentCreateDto.Value == existingLoan.Balance)
            {
                try
                {
                    _unitOfWork.BeginTransaccion();

                    existingLoan.Balance = 0;
                    // Soft Delete Loan ↓
                    existingLoan.DeletedAt = DateTime.UtcNow;
                    existingLoan.RoutePosition = -1;
                    _unitOfWork.Loans.Update(existingLoan);
                    await _unitOfWork.CompleteAsync();

                    var toInsert = new PartialPayment
                    {
                        Value = partialPaymentCreateDto.Value,
                        LoanId = loanId
                    };

                    await _unitOfWork.PartialPayments.InsertAsync(toInsert);
                    await _unitOfWork.CompleteAsync();

                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    throw new AppException("Cannot save PartialPayment.");
                }
            }

            if(partialPaymentCreateDto.Value < existingLoan.Balance)
            {
                try
                {
                    _unitOfWork.BeginTransaccion();

                    existingLoan.Balance -= partialPaymentCreateDto.Value;
                    existingLoan.UpdatedAt = DateTime.UtcNow;
                    _unitOfWork.Loans.Update(existingLoan);
                    await _unitOfWork.CompleteAsync();

                    var toInsert = new PartialPayment
                    {
                        Value = partialPaymentCreateDto.Value,
                        LoanId = loanId
                    };

                    await _unitOfWork.PartialPayments.InsertAsync(toInsert);
                    await _unitOfWork.CompleteAsync();

                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    throw new AppException("Cannot save PartialPayment.");
                }
            }
        }
    }
}
