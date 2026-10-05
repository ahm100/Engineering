namespace Engineering.Application.Services.TransportationRequests.Models.UpdateAfterCargoDeclaration;

public class UpdateAfterCargoDeclarationRequestValidator : AbstractValidator<UpdateAfterCargoDeclarationRequest>
{
    public UpdateAfterCargoDeclarationRequestValidator()
    {
        RuleFor(oo => oo.CargoId)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .WithError(TransportationRequestErrors.CargoIsEmpty);
    }
}