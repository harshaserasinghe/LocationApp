using System.Collections.Generic;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface ICosmosDBService
    {
        Task<T> GetEntityAsync<T>(string containerId, string partitionKey, string id);
        Task<List<T>> GetEntitiesAsync<T>(string containerId, string query);
        Task AddEntityAsync<T>(T entity, string containerId);
        Task UpdateEntityAsync<T>(T entity, string containerId, string partitionKey);
        Task DeleteEntityAsync<T>(string containerId, string partitionKey, string id);
    }
}
