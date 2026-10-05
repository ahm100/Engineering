namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryByCodes;

public record GetsCostCategoryByCodesRequest(
    List<string>? Codes
    );
