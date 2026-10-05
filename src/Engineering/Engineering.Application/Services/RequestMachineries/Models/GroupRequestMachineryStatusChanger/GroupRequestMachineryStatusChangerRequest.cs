using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GroupRequestMachineryStatusChanger;

public record GroupRequestMachineryStatusChangerRequest(
    List<long> Ids,
    RequestMachineryStatus Status,
    string? Description
     ) : IHttpRequest;
