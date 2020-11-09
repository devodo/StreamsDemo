using System;
using System.Text.Json;
using Amazon.DynamoDBv2.DocumentModel;

namespace StreamsDemo.Api.Repositories
{
    public static class DocumentExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
        };
        
        public static Document ToDocument<T>(this T input, string partitionKey, string sortKey = null) where T : class
        {
            if (input == null)
            {
                throw new ArgumentException(nameof(input));
            }

            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentException("Partition key not valid", nameof(partitionKey));
            }
            
            var json = JsonSerializer.Serialize(input, JsonOptions);
            var document = Document.FromJson(json);
            document["PartitionKey"] = partitionKey;

            if (sortKey != null)
            {
                document["SortKey"] = sortKey;
            }

            return document;
        }
        
        public static T FromDocument<T>(this Document document) where T : class
        {
            return document == null ? null : JsonSerializer.Deserialize<T>(document.ToJson());
        }
    }
}