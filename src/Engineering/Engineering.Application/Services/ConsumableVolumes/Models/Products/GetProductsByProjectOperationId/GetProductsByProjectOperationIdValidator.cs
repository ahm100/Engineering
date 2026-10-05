namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationId;

public class GetProductsByProjectOperationIdValidator : AbstractValidator<GetProductsByProjectOperationIdRequest>
{
    public GetProductsByProjectOperationIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.ProjectOperationIdIsEmpty);
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
