namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorkById;

public class GetFixAssetMachineryNotWorkByIdQueryValidator : AbstractValidator<GetFixAssetMachineryNotWorkByIdQuery>
{
    public GetFixAssetMachineryNotWorkByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}