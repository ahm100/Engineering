namespace Engineering.Application.Services.Machineries.Queries.GetMachineryByCode;

public class GetMachineryByCodeQueryValidator : AbstractValidator<GetMachineryByCodeQuery>
{
    public GetMachineryByCodeQueryValidator()
    {
        RuleFor(oo => oo.MachineryCode).NotEmpty().WithError(MachineryErrors.MachineryCodeIsEmpty);
    }
}