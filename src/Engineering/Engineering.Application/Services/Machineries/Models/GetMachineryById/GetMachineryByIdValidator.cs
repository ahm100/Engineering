namespace Engineering.Application.Services.Machineries.Models.GetMachineryById;

public class GetMachineryByIdValidator : AbstractValidator<GetMachineryByIdRequest>
{
    public GetMachineryByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}
