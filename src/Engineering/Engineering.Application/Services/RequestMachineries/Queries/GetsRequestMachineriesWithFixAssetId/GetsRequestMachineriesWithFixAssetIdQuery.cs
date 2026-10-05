using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetsRequestMachineriesWithFixAssetId;

public record GetsRequestMachineriesWithFixAssetIdQuery(
    long? FixAssetId) : IQuery<List<RequestMachinery>>;
