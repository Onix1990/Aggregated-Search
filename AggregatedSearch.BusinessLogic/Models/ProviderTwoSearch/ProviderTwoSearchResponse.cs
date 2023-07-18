namespace AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch
{
    /// <summary>
    /// Ответ на запрос поиска маршрутов
    /// </summary>
    public class ProviderTwoSearchResponse
    {
        /// <summary>
        /// Маршруты
        /// </summary>
        public ProviderTwoRoute[] Routes { get; set; } = null!;
    }
}