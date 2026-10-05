namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperation;

public class CreateRequestMachineryProjectOperationCommandValidator : AbstractValidator<CreateRequestMachineryProjectOperationCommand>
{
    public CreateRequestMachineryProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotEmpty().WithError(RequestMachineryProjectOperationErrors.InValidProjectOperation);
        RuleFor(oo => oo.RequestMachinery).NotEmpty().WithError(RequestMachineryProjectOperationErrors.InValidRequestMachinery);
    }
}
