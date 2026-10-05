namespace Engineering.Application.Services.Machineries.Models.GetMachineryByCode;

public class GetMachineryByCodeValidator : AbstractValidator<GetMachineryByCodeRequest>
{
    public GetMachineryByCodeValidator()
    {
        RuleFor(oo => oo.MachineryCode).NotEmpty().WithError(MachineryErrors.MachineryCodeIsEmpty);
    }
}
