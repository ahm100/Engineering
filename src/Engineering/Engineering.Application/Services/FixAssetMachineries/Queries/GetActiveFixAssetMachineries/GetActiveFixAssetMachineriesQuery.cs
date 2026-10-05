using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetActiveFixAssetMachineries;

public record GetActiveFixAssetMachineriesQuery(
   List<long>? MachineryIds,
   FixAssetMachineryType? Type,
   string? NumberPlates,
   string? FilterData,
   long? CompanyId,
   int PageIndex,
   int PageSize
    ) : IQuery<DataResult<List<FixAssetMachinery>>>;