namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorkById;

public class GetFixAssetMachineryNotWorkByIdValidator : AbstractValidator<GetFixAssetMachineryNotWorkByIdRequest>
{
    public GetFixAssetMachineryNotWorkByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
