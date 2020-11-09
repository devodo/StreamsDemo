using System;
using System.Threading.Tasks;

namespace StreamsDemo.Api.Repositories
{
    public interface IOrderRepository
    {
        Task InsertOrder(Order order);
        Task<Order> GetOrder(Guid orderId);
    }
}