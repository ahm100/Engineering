namespace Engineering.Application.Services.Machineries.Queries.GetMachineryById;

public class GetMachineryByIdQueryValidator : AbstractValidator<GetMachineryByIdQuery>
{
    public GetMachineryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MachineryErrors.IdIsEmpty);
    }
}