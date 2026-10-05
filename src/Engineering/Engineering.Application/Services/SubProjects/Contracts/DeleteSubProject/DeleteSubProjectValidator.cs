namespace Engineering.Application.Services.SubProjects.Contracts.DeleteSubProject;

public class DeleteSubProjectValidator : AbstractValidator<DeleteSubProjectRequest>
{
    public DeleteSubProjectValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
