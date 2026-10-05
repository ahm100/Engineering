namespace Engineering.Application.Services.RequestContractors.Commands.CreateRequestContractor;

public class CreateRequestContractorCommandValidator : AbstractValidator<CreateRequestContractorCommand>
{
    public CreateRequestContractorCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidProjectOperationDetail);
        RuleFor(oo => oo.ServiceInfo).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidServiceInfo);
        RuleFor(oo => oo.Volume).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidVolume);
    }
}
