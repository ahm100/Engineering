
namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Models;

public record CostCategory
{
    public long Id { get; set; }
    public string CostCategoryCode { get; set; } = string.Empty;
    public string CostCategoryTitle { get; set; } = string.Empty;
    public long CostGroupId { get; set; }
    public string CostGroupCode { get; set; } = string.Empty;
    public string CostGroupTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
