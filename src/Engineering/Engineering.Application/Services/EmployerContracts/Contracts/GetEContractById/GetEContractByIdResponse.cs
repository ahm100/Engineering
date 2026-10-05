using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;

public class GetEContractByIdResponse
{
    public long Id { get; set; }
    public long HeadId { get; set; }
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public string? FullCode => $"{HeadCode}-{Code}";
    public bool IsFirst { get; set; }
    public bool IsCovered =>
    (AdvancePayment != 0) ||
    (TotalAmount != 0) ||
    (CurrencyRate ?? 0) != 0;
    public EContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public string? Code { get; set; }
    public string? HeadCode { get; set; }
    public decimal? CurrencyRate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AdvancePayment { get; set; }
    public long CurrencyId { get; set; }
    public string? Currency { get; set; }
    public decimal? VolumeTolerance { get; set; }
    public decimal? PriceTolerance { get; set; }
    public string? Description { get; set; }
    public List<GetEDocModel>? EmployerDocs { get; set; }
    public List<GetEConsiderationModel>? EmployerConsiderations { get; set; }
    public List<GetECostOverModel>? EmployerCostOvers { get; set; }
    public List<GetEOperationModel>? EmployerOperations { get; set; }
};

public class GetEDocModel
{
    public long Id { get; set; }
    public EDocumentType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public DateTime RegistrationDate { get; set; }
    public decimal? Version { get; set; }
    public string? Description { get; set; }
    public required List<string> urls { get; set; }
};

public class GetEConsiderationModel
{
    public long Id { get; set; }
    public ConsiderationType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public string FlagId => Id.ToString();
    public string? Description { get; set; }
};

public class GetECostOverModel
{
    public long Id { get; set; }
    public long CostOverId { get; set; }
    public string CostOverName { get; set; } = string.Empty;
    public string CostOverCode { get; set; } = string.Empty;
    public decimal Percent { get; set; }
    public List<GetECostOverImpactsModel>? Impacts { get; set; }
};

public class GetECostOverImpactsModel
{
    public long Id { get; set; }
    public long ChildCostOverId { get; set; }
    public long CostOverId { get; set; }
    public string CostOverName { get; set; } = string.Empty;
    public string CostOverCode { get; set; } = string.Empty;
    public decimal Percent { get; set; }
};

public class GetEOperationModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal Workload { get; set; }
    public decimal TolerancePercentage { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal IncreaseRate { get; set; }
    public decimal? Priority { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public List<string>? FlagIds { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public ProjectOperationStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public bool GoodsInProgress { get; set; }
    public string? ProjectOperationDescription { get; set; }
    public string? Description { get; set; }
    public List<GetEOperationDetailModel>? Details { get; set; }
    public List<GetEOperationHistoryModel>? Histories { get; set; }
    public List<GetEOperationProductModel>? Products { get; set; }
    public List<GetEOperationServiceModel>? Services { get; set; }
};

public class GetEOperationDetailModel
{
    public long Id { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public string PrivateName { get; set; } = string.Empty;
    public string PrivateCode { get; set; } = string.Empty;
    public string PublicName { get; set; } = string.Empty;
    public string PublicCode { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount => Length * Width * Height * Weight * Number;
    public ProjectOperationDetailStatus? Status { get; set; }
    public string? StatusDescription => Status!.GetEnumDescription();
    public string? Description { get; set; }
};

public class GetEOperationHistoryModel
{
    public long Id { get; set; }
    public decimal? Price { get; set; }
    public decimal Workload { get; set; }
    public string? Description { get; set; } = string.Empty;
};

public class GetEOperationProductModel
{
    public long Id { get; set; }
    public long ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int? Count { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public bool IsStandard { get; set; }
    public string? Description { get; set; }
};
public class GetEOperationServiceModel
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public string? Description { get; set; }
};
