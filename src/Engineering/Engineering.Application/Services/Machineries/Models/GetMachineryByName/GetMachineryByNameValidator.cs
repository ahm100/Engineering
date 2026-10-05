namespace Engineering.Application.Services.Machineries.Models.GetMachineryByName;

public class GetMachineryByNameValidator : AbstractValidator<GetMachineryByNameRequest>
{
    public GetMachineryByNameValidator()
    {
        RuleFor(oo => oo.MachineryName).NotEmpty().WithError(MachineryErrors.MachineryNameIsEmpty);
    }
}
