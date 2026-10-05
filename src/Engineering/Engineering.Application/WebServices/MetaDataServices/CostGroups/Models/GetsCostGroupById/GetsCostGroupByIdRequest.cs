namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetsCostGroupById;

public record GetsCostGroupByIdRequest(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    );
