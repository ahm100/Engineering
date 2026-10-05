namespace Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;

public record GetAdvertisementByIdsRequest(
    List<long> Ids,
    int PageIndex,
    int PageSize) : IQuery<GetAdvertisementByIdsResponse?>;