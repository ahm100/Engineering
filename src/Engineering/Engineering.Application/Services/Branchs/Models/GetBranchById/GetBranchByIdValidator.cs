namespace Engineering.Application.Services.Branchs.Models.GetBranchById;

public class GetBranchByIdValidator : AbstractValidator<GetBranchByIdRequest>
{
    public GetBranchByIdValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.BranchId);

    }
}