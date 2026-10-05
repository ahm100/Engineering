namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByIds;

public class GetsBranchByIdsQueryValidator : AbstractValidator<GetsBranchByIdsQuery>
{
    public GetsBranchByIdsQueryValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(BranchErrors.IdIsEmpty);
    }
}