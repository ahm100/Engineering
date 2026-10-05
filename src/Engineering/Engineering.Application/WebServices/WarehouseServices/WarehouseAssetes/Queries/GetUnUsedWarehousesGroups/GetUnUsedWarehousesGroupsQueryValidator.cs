
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesGroups;

public class GetUnUsedWarehousesGroupsQueryValidator : AbstractValidator<GetUnUsedWarehousesGroupsQuery>
{
    public GetUnUsedWarehousesGroupsQueryValidator()
    {
        RuleForEach(c => c.WarehouseIds)
            .IsPositive(GlobalCmts.Id);
    }
}
