using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Models.ProviderOneSearch;

namespace AggregatedSearch.BusinessLogic.Clients
{
    /// <summary>
    /// Клиент первого провайдера поиска
    /// </summary>
    public interface IProviderOneSearchClient
    {
        /// <summary>
        /// Получить маршруты
        /// </summary>
        /// <param name="request">Данные для фильтра</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public Task<ProviderOneSearchResponse> SearchAsync(
            ProviderOneSearchRequest request,
            CancellationToken cancellationToken
        );

        /// <summary>
        /// Проверка доступности
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
    }
}