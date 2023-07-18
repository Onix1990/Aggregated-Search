using AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;
using AutoMapper;

namespace AggregatedSearch.BusinessLogic.Mapping
{
    /// <summary>
    /// Профиль маппинга второго провайдера поиска
    /// </summary>
    public class ProviderTwoProfile : Profile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProviderTwoProfile()
        {
            CreateMap<SearchRequest, ProviderTwoSearchRequest>()
                .ForMember(x => x.Departure, opt => opt.MapFrom(m => m.Origin))
                .ForMember(x => x.Arrival, opt => opt.MapFrom(m => m.Destination))
                .ForMember(x => x.DepartureDate, opt => opt.MapFrom(m => m.OriginDateTime))
                .ForMember(x => x.MinTimeLimit, opt => opt.MapFrom(m =>
                    m.Filters != null
                        ? m.Filters.MinTimeLimit
                        : null
                ));
            CreateMap<ProviderTwoRoute, Route>()
                .ForMember(x => x.Origin, opt => opt.MapFrom(m => m.Departure.Point))
                .ForMember(x => x.Destination, opt => opt.MapFrom(m => m.Arrival.Point))
                .ForMember(x => x.OriginDateTime, opt => opt.MapFrom(m => m.Departure.Date))
                .ForMember(x => x.DestinationDateTime, opt => opt.MapFrom(m => m.Arrival.Date))
                .ForMember(x => x.Price, opt => opt.MapFrom(m => m.Price))
                .ForMember(x => x.TimeLimit, opt => opt.MapFrom(m => m.TimeLimit));
        }
    }
}