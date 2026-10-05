using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;

public class PriceIndexContractAdjustmentConfigurationResponse
{
    public int BaseYear { get; set; }
    public ContractAdjustmentPeriod BasePeriod { get; set; }
    public long ReferenceId { get; set; }
    public long IndexId { get; set; }
}
