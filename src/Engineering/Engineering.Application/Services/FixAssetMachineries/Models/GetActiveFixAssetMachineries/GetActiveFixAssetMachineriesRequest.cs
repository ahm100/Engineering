using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetActiveFixAssetMachineries;

public record GetActiveFixAssetMachineriesRequest(
   List<long>? MachineryIds,
   FixAssetMachineryType? Type,
   string? NumberPlates,
   string? FilterData,
   int PageIndex,
   int PageSize
     ) : IHttpRequest;
