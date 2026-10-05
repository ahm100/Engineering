namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryProjectOperation;

public class DeleteRequestMachineryProjectOperationCommandValidator : AbstractValidator<DeleteRequestMachineryProjectOperationCommand>
{
    public DeleteRequestMachineryProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryProjectOperationId).NotNull().WithError(RequestMachineryProjectOperationErrors.InValidProjectOperation);
    }
}
