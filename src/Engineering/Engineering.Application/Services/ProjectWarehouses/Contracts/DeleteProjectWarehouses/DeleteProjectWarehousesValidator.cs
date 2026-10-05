namespace Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouses;

public class DeleteProjectWarehousesValidator : AbstractValidator<DeleteProjectWarehousesRequest>
{
    public DeleteProjectWarehousesValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty();
        RuleFor(oo => oo.Ids).HasNoDuplicates(oo => oo, GlobalCmts.Id);
        RuleForEach(oo => oo.Ids).IsPositive(GlobalCmts.Id);
    }
}
