using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
public record GetEContractHistoryResponse(
    List<GetEContractHistoryModel> Data,
    int RowCount);

public class GetEContractHistoryModel
{
    public long Id { get; set; }
    public string? FullCode => $"{HeadCode}-{Code}";
    public bool IsFirst { get; set; }
    public EContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public string? Code { get; set; }
    public string? HeadCode { get; set; }
    public decimal? CurrencyRate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AdvancePayment { get; set; }
    public string? Description { get; set; }
};