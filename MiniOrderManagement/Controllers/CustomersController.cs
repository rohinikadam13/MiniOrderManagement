using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Commands.Customers.CreateCustomer;
using MiniOrderManagement.Application.Queries.Customers;

namespace MiniOrderManagement.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CreateCustomerHandler _createCustomerHandler;
        private readonly GetCustomerByIdHandler _getCustomerByIdHandler;

        public CustomersController(
            CreateCustomerHandler createCustomerHandler,
            GetCustomerByIdHandler getCustomerByIdHandler)
        {
            _createCustomerHandler = createCustomerHandler;
            _getCustomerByIdHandler = getCustomerByIdHandler;
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var id = await _createCustomerHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

     
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCustomerByIdQuery(id);
            var customer = await _getCustomerByIdHandler.Handle(query);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
    }
}
