namespace Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryNotWork;

public record UpdateFixAssetMachineryNotWorkRequest(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    string? Description,
    List<string>? Documents
     ) : IHttpRequest;
