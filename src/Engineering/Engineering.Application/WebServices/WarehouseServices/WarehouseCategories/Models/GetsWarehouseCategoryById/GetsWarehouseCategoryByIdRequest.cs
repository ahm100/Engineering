
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetsWarehouseCategoryById;

public class GetsWarehouseCategoryByIdRequest
{
    public List<long> Ids { get; set; } = new();
    public bool? IgnoreQuery { get; set; }
    public string? FilterData { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
