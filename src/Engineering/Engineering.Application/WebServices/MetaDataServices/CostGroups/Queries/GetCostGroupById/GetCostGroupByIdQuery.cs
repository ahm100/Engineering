using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetCostGroupById;

public record GetCostGroupByIdQuery(
    long Id
    ) : IQuery<CostGroupModel?>;
