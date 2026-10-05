namespace Engineering.Application.Services.SubProjects.Contracts.ChangeSubProjectStatus;

public class ChangeSubProjectStatusValidator : AbstractValidator<ChangeSubProjectStatusRequest>
{
    public ChangeSubProjectStatusValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.Status)
            .IsEnum(GlobalCmts.Status);
    }
}
