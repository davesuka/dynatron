using AutoMapper;
using Azure;
using Dynatron.API.DTO.Request;
using Dynatron.API.DTO.Response;
using Dynatron.API.Interfaces;
using Dynatron.API.Model;
using Dynatron.API.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dynatron.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly CustomerValidator _validatorRules;
        private readonly IMapper _mapper;

        public CustomerController(IGenericRepository<Customer> customerRepository, IMapper mapper, CustomerValidator validatorRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _validatorRules = validatorRules;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _customerRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerResponseDTO>>(response));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _customerRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<CustomerResponseDTO>(response));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRequestDTO customerRequestDTO)
        {
            var validationResult = await _validatorRules.ValidateAsync(customerRequestDTO);

            if (!validationResult.IsValid)
            {                
                return  BadRequest(validationResult.Errors);
            }

            var customer = _mapper.Map<Customer>(customerRequestDTO);
            customer.CreatedDate = DateTime.Now;
            var response = await _customerRepository.RegisterAsync(customer);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditCustomer(int id, [FromBody] CustomerRequestDTO customerRequestDTO)
        {
            if(!await _customerRepository.Exists(id))
            {
                return NotFound();
            }

            var customer = _mapper.Map<Customer>(customerRequestDTO);
            customer.Id = id;
            customer.LastUpdatedDate = DateTime.Now;
            var response = await _customerRepository.EditAsync(customer);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(int id)
        {
            var customerExists = await GetCustomerById(id);
            if (customerExists == null)
            {
                return NotFound();
            }

            var response = await _customerRepository.RemoveAsync(id);
            return Ok(response);
        }

    }
}
