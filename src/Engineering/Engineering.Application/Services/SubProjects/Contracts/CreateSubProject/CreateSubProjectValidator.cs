namespace Engineering.Application.Services.SubProjects.Contracts.CreateSubProject;

public class CreateSubProjectValidator : AbstractValidator<CreateSubProjectRequest>
{
    public CreateSubProjectValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.Name)
            .HasMaxLength(SubProjectCmts.Name, 250);

        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);

        RuleFor(oo => oo.StartDate)
            .IsDate(GlobalCmts.StartDate);

        RuleFor(oo => oo.EndDate)
            .IsOptionalDateGreaterThanOrEqualTo(
                oo => oo.StartDate,
                GlobalCmts.EndDate,
                GlobalCmts.StartDate);

        RuleFor(oo => oo.Description)
            .HasOptionalMaxLength(GlobalCmts.Description, 1500);

        RuleFor(oo => oo.ManagerId)
            .IsOptionalPositive(SubProjectCmts.ManagerId);
    }
}
