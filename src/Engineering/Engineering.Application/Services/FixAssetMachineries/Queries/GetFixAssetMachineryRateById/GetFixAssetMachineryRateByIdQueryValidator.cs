namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryRateById;

public class GetFixAssetMachineryRateByIdQueryValidator : AbstractValidator<GetFixAssetMachineryRateByIdQuery>
{
    public GetFixAssetMachineryRateByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}