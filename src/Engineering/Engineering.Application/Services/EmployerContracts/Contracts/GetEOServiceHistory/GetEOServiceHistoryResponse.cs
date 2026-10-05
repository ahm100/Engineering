namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
public record GetEOServiceHistoryResponse(
    List<GetEOServiceHistoryModel> Data,
    int RowCount);

public class GetEOServiceHistoryModel
{
    public long Id { get; set; } = 0;
    public long EOServiceId { get; set; } = 0;
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
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
};