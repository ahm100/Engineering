using Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;
using Engineering.Domain.Entities.Contracts.Enums;
namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;
public class GetContractAdjustmentConfigurationResponse
{
    public long ContractId { get; set; }
    public ContractAdjustmentMethod Method { get; set; }
    public List<ContractAdjustmentConfigurationModel> Configurations { get; set; } = [];
}

public class ContractAdjustmentConfigurationModel
{
    public long Id { get; set; }
    public ContractAdjustmentScopeResponse Scope { get; set; } = new();
    public ContractAdjustmentConfigurationAdjustmentResponse Adjustment { get; set; } = null!;
}
