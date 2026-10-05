namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;

public class GetServiceInfoByCodeValidator : AbstractValidator<GetServiceInfoByCodeRequest>
{
    public GetServiceInfoByCodeValidator()
    {
        RuleFor(oo => oo.ServiceInfoCode).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
    }
}
