using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;

public class GetProcesVerbalDetailByIdResponse
{
    public long Id { get; set; }
    public string TitleFa { get; set; } = string.Empty;
    public string? TitleEn { get; set; }
    public ProcesVerbalType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public DateTime RecordDateTime { get; set; }
    public string? Location { get; set; }
    public long? ContractId { get; set; }
    public long? ContractNum { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }

    public ProcesVerbalDeliveryStatus? DeliveryStatus { get; set; }
    public string? DeliveryStatusDescription => DeliveryStatus?.GetEnumDescription();

    public string? Limitations { get; set; }

    public ProcesVerbalProductStatus? ProductStatus { get; set; }
    public string? ProductStatusDescription => ProductStatus?.GetEnumDescription();

    public ProcesVerbalWorkStatus? WorkStatus { get; set; }
    public string? WorkStatusDescription => WorkStatus?.GetEnumDescription();

    public ProcesVerbalLimitationStatus? LimitationStatus { get; set; }
    public string? LimitationStatusDescription => LimitationStatus?.GetEnumDescription();

    public ProcesVerbalWorkStartStatus? WorkStartStatus { get; set; }
    public string? WorkStartStatusDescription => WorkStartStatus?.GetEnumDescription();

    public ProcesVerbalWorkStopReason? WorkStopReason { get; set; }
    public string? WorkStopReasonDescription => WorkStopReason?.GetEnumDescription();

    public ProcesVerbalWorkStopStatus? WorkStopStatus { get; set; }
    public string? WorkStopStatusDescription => WorkStopStatus?.GetEnumDescription();

    public List<ProcesVerbalDocDetailModel>? Docs { get; set; }
    public List<ProcesVerbalItemDetailModel>? Items { get; set; }
    public List<ProcesVerbalPODDetailModel>? PODs { get; set; }
    public List<ProcesVerbalProductDetailModel>? Products { get; set; }
}

public record ProcesVerbalDocDetailModel(
    long Id,
    string URL);

public record ProcesVerbalItemDetailModel(
    long Id,
    string TitleFa);

public record ProcesVerbalPODDetailModel(
    long Id,
    string PODDescription,
    decimal FinalAmount,
    decimal NewFinalAmount);

public record ProcesVerbalProductDetailModel(
    long Id,
    string? Title,
    decimal FinalValue,
    decimal NewFinalValue,
    decimal ValueDiffr,
    string? Description,
    ProcesVerbalProductItemStatus Status,
    string StatusDescription);