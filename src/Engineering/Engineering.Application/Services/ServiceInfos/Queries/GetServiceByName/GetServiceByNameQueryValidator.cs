namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceByName;

public class GetServiceInfoByNameQueryValidator : AbstractValidator<GetServiceInfoByNameQuery>
{
    public GetServiceInfoByNameQueryValidator()
    {
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoNameIsEmpty);
    }
}