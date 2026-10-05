using Engineering.Application.Services.Contracts.Contracts.ContractRegistration;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;

public class GetContractRegistrationByIdResponse
{
    public long Id { get; set; }
    public long? ContractNumber { get; set; }
    public string FaTitle { get; set; } = string.Empty;
    public string? EnTitle { get; set; }
    public string? Description { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long ContractPartyId { get; set; }
    public string? ContractPartyName { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public ContractDurationUnit DurationUnit { get; set; }
    public string DurationUnitTitle => DurationUnit.GetEnumDescription();
    public DateTime EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public bool IsRegistrationPending { get; set; }
    public string StatusTitle => Status.GetEnumDescription();
    public string? ContractTypeCode { get; set; }
    public PricingMethod? PricingMethod { get; set; }
    public string? PricingMethodTitle => PricingMethod?.GetEnumDescription();
    public List<string> Urls { get; set; } = [];
    public List<GetContractRegistrationStatusHistoryResponse> StatusHistories { get; set; } = [];
    public ContractRegistrationFinancialResponse? Financial { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public bool RequiresCalculatedInitialAmount { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public decimal ActiveFinancialChangeAmount { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public bool IsRegistrationStructureCompatible { get; set; }
}

public class GetContractRegistrationStatusHistoryResponse
{
    public long Id { get; set; }
    public ContractStatus FromStatus { get; set; }
    public ContractStatus ToStatus { get; set; }
    public ContractStatusTransitionType Operation { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string? Reason { get; set; }
    public string? Description { get; set; }
    public int? SuspensionDurationMonths { get; set; }
    public DateTime Created { get; set; }
    public long? CreatorId { get; set; }
    public List<string> Urls { get; set; } = [];
}
