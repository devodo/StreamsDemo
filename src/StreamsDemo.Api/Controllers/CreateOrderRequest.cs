using System;

namespace StreamsDemo.Api.Controllers
{
    public class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}