namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryProjectOperationDetail;

public class DeleteRequestMachineryProjectOperationDetailCommandValidator : AbstractValidator<DeleteRequestMachineryProjectOperationDetailCommand>
{
    public DeleteRequestMachineryProjectOperationDetailCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryProjectOperationDetailId).NotNull().WithError(RequestMachineryProjectOperationDetailErrors.InValidRequestMachineryProjectOperationDetail);
    }
}
