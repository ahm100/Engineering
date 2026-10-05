using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Models.GroupRequestContractorStatusChanger;

public record GroupRequestContractorStatusChangerRequest(
    List<long> Ids,
    RequestContractorStatus Status,
    string? Description
     ) : IHttpRequest;
