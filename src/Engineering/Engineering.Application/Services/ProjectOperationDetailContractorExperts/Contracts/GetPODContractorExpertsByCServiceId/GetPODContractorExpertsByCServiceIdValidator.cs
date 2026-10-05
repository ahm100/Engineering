namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;

public class GetPODContractorExpertsByCServiceIdValidator : AbstractValidator<GetPODContractorExpertsByCServiceIdRequest>
{
    public GetPODContractorExpertsByCServiceIdValidator()
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