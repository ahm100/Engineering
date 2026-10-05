namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByContractorIds;

public class GetsOperationInfoByContractorIdsQueryValidator : AbstractValidator<GetsOperationInfoByContractorIdsQuery>
{
    public GetsOperationInfoByContractorIdsQueryValidator()
    {
        RuleFor(oo => oo.ContractorIds)
         .NotEmpty().WithError(OperationInfoErrors.ContractorIsEmpty)
         .NotNull().WithError(OperationInfoErrors.ContractorIsEmpty);

        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}