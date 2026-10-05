namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryReservations;

public class DeleteRequestMachineryReservationsCommandValidator : AbstractValidator<DeleteRequestMachineryReservationsCommand>
{
    public DeleteRequestMachineryReservationsCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
