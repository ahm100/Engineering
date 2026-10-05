

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationByLegacyId;

public record GetDailyProjectOperationByLegacyIdRequest(
    long LegacyId
    ) : IHttpRequest;
