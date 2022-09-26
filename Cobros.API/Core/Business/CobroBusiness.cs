using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
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

        public async Task<IEnumerable<CobroDto>> GetAllCobros()
        {
            var cobros = await _unitOfWork.Cobros.GetAllIncludingActiveLoansAsync();

            return _mapper.Map<IEnumerable<CobroDto>>(cobros);
        }

        public async Task CreateCobro(CobroCreateDto cobroCreateDto)
        {
            var existingCobro = await _unitOfWork.Cobros.GetByNameAsync(name: cobroCreateDto.Name);

            if (existingCobro != null)
                throw new AppException($"Name: {cobroCreateDto.Name} already exists.");

            var toInsertCobro = _mapper.Map<Cobro>(cobroCreateDto);

            if(cobroCreateDto.UserId != null)
            {
                var existingUser = await _unitOfWork.Users
                                    .GetByIdAsync((int)cobroCreateDto.UserId);

                if (existingUser == null)
                    throw new NotFoundException($"User with Id: {cobroCreateDto.UserId} not found.");
            }

            if(cobroCreateDto.DebtCollectorId != null)
            {
                var existingDebtCollector = await _unitOfWork
                                                .DebtCollectors.GetByIdAsync((int)cobroCreateDto.DebtCollectorId);

                if (existingDebtCollector == null)
                    throw new NotFoundException($"DebtCollector with Id: {cobroCreateDto.DebtCollectorId} not found.");
            }

            await _unitOfWork.Cobros.InsertAsync(toInsertCobro);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCobro(int id)
        {
            var existingCobro = await _unitOfWork.Cobros.GetByIdAsync(id);

            if (existingCobro == null)
                throw new AppException($"Cobro with Id: {id} does not exist.");

            await _unitOfWork.Cobros.DeleteAsync(existingCobro);
            await _unitOfWork.CompleteAsync();
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
