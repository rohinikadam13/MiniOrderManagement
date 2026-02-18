using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Commands.Orders.CreateOrder;
using MiniOrderManagement.Application.Queries.Orders;
using MiniOrderManagement.Domain.Exceptions;

namespace MiniOrderManagement.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderHandler _createOrderHandler;
        private readonly GetOrdersByCustomerIdHandler _getOrdersByCustomerIdHandler;

        public OrdersController(
            CreateOrderHandler createOrderHandler,
            GetOrdersByCustomerIdHandler getOrdersByCustomerIdHandler)
        {
            _createOrderHandler = createOrderHandler;
            _getOrdersByCustomerIdHandler = getOrdersByCustomerIdHandler;
        }

  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            try
            {
                await _createOrderHandler.Handle(command);
                return CreatedAtAction(nameof(GetByCustomer),
                    new { customerId = command.CustomerId },
                    new { message = "Order created successfully" });
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var query = new GetOrdersByCustomerIdQuery(customerId);
            var result = await _getOrdersByCustomerIdHandler.Handle(query);
            return Ok(result);
        }
    }
}
