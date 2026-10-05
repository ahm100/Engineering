namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;

public class GetProjectWarehousesByProjectIdsValidator : AbstractValidator<GetProjectWarehousesByProjectIdsRequest>
{
    public GetProjectWarehousesByProjectIdsValidator()
    {
        RuleFor(oo => oo.ProjectIds).NotEmpty();
        RuleFor(oo => oo.ProjectIds).HasNoDuplicates(oo => oo, GlobalCmts.ProjectId);
        RuleForEach(oo => oo.ProjectIds).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize).PageSizeZero(GlobalCmts.PageSize);
    }
}
