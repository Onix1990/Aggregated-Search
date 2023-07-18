using System;

namespace AggregatedSearch.BusinessLogic.Models.RouteSearch
{
    /// <summary>
    /// Маршрут
    /// </summary>
    public class Route : IEquatable<Route>
    {
        /// <summary>
        /// Идентификатор маршрута
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя начальной точки
        /// </summary>
        public string Origin { get; set; } = null!;

        /// <summary>
        /// Имя конечной точки
        /// </summary>
        public string Destination { get; set; } = null!;

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime OriginDateTime { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime DestinationDateTime { get; set; }

        /// <summary>
        /// Стоимость маршрута
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Лимит времени
        /// </summary>
        public DateTime TimeLimit { get; set; }

        /// <inheritdoc />
        public bool Equals(Route? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Origin == other.Origin && Destination == other.Destination &&
                   OriginDateTime.Equals(other.OriginDateTime) &&
                   DestinationDateTime.Equals(other.DestinationDateTime) && Price == other.Price &&
                   TimeLimit.Equals(other.TimeLimit);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Route) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Destination, OriginDateTime, DestinationDateTime, Price, TimeLimit);
        }
    }
}