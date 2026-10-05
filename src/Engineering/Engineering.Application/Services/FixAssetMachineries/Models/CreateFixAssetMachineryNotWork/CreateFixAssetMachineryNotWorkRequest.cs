namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryNotWork;

public record CreateFixAssetMachineryNotWorkRequest(
    long FixAssetMachineryId,
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    string? Description,
    List<string>? Documents
     ) : IHttpRequest;
