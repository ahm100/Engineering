namespace Engineering.Application.Services.RequestMachineries.Queries.GetFilteredRequestMachineryProjectOperationDetails;

public class GetFilteredRequestMachineryProjectOperationDetailsQueryValidator : AbstractValidator<GetFilteredRequestMachineryProjectOperationDetailsQuery>
{
    public GetFilteredRequestMachineryProjectOperationDetailsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(RequestMachineryErrors.InValidProjectOperationDetail);
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
