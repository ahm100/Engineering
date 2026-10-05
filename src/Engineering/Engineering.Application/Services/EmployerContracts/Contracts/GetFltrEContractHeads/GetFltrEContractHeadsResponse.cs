using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;

public record GetFltrEContractHeadsResponse(
    List<GetFltrEContractHeadsModel> Data,
    int RowCount
    );

public class GetFltrEContractHeadsModel
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long EmployerId { get; set; }
    public string? Employer { get; set; } = string.Empty;
    public long CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
    public EContractType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => StartDate.ToShamsi();
    public decimal? VolumeTolerance { get; set; }
    public decimal? PriceTolerance { get; set; }
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
};
