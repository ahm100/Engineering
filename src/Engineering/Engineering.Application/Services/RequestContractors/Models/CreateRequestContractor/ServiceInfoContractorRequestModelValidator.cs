namespace Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;

public class ServiceInfoContractorRequestModelValidator : AbstractValidator<ServiceInfoContractorRequestModel>
{
    public ServiceInfoContractorRequestModelValidator()
    {
        RuleFor(oo => oo.ServiceInfoId).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidServiceInfo)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.Volume).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidVolume);
    }
}
