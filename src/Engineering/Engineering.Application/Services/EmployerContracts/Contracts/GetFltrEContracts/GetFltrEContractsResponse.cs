using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;

public record GetFltrEContractsResponse(
    List<GetFltrEContractsModel> Data,
    int RowCount
    );

public class GetFltrEContractsModel
{
    public long Id { get; set; }
    public string? FullCode => $"{HeadCode}-{Code}";
    public long HeadId { get; set; }
    public string? HeadCode { get; set; }
    public long EmployerId { get; set; }
    public string? Employer { get; set; } = string.Empty;
    public long CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public bool? IsPrimaryManagerConfirmed { get; set; }
    public bool? IsFinalManagerConfirmed { get; set; }
    public bool IsFirst { get; set; }
    public EContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public EContractType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public string? Code { get; set; }
    public decimal? CurrencyRate { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => EndDate.ToShamsi();
    public decimal TotalAmount { get; set; }
    public decimal AdvancePayment { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
};
