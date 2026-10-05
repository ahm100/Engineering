using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.CreateEContractHeader;

public class CreateEContractHeaderRequest : IHttpRequest
{
    public long CostCenterId { get; set; }
    public long EmployerId { get; set; }
    public long CurrencyId { get; set; }
    public string? Code { get; set; }
    public decimal? VolumeTolerance { get; set; }
    public EContractType Type { get; set; }
    public string? Description { get; set; }
    public required List<CreateEContractModel> CreateContracts { get; set; }
};
