namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
public record GetEOProductHistoryResponse(
    List<GetEOProductHistoryModel> Data,
    int RowCount);

public class GetEOProductHistoryModel
{
    public long Id { get; set; } = 0;
    public long EOProductId { get; set; } = 0;
    public long ProductGroupId { get; set; }
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public bool IsStandard { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
};