using System;

namespace AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch
{
    /// <summary>
    /// Фильтр для получения маршрутов
    /// </summary>
    public class ProviderTwoSearchRequest
    {
        /// <summary>
        /// Имя начальной точки
        /// </summary>
        public string Departure { get; set; } = null!;

        /// <summary>
        /// Имя конечной точки
        /// </summary>
        public string Arrival { get; set; } = null!;

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Минимальное значение лимита времени
        /// </summary>
        public DateTime? MinTimeLimit { get; set; }
    }
}