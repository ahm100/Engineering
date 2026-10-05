namespace Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;

public class ProjectWarehouseRequestValidator : AbstractValidator<ProjectWarehouseRequest>
{
    public ProjectWarehouseRequestValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.WarehouseId);
        RuleFor(oo => oo.IsDefault).IsRequiredBool(ProjectWarehouseCmts.IsDefault);
    }
}
