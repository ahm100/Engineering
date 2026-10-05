namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceByCode;

public class GetServiceInfoByCodeQueryValidator : AbstractValidator<GetServiceInfoByCodeQuery>
{
    public GetServiceInfoByCodeQueryValidator()
    {
        RuleFor(oo => oo.ServiceInfoCode).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
    }
}