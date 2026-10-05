
namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Models;

public record CostGroup
{
    public long Id { get; set; }
    public string CostGroupCode { get; set; } = string.Empty;
    public string CostGroupTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
