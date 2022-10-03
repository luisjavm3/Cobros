using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController:ControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CustomersController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomer(CustomerCreateDto customerCreateDto)
        {
            await _customerBusiness.InsertCustomer(customerCreateDto);
            return Ok();
        }
    }
}
