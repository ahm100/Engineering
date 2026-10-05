namespace Engineering.Application.Services.RequestContractors.Models.UpdateRequestContractor;

public class UpdateRequestContractorValidator : AbstractValidator<UpdateRequestContractorRequest>
{
    public UpdateRequestContractorValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.Volume).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidVolume);
    }
}
