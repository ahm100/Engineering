
namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;

public record GetCStatementFContractsResponse(
    List<GetCStatementFContractsModel> Data
    );

public record GetCStatementFContractsModel
{
    public long Id { get; set; }
    public long ContractId { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public string? ContractorContractHeaderDescription { get; set; } = "";
    public string? ContractorContractType { get; set; } = "";
    public string? ContractorContractTypeCode { get; set; } = "";
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = "";
    public long ContractorId { get; set; }
    public string? Contractor { get; set; } = "";
    public DateTime StartDateMiladi { get; set; }
    public string StartDate => StartDateMiladi.ToShamsi();
    public DateTime EndDateMiladi { get; set; }
    public string EndDate => EndDateMiladi.ToShamsi();
    public decimal? TotalAmount { get; set; } = 0;
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public decimal? FixedContractPct { get; set; } = 0;
    public decimal? FixedContractPctAmount { get; set; } = 0;
    public string? FixedContractPctDesc { get; set; } = "";
    public decimal? ProjectFixedContractPct { get; set; } = 0;
    public decimal? ProjectFixedContractPctAmount { get; set; } = 0;
    public string? ProjectFixedContractPctDesc { get; set; } = "";
    public decimal? ManagerFixedContractPct { get; set; } = 0;
    public decimal? ManagerFixedContractPctAmount { get; set; } = 0;
    public string? ManagerFixedContractPctDesc { get; set; } = "";
    public string? Description { get; set; } = "";
    public List<GetCStatementFContractsDailiesModel>? DailyServices { get; set; } = new();
}

public record GetCStatementFContractsDailiesModel
{
    public long Id { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public long DailyServiceId { get; set; }
    public long DailyId { get; set; }
    public decimal Volume { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public DateTime? Created { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = "";

    public string? ServiceInfoUnitOfMeasurement => ServiceInfoMeasurement;
    public string? ProjectOperationUnitOfMeasurement => ProjectOperationMeasurement;

    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public decimal? Workload { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long? ProjectOperationMeasureId { get; set; }
    public string? ProjectOperationMeasurement { get; set; } = "";

    public long ProjectOperationDetailContractorServiceId { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; }
    public string? ServiceInfoCode { get; set; }
    public long? ServiceInfoMeasureId { get; set; }
    public string? ServiceInfoMeasurement { get; set; } = "";
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
