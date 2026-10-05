namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectId;

public class GetProjectWarehousesByProjectIdValidator : AbstractValidator<GetProjectWarehousesByProjectIdRequest>
{
    public GetProjectWarehousesByProjectIdValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize).PageSizeZero(GlobalCmts.PageSize);
    }
}
