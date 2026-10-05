namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;

public record GetFilteredGoodsManagerAssignmentsResponse(
    List<GetFilteredOrganizationModel> Data,
    int RowCount);

public class GetFilteredOrganizationModel : IUserAuditable
{
    public long OrganizationId { get; set; }
    public string? OrganizationFa { get; set; }
    public string? OrganizationEn { get; set; }
    public string? Description { get; set; }
    public long? ManagerId { get; set; }
    public string Manager { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public DateTime? Created { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;

    public required List<GetFilteredProductModel> Products { get; set; }

    public List<long> RemoveProductIds => Products.Listed(x => x.ProductId);
};

public class GetFilteredProductModel
{
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
};


public class GetFilteredGoodsManagerAssignmentModel
{
    public long OrganizationId { get; set; }
    public string? Organization { get; set; }
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
};
