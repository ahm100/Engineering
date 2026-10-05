namespace Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservationById;

public class GetMachineryReservationByIdQueryValidator : AbstractValidator<GetMachineryReservationByIdQuery>
{
    public GetMachineryReservationByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MachineryReservationErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}