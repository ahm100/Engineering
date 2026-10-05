namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryRateById;

public class GetFixAssetMachineryRateByIdValidator : AbstractValidator<GetFixAssetMachineryRateByIdRequest>
{
    public GetFixAssetMachineryRateByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
