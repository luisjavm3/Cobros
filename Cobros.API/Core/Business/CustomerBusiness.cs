using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Customer;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task InsertCustomer(CustomerCreateDto customerCreateDto)
        {
            var exists = await _unitOfWork.People
                .CheckPersonExistence(customerCreateDto.NationalID);

            if(exists)
                throw new AppException($"A Person with ID: {customerCreateDto.NationalID} already exists.");

            var toInsert = _mapper.Map<Customer>(customerCreateDto);

            await _unitOfWork.Customers.InsertAsync(toInsert);
            await _unitOfWork.CompleteAsync();
        }
    }
}
