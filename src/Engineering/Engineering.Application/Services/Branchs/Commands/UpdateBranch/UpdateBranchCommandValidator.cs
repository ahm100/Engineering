namespace Engineering.Application.Services.Branchs.Commands.UpdateBranch;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(BranchErrors.BranchWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(v => v.Category)
            .NotEmpty().WithError(BranchErrors.CategoryIdIsEmpty)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound);

        RuleFor(v => v.BranchName)
            .NotEmpty().WithError(BranchErrors.BranchNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(v => v.BranchCode)
            .NotEmpty().WithError(BranchErrors.BranchCodeIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}