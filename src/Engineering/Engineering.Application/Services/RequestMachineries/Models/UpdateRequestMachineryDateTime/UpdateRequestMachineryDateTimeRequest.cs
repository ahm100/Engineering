namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDateTime;

public record UpdateRequestMachineryDateTimeRequest(
    long RequestMachineryId,
    string? TimeRequired,
    DateTime FromDate,
    DateTime ToDate,
    TimeSpan FromTime,
    TimeSpan ToTime) : IHttpRequest;
