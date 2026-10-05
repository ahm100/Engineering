using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.MetaDataServices.Models.GetCostCategoryById;

public class GetCostCategoryByIdResponse
{
    [JsonProperty("value")]
    public CostCategoryModel? Value { get; set; }
}
