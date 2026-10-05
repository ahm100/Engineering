namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceById;

public class GetServiceInfoByIdQueryValidator : AbstractValidator<GetServiceInfoByIdQuery>
{
    public GetServiceInfoByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ServiceInfoErrors.IdIsEmpty);
    }
}