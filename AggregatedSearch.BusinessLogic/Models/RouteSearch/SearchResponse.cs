namespace AggregatedSearch.BusinessLogic.Models.RouteSearch
{
    /// <summary>
    /// Ответ на запрос поиска маршрутов
    /// </summary>
    public class SearchResponse
    {
        /// <summary>
        /// Маршруты
        /// </summary>
        public Route[] Routes { get; set; } = { };

        /// <summary>
        /// Минимальная стоимость маршрута
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Максимальная стоимость маршрута
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Самый быстрый маршрут (в минутах)
        /// </summary>
        public int MinMinutesRoute { get; set; }

        /// <summary>
        /// Самый долгий маршрут (в минутах)
        /// </summary>
        public int MaxMinutesRoute { get; set; }
    }
}