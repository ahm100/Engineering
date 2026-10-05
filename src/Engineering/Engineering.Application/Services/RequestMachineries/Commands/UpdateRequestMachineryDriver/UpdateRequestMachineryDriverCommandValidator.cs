namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDriver;

public class UpdateRequestMachineryDriverCommandValidator : AbstractValidator<UpdateRequestMachineryDriverCommand>
{
    public UpdateRequestMachineryDriverCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
