namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryAssignments;

public class DeleteRequestMachineryAssignmentsCommandValidator : AbstractValidator<DeleteRequestMachineryAssignmentsCommand>
{
    public DeleteRequestMachineryAssignmentsCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
