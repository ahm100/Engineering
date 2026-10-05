namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;

public class GetPODContractorExpertsByCVEIdValidator : AbstractValidator<GetPODContractorExpertsByCVEIdRequest>
{
    public GetPODContractorExpertsByCVEIdValidator()
    {
        RuleFor(oo => oo.ConsumableVolumeExpertId)
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