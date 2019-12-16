using SimpleRestApiAspNetCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace SimpleRestApiAspNetCore.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        //Add message to the database container
        public async Task AddMessageAsync(Message message)
        {
            await this._container.CreateItemAsync<Message>(message, new PartitionKey(message.Id));
        }

        //Return the range (start, count) of messages from the database container.
        public async Task<List<Message>> GetMessagesAsync(int start)
        {
            var query = "SELECT * FROM c";
            var queryResults = this._container.GetItemQueryIterator<Message>(new QueryDefinition(query));
            List<Message> results = new List<Message>();
            while (queryResults.HasMoreResults)
            {
                var response = await queryResults.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results.Skip(start).Take(Constants.MAX_RETURNED_RECORDS).ToList();
        }
    }
}
