namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseById;

public class GetProjectWarehouseByIdValidator : AbstractValidator<GetProjectWarehouseByIdRequest>
{
    public GetProjectWarehouseByIdValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
