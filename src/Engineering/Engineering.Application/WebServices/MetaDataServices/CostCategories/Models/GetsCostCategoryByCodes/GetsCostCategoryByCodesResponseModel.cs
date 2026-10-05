using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryByCodes;

public class GetsCostCategoryByCodesResponseModel
{
    [JsonProperty("data")]
    public List<CostCategoryModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
