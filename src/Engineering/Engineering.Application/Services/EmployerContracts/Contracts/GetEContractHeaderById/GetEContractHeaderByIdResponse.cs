using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;

public class GetEContractHeaderByIdResponse
{
    public long Id { get; set; }
    public long EmployerId { get; set; }
    public string Employer { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
    public string? Code { get; set; }
    public decimal? VolumeTolerance { get; set; }
    public decimal? PriceTolerance { get; set; }
    public long CurrencyId { get; set; }
    public string? Currency { get; set; }
    public EContractType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required List<GetEContractByIdResponse> EContracts { get; set; }
};
