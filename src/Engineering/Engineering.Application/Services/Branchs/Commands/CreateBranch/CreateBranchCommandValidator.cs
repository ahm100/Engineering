namespace Engineering.Application.Services.Branchs.Commands.CreateBranch;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
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