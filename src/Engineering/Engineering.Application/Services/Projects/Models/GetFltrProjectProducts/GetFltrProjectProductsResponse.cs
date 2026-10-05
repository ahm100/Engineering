namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;

public record GetFltrProjectProductsResponse(
    List<GetFltrEContractsModel> Data,
    int RowCount
    );

public class GetFltrProjectProductsModel
{
    public long Id { get; set; }
    public decimal RequestQuantity { get; private set; } = 0;
    public decimal RemainingQuantity { get; private set; } = 0;
    public decimal CompletedQuantity { get; private set; } = 0;
    public decimal InProgressQuantity { get; private set; } = 0;
    public long ProductGroupId { get; private set; }
    public long ProductGroupName { get; private set; }
    public decimal TolerancePercentage { get; private set; } = 0;
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
};
