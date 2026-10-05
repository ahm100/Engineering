namespace Engineering.Application.Services.Branchs.Models.GetBranchByName;

public class GetBranchByNameValidator : AbstractValidator<GetBranchByNameRequest>
{
    public GetBranchByNameValidator()
    {
        RuleFor(v => v.BranchName)
            .IsRequiredString(BranchCmts.BranchName);
    }
}