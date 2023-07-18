using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;
using AggregatedSearch.BusinessLogic.Services;
using AggregatedSearch.BusinessLogic.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AggregatedSearch.Api.Controllers
{
    /// <summary>
    /// Контроллер работы с маршрутами
    /// </summary>
    [ApiController]
    [Route("[controller]s")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class RouteController : ControllerBase
    {
        /// <summary>
        /// Сервис агрегированного поиска
        /// </summary>
        private readonly ISearchService _searchService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="searchService">Сервсис агрегированного поиска</param>
        public RouteController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Получить маршруты
        /// </summary>
        /// <param name="searchRequest">Данные для фильтра</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<SearchResponse>> GetAllByAsync(
            [FromQuery] SearchRequest searchRequest,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var response = await _searchService.SearchAsync(
                    request: searchRequest,
                    cancellationToken: cancellationToken
                );
                return Ok(response);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationProblemDetails(e.Errors.FormatValidationErrors()));
            }
        }

        /// <summary>
        /// Получить маршрут по Guid (из кэша)
        /// </summary>
        /// <param name="id">Идентификатор маршурта</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Route> GetBy([FromRoute] Guid id)
        {
            try
            {
                var response = _searchService.GetBy(id);
                return Ok(response);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }
    }
}