using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbals;

public record GetProcesVerbalsQuery(
    long? TargetId,
    long? ProjectId,
    long? ContractId,
    ProcesVerbalType? Type,
    string? Title,
    int PageIndex,
    int PageSize) : IQuery<GetProcesVerbalsResponse?>;