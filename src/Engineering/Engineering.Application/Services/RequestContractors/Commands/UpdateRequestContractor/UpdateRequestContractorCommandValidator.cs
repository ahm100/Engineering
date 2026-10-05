namespace Engineering.Application.Services.RequestContractors.Commands.UpdateRequestContractor;

public class UpdateRequestContractorCommandValidator : AbstractValidator<UpdateRequestContractorCommand>
{
    public UpdateRequestContractorCommandValidator()
    {
        RuleFor(oo => oo.RequestContractor).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidRequestContractor);

        RuleFor(oo => oo.Volume).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidVolume);
    }
}
