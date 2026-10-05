using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;

namespace Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;

public record GetAdvertisementByIdsQuery(
    List<long> Ids,
    int PageIndex,
    int PageSize) : IQuery<GetAdvertisementByIdsResponse?>;