namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationId;

public class GetExpertsByProjectOperationIdQueryValidator : AbstractValidator<GetExpertsByProjectOperationIdQuery>
{
    public GetExpertsByProjectOperationIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ConsumableVolumeExpertErrors.ProjectOperationIdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
