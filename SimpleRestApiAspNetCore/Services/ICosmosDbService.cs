using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleRestApiAspNetCore.Models;

namespace SimpleRestApiAspNetCore.Services
{
    public interface ICosmosDbService
    {
        Task<List<Message>> GetMessagesAsync(int start, int maxCountReturnedRecords);
        Task AddMessageAsync(Message message);
    }
}
