namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoId;

public class GetsOperationInfoGroupRelationByIdValidator : AbstractValidator<GetsOperationInfoGroupRelationByIdRequest>
{
    public GetsOperationInfoGroupRelationByIdValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupRelationErrors.OperationInfoIsEmpty);
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
