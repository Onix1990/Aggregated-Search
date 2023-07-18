using AggregatedSearch.BusinessLogic.Models.ProviderOneSearch;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;
using AutoMapper;

namespace AggregatedSearch.BusinessLogic.Mapping
{
    /// <summary>
    /// Профиль маппинга первого провайдера поиска
    /// </summary>
    public class ProviderOneProfile : Profile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProviderOneProfile()
        {
            CreateMap<SearchRequest, ProviderOneSearchRequest>()
                .ForMember(x => x.From, opt => opt.MapFrom(m => m.Origin))
                .ForMember(x => x.To, opt => opt.MapFrom(m => m.Destination))
                .ForMember(x => x.DateFrom, opt => opt.MapFrom(m => m.OriginDateTime))
                .ForMember(x => x.DateTo, opt => opt.MapFrom(m =>
                    m.Filters != null
                        ? m.Filters.DestinationDateTime
                        : null
                ))
                .ForMember(x => x.MaxPrice, opt => opt.MapFrom(
                    m => m.Filters != null
                        ? m.Filters.MaxPrice
                        : null
                ));
            CreateMap<ProviderOneRoute, Route>()
                .ForMember(x => x.Origin, opt => opt.MapFrom(m => m.From))
                .ForMember(x => x.Destination, opt => opt.MapFrom(m => m.To))
                .ForMember(x => x.OriginDateTime, opt => opt.MapFrom(m => m.DateFrom))
                .ForMember(x => x.DestinationDateTime, opt => opt.MapFrom(m => m.DateTo))
                .ForMember(x => x.Price, opt => opt.MapFrom(m => m.Price))
                .ForMember(x => x.TimeLimit, opt => opt.MapFrom(m => m.TimeLimit));
        }
    }
}