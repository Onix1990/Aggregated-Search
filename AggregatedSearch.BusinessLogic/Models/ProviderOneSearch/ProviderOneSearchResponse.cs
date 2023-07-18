namespace AggregatedSearch.BusinessLogic.Models.ProviderOneSearch
{
    /// <summary>
    /// Ответ на запрос поиска маршрутов
    /// </summary>
    public class ProviderOneSearchResponse
    {
        /// <summary>
        /// Маршруты
        /// </summary>
        public ProviderOneRoute[] Routes { get; set; } = null!;
    }
}