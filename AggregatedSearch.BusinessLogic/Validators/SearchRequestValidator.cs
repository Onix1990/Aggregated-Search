using AggregatedSearch.BusinessLogic.Models.RouteSearch;
using FluentValidation;

namespace AggregatedSearch.BusinessLogic.Validators
{
    /// <summary>
    /// Валидатор фильтра для получения маршрутов
    /// </summary>
    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SearchRequestValidator()
        {
            RuleFor(x => x.Origin).NotEmpty();
            RuleFor(x => x.Destination).NotEmpty();
            RuleFor(x => x.OriginDateTime).NotNull();
        }
    }
}