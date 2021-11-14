using ElectronicShopping.LogConsumer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.LogConsumer.ElasticContexts
{
    public interface IElasticContext
    {
        Task<IndexResponseModel> IndexCustomAsync<T>(string indexName, T document, CancellationToken ct = default) where T : class;
        IndexResponseModel IndexCustom<T>(string indexName, T document) where T : class;
    }
}
