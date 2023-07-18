using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace AggregatedSearch.BusinessLogic.Utils
{
    /// <summary>
    /// Методы расширения для работы с валидацией
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Преобразовать список ValidationFailure в Dictionary
        /// </summary>
        /// <param name="validationFailures"></param>
        /// <returns></returns>
        public static IDictionary<string, string[]> FormatValidationErrors(
            this IEnumerable<ValidationFailure> validationFailures
        )
        {
            return validationFailures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, group => group.Select(x => x.ErrorMessage).ToArray());
        }
    }
}