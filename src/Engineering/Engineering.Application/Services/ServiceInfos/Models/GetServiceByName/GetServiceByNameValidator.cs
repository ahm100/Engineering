namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceByName;

public class GetServiceInfoByNameValidator : AbstractValidator<GetServiceInfoByNameRequest>
{
    public GetServiceInfoByNameValidator()
    {
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoNameIsEmpty);
    }
}
