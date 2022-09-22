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

        public async Task<CobroDto> GetCobroById(int id)
        {
            var existing = await _unitOfWork.Cobros.GetByIdIncludingActiveLoansAsync(id);

            if (existing == null)
                throw new NotFoundException($"Cobro with Id: {id} not found.");

            return _mapper.Map<CobroDto>(existing);
        }
    }
}
