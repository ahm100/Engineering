namespace Engineering.Application.Services.ProjectTypes.Models.DisableProjectType;

public class DisableProjectTypeValidator : AbstractValidator<DisableProjectTypeRequest>
{
    public DisableProjectTypeValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectTypeId);
    }
}
