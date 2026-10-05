namespace Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationById;

public class GetMachineryReservationByIdValidator : AbstractValidator<GetMachineryReservationByIdRequest>
{
    public GetMachineryReservationByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MachineryReservationErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
