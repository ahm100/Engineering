namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Queries.GetPODContractorExpertsByCServiceId;

public class GetPODContractorExpertsByCServiceIdQueryValidator : AbstractValidator<GetPODContractorExpertsByCServiceIdQuery>
{
    public GetPODContractorExpertsByCServiceIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailContractorServiceId)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}