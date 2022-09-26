using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class CobroBusiness : ICobroBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CobroBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task CreateCobro(CobroCreateDto cobroCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CobroDto> GetCobroById(int id)
        {
            var existing = await _unitOfWork.Cobros.GetByIdIncludingActiveLoansAsync(id);

            if (existing == null)
                throw new NotFoundException($"Cobro with Id: {id} not found.");

            return _mapper.Map<CobroDto>(existing);
        }

        public async Task UpdateCobro(int id, CobroUpdateDto cobroUpdateDto)
        {
            var existingCobro = await _unitOfWork.Cobros.GetByIdAsync(id);

            if (existingCobro == null)
                throw new NotFoundException($"Cobro with Id: {id} not found");

            var existingUser = await _unitOfWork.Users.GetByIdAsync(cobroUpdateDto.UserId);

            if (existingUser == null)
                throw new NotFoundException($"User with Id: {cobroUpdateDto.UserId} not found");

            var existingDebtCollector = await _unitOfWork.DebtCollectors.GetByIdAsync(cobroUpdateDto.DebtCollectorId);

            if (existingDebtCollector == null)
                throw new NotFoundException($"DebtCollector with Id: {cobroUpdateDto.DebtCollectorId} not found");

            // Update cobro.
            existingCobro.Name = cobroUpdateDto.Name;
            existingCobro.User = existingUser;
            existingCobro.DebtCollector = existingDebtCollector;

            _unitOfWork.Cobros.Update(existingCobro);
            await _unitOfWork.CompleteAsync();
        }
    }
}
