namespace Engineering.Application.Services.Branchs.Models.GetBranchByCode;

public class GetBranchByCodeValidator : AbstractValidator<GetBranchByCodeRequest>
{
    public GetBranchByCodeValidator()
    {
        RuleFor(v => v.BranchCode)
            .IsRequiredString(BranchCmts.BranchCode);
    }
}