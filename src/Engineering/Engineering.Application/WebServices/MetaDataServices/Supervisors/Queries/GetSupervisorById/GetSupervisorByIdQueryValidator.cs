
namespace Engineering.Application.WebServices.MetaDataServices.Supervisors.Queries.GetSupervisorById;

public class GetSupervisorByIdQueryValidator : AbstractValidator<GetSupervisorByIdQuery>
{
    public GetSupervisorByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}