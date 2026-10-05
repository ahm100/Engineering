using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;

namespace Engineering.Application.Services.Advertisements.Queries.GetAdvertisementById;

public record GetAdvertisementByIdQuery(
    long Id) : IQuery<GetAdvertisementByIdResponse?>;