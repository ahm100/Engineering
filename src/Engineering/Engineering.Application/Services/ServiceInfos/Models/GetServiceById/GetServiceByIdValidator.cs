namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceById;

public class GetServiceInfoByIdValidator : AbstractValidator<GetServiceInfoByIdRequest>
{
    public GetServiceInfoByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}
