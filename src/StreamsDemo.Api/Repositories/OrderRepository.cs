using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace StreamsDemo.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private const string TableName = "DemoTable";
        private const string SortKey = "order";
        
        private readonly Table _table;

        public OrderRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _table = Table.LoadTable(dynamoDbClient, TableName);
        }

        public async Task InsertOrder(Order order)
        {
            var document = order.ToDocument(CreatePartitionKey(order.OrderId), SortKey);
            await _table.PutItemAsync(document);
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            var document = await _table.GetItemAsync(CreatePartitionKey(orderId), SortKey);

            return document?.FromDocument<Order>();
        }

        private static string CreatePartitionKey(Guid orderId)
        {
            return $"order-{orderId:N}";
        }
    }
}