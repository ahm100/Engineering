
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;

public record GetContractorPriceHistoryResponse(
    List<GetContractorPriceHistoryModel> Data,
    int RowCount
    );


public record GetContractorPriceHistoryModel
{
    public long Id { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => EndDate.ToShamsi();
    public decimal Price { get; set; }
    public long CurrencyId { get; set; }
    public string? Currency { get; set; }
    public bool IsActive { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => Created.ToShamsi();
}
