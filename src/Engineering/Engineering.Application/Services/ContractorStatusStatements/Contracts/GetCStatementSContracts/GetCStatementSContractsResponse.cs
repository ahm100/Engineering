
namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;

public record GetCStatementSContractsResponse(
    List<GetCStatementSContractsModel> Data
    );

public record GetCStatementSContractsModel
{
    public long Id { get; set; }
    public long ContractId { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public string? ContractorContractHeaderDescription { get; set; } = string.Empty;
    public string? ContractorContractType { get; set; } = string.Empty;
    public string? ContractorContractTypeCode { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public DateTime StartDateMiladi { get; set; }
    public string StartDate => StartDateMiladi.ToShamsi();
    public DateTime EndDateMiladi { get; set; }
    public string EndDate => EndDateMiladi.ToShamsi();
    public decimal? TotalAmount { get; set; } = 0;
    public decimal? TotalWorkedAmount => DailyServices!.Count == 0 ? 0 : DailyServices.Sum(x => x.TotalPrice);
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public string? Description { get; set; }
    public List<GetCStatementSContractsDailiesModel>? DailyServices { get; set; } = new();
}

public record GetCStatementSContractsDailiesModel
{
    public long Id { get; set; }
    public long DailyServiceId { get; set; }
    public long DailyId { get; set; }
    public decimal Volume { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AcceptablePercentage { get; set; }
    public decimal AcceptableAmount { get; set; }
    public string? AcceptableDescription { get; set; }
    public decimal ProjectManagementApprovalPercentage { get; set; }
    public decimal ProjectManagementApprovedPrice { get; set; }
    public string? ProjectManagementApprovedDescription { get; set; }
    public decimal ManagementApprovalPercentage { get; set; }
    public decimal ApprovedPrice { get; set; }
    public string? ApprovedDescription { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public DateTime? Created { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;

    public string? ServiceInfoUnitOfMeasurement => ServiceInfoMeasurement;
    public string? ProjectOperationUnitOfMeasurement => ProjectOperationMeasurement;

    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public decimal? Workload { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long? ProjectOperationMeasureId { get; set; }
    public string? ProjectOperationMeasurement { get; set; } = string.Empty;

    public long ProjectOperationDetailContractorServiceId { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; }
    public string? ServiceInfoCode { get; set; }
    public long? ServiceInfoMeasureId { get; set; }
    public string? ServiceInfoMeasurement { get; set; } = string.Empty;
    public decimal ServiceVolume { get; set; }

    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Number { get; set; }
    public decimal? FinalAmount => Length * Width * Height * Weight * Number;
    public string? PublicCode { get; set; }
    public string? PublicName { get; set; }
    public string? Description { get; set; }
}
