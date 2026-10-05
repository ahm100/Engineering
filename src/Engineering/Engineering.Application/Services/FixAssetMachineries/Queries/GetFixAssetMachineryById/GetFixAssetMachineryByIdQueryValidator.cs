namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;

public class GetFixAssetMachineryByIdQueryValidator : AbstractValidator<GetFixAssetMachineryByIdQuery>
{
    public GetFixAssetMachineryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}