using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;

public class CurrencyContractAdjustmentConfigurationResponse
{
    public DateTime BaseDate { get; set; }
    public decimal BaseRate { get; set; }
    public long CurrencyId { get; set; }
    public ContractAdjustmentCurrencyReferenceType ReferenceType { get; set; }
    public string? CustomReference { get; set; }
}
