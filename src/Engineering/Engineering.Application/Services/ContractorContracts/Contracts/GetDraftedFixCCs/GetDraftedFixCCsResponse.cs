
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;

public record GetDraftedFixCCsResponse(
    List<GetDraftedFixCCsModel> Data
    );

public record GetDraftedFixCCsModel
{
    public long Id { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public string? ContractorContractHeaderDescription { get; set; } = string.Empty;
    public string? ContractorContractType { get; set; } = string.Empty;
    public string? ContractorContractTypeCode { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public long ContractorId { get; set; }
    public DateTime? StartDateMiladi { get; set; }
    public string StartDate => StartDateMiladi == null ? "" : StartDateMiladi.ToShamsi()!;
    public DateTime? EndDateMiladi { get; set; }
    public string EndDate => EndDateMiladi == null ? "" : EndDateMiladi.ToShamsi()!;
    public decimal? TotalAmount { get; set; } = 0;
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public string? Description { get; set; }
    public List<long> PODContractorServiceIds { get; set; } = new();
    public List<GetDraftedFixDailiesModel>? DailyServices { get; set; } = new();
}

public record GetDraftedFixDailiesModel
{
    public long Id { get; set; }
    public long DailyId { get; set; }

    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long? ProjectOperationMeasureId { get; set; }
    public decimal? ProjectOperationWorkload { get; set; }
    public string? ProjectOperationMeasurement { get; set; } = string.Empty;

    public long ProjectOperationDetailContractorServiceId { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; }
    public string? ServiceInfoCode { get; set; }
    public long? ServiceInfoMeasureId { get; set; }
    public string? ServiceInfoMeasurement { get; set; } = string.Empty;

    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Number { get; set; }
    public decimal? FinalAmount => Length * Width * Height * Weight * Number;
    public string? PublicCode { get; set; }
    public string? PublicName { get; set; }
    public string? Description { get; set; }

    public decimal Volume { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public DateTime? CreatedMiladi { get; set; }
    public string? Created => CreatedMiladi.ToShamsi();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}
