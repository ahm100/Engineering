namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperationDetail;

public class CreateRequestMachineryProjectOperationDetailCommandValidator : AbstractValidator<CreateRequestMachineryProjectOperationDetailCommand>
{
    public CreateRequestMachineryProjectOperationDetailCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotEmpty().WithError(RequestMachineryProjectOperationDetailErrors.InValidProjectOperationDetail);
        RuleFor(oo => oo.RequestMachinery).NotEmpty().WithError(RequestMachineryProjectOperationDetailErrors.InValidRequestMachinery);
    }
}
