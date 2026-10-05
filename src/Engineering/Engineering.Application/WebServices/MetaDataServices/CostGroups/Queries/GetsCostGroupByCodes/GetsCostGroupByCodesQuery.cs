using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetsCostGroupByCodes;

public record GetsCostGroupByCodesQuery(
    List<string>? Codes
    ) : IQuery<DataResult<List<CostGroupModel>>>;
