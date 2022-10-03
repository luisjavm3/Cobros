using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.DebtCollector;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class DebtCollectorBusiness : IDebtCollectorBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DebtCollectorBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task InsertDebtCollector(DebtCollectorCreateDto debtCollectorCreateDto)
        {
            var exists = await _unitOfWork.People
                .CheckPersonExistence(debtCollectorCreateDto.NationalID);

            if (exists)
                throw new AppException($"A Person with NationalID: {debtCollectorCreateDto.NationalID} already exists.");

            DebtCollector toInsert = _mapper.Map<DebtCollector>(debtCollectorCreateDto);

            await _unitOfWork.DebtCollectors.InsertAsync(toInsert);
            await _unitOfWork.CompleteAsync();
        }
    }
}
