using Cobros.API.Core.Model.DTO.Customer;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ICustomerBusiness
    {
        Task InsertCustomer(CustomerCreateDto customerCreateDto);
    }
}
