using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractHeader;

public class UpdateEContractHeaderRequest : IHttpRequest
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public decimal? VolumeTolerance { get; set; }
    public EContractType Type { get; set; }
    public string? Description { get; set; }
    public List<CreateEContractModel>? CreateContracts { get; set; }
    public List<UpdateEContractRequest>? UpdateContracts { get; set; }
    public List<long>? DeleteContracts { get; set; }
};