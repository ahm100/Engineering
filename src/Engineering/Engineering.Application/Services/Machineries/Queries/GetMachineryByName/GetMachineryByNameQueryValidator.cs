namespace Engineering.Application.Services.Machineries.Queries.GetMachineryByName;

public class GetMachineryByNameQueryValidator : AbstractValidator<GetMachineryByNameQuery>
{
    public GetMachineryByNameQueryValidator()
    {
        RuleFor(oo => oo.MachineryName).NotEmpty().WithError(MachineryErrors.MachineryNameIsEmpty);
    }
}