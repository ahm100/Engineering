using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;

public class GetContractRegistrationGridModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public long? ContractNumber { get; set; }
    public string FaTitle { get; set; } = string.Empty;
    public long ContractPartyId { get; set; }
    public string? ContractPartyName { get; set; }
    public decimal? InitialAmount { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyTitle { get; set; }
    public string? CurrencyIso { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool HasAttachment { get; set; }
    public ContractStatus Status { get; set; }
    public bool IsRegistrationPending { get; set; }
    public string StatusTitle => Status.GetEnumDescription();

    [System.Text.Json.Serialization.JsonIgnore]
    public bool RequiresCalculatedInitialAmount { get; set; }
}
