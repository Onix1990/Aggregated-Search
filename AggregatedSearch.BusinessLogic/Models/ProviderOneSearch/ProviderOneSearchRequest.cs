using System;

namespace AggregatedSearch.BusinessLogic.Models.ProviderOneSearch
{
    /// <summary>
    /// Фильтр для получения маршрутов
    /// </summary>
    public class ProviderOneSearchRequest
    {
        /// <summary>
        /// Имя начальной точки
        /// </summary>
        public string From { get; set; } = null!;

        /// <summary>
        /// Имя конечной точки
        /// </summary>
        public string To { get; set; } = null!;

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Максимальная стоимость маршрута
        /// </summary>
        public decimal? MaxPrice { get; set; }
    }
}