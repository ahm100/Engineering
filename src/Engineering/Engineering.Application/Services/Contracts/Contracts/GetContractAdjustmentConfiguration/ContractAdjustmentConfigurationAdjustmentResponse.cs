using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;

public class ContractAdjustmentConfigurationAdjustmentResponse
{
    public ContractTypeDetailAdjustmentType Type { get; set; }
    public PriceIndexContractAdjustmentConfigurationResponse? PriceIndex { get; set; }
    public CurrencyContractAdjustmentConfigurationResponse? Currency { get; set; }
    public OtherContractAdjustmentConfigurationResponse? Other { get; set; }
}
