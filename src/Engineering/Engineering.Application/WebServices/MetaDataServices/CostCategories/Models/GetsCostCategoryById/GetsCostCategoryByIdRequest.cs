namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryById;

public record GetsCostCategoryByIdRequest(
    List<long>? Ids,
    long? GroupId,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    );
