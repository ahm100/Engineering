namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByContractorIds;

public class GetsProjectOperationDetailByContractorIdsRequestValidator : AbstractValidator<GetsProjectOperationDetailByContractorIdsRequest>
{
    public GetsProjectOperationDetailByContractorIdsRequestValidator()
    {
        RuleFor(oo => oo.ContractorIds)
            .NotEmpty().WithError(ProjectOperationDetailErrors.ContactorsIdIsEmpty)
            .NotNull().WithError(ProjectOperationDetailErrors.ContactorsIdIsEmpty);

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
