namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateMppFileProject;

public class CreateMppFileProjectValidator : AbstractValidator<CreateMppFileProjectRequest>
{
    public CreateMppFileProjectValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
