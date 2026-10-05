namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryById;

public class GetFixAssetMachineryByIdValidator : AbstractValidator<GetFixAssetMachineryByIdRequest>
{
    public GetFixAssetMachineryByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
