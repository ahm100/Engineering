namespace Engineering.Application.Services.ProjectTypes.Models.InactiveProjectType;

public class InactiveProjectTypeValidator : AbstractValidator<InactiveProjectTypeRequest>
{
    public InactiveProjectTypeValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectTypeId);

    }
}
