using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;

public record CreateProcesVerbalRequest(
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
    ) : IHttpRequest;

public record ProcesVerbalPODModel(
    long PODId,
    decimal NewFinalAmount);

public record ProcesVerbalProductModel(
    long ProductId,
    decimal NewFinalAmount,
    ProcesVerbalProductItemStatus Status,
    string Description);