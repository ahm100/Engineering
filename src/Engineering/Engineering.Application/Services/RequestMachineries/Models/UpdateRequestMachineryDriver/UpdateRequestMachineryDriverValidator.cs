namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDriver;

public class UpdateRequestMachineryDriverValidator : AbstractValidator<UpdateRequestMachineryDriverRequest>
{
    public UpdateRequestMachineryDriverValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
