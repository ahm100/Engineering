using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;

public record GetFixAssetMachineryByIdQuery(
    long Id
    ) : IQuery<FixAssetMachinery?>;