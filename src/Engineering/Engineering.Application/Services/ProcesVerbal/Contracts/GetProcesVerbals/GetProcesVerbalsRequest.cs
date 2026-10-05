using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;

public record GetProcesVerbalsRequest(
    long? TargetId,
    long? ProjectId,
    long? ContractId,
    ProcesVerbalType? Type,
    string? Title,
    int PageIndex,
    int PageSize) : IHttpRequest;