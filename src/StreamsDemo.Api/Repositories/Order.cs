using System;

namespace StreamsDemo.Api.Repositories
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }
        public string Name { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Version { get; set; }
    }
}