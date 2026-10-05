using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;

public class GetContractGuaranteesModel
{
    public long Id { get; set; }
    public ContractGuaranteeType Type { get; set; }
    public string TypeTitle => Type.GetEnumDescription();
    public decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public ContractGuaranteeStatus Status { get; set; }
    public string StatusTitle => Status.GetEnumDescription();
    public string? FileUrl { get; set; }
}
