using Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Commands.CreateProcesVerbal;

public record CreateProcesVerbalCommand(
    long ProjectId,
    long ContractId,
    string TitleFa,
    string? TitleEn,
    ProcesVerbalType Type,
    DateTime RecordDateTime,
    string? Location,
    ProcesVerbalDeliveryStatus? DeliveryStatus,
    string? Limitations,
    //ProcesVerbalProductStatus? EquippingStatus, system must count by product statusses
    ProcesVerbalWorkStatus? WorkStatus,
    ProcesVerbalLimitationStatus? LimitationStatus,
    ProcesVerbalWorkStartStatus? WorkStartStatus,
    ProcesVerbalWorkStopReason? WorkStopReason,
    ProcesVerbalWorkStopStatus? WorkStopStatus,
    List<string>? Items,
    List<string>? Docs,
    List<ProcesVerbalPODModel>? PODs,
    List<ProcesVerbalProductModel>? Products
) : ICommand<CreateProcesVerbalResponse?>;