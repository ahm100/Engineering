using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;

namespace Engineering.Application.Services.Advertisements.Queries.GetFltrAdvertisement;

public record GetFltrAdvertisementQuery(string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize) : IQuery<GetFltrAdvertisementResponse?>;