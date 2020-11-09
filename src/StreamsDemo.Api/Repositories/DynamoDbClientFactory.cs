using Amazon.DynamoDBv2;
using Microsoft.Extensions.Options;
using StreamsDemo.Api.Config;

namespace StreamsDemo.Api.Repositories
{
    public class DynamoDbClientFactory
    {
        private DynamoDbConfig _config;
        
        public DynamoDbClientFactory(IOptions<DynamoDbConfig> config)
        {
            _config = config.Value;
        }
        
        public IAmazonDynamoDB CreateClient()
        {
            return new AmazonDynamoDBClient(new AmazonDynamoDBConfig
            {
                ServiceURL = _config.EndpointUrl
            });
        }
    }
}