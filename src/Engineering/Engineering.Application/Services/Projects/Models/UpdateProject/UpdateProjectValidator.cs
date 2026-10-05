
namespace Engineering.Application.Services.Projects.Models.UpdateProject;

using Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;

public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
        RuleFor(oo => oo.ProjectName)
            .NotEmpty().WithError(ProjectErrors.ProjectNameIsEmpty);
        RuleFor(oo => oo.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
        RuleFor(oo => oo.IsActive)
            .NotNull().WithError(ProjectErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.ProjectName)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.IsOrganizationUnit)
            .IsRequiredBool(ProjectCmts.IsOrganizationUnit);
        RuleFor(oo => oo.ProjectWarehouses)
            .HasNoDuplicates(oo => oo?.Id, GlobalCmts.WarehouseId);
        RuleFor(oo => oo.ProjectWarehouses)
            .NotEmpty().WithError(ProjectWarehouseErrors.NoDefault)
            .When(oo => oo.ProjectWarehouses is not null);
        RuleFor(oo => oo.ProjectWarehouses)
            .Must(oo => oo?.Count(xx => xx?.IsDefault == true) <= 1)
            .WithError(ProjectWarehouseErrors.MoreThanOneDefault)
            .When(oo => oo.ProjectWarehouses is not null);
        RuleFor(oo => oo.ProjectWarehouses)
            .Must(oo => oo?.Any(xx => xx?.IsDefault == true) == true)
            .WithError(ProjectWarehouseErrors.NoDefault)
            .When(oo => oo.ProjectWarehouses is not null);
        RuleForEach(oo => oo.ProjectWarehouses)
            .NotEmpty().WithError(GlobalErrors.RequiredNull(GlobalCmts.WarehouseId))
            .SetValidator(new ProjectWarehouseRequestValidator());
    }
}
