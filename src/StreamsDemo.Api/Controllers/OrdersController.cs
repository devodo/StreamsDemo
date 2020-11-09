using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StreamsDemo.Api.Repositories;

namespace StreamsDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepository.InsertOrder(order);
            
            var orderUrl = Url.Action("GetOrder", "Orders", new { orderId = order.OrderId }, protocol: Request.Scheme);
            return Created(orderUrl, order);
        }
        
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrder([FromRoute]Guid orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
    }
}
